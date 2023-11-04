using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Project;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace projectverseAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ProjectVerseContext _context;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public ProjectService(
            ProjectVerseContext context,
            IAuthenticationService authenticationService,
            IMapper mapper)
        {
            _context = context;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }
        
        public async Task<List<Project>> GetAllProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.UsedTechnologies)
                .Include(p => p.User)
                .ToListAsync();

            return projects;
        }
        
        public async Task<List<Project>> GetAllProjectsByUserID(Guid userId)
        {
            var usersProjects = await _context.Projects
                .Include(p => p.User)
                .Include(p => p.UsedTechnologies)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return usersProjects;
        }

        public async Task<Project?> GetProjectById(Guid projectId)
        {
            var project = await _context.Projects
                .Include(p => p.User)
                .Include(p => p.UsedTechnologies)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            return project;
        }

        public async Task<Guid> CreateProject(CreateProjectRequestDTO projectDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var project = _mapper.Map<Project>(projectDTO);
                var currentUser = await _authenticationService.GetCurrentUser();

                if (currentUser is null)
                    throw new Exception("Cannot get current user.");

                if (projectDTO.IsPublished.Equals(false))
                {
                    project.User = currentUser;
                    project.UserId = Guid.Parse(currentUser.Id);

                    var addedProject = await _context.Projects.AddAsync(project);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                
                    return addedProject.Entity.Id;
                }

                Guid[] createdEntitesIds = await Task.WhenAll(new Task<Guid>[]
                {
                    Task.Run(async () =>
                    {
                        project.User = currentUser;
                        project.UserId = Guid.Parse(currentUser.Id);

                        var addedProject = await _context.Projects.AddAsync(project);
                        return addedProject.Entity.Id;
                    }),
                    Task.Run(async () =>
                    {
                        var post = new Post
                        {
                            Id = Guid.NewGuid(),
                            Project = project,
                            ProjectId = project.Id,
                            Views = 0,
                            Likes = 0,
                            PostComments = new List<PostComment>()
                        };

                        var addedPost = await _context.Posts.AddAsync(post);
                        return addedPost.Entity.Id;
                    })
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return createdEntitesIds.First();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }
        public async Task UpdateProject(UpdateProjectRequestDTO projectDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var exists = _context.Projects.Any(c => c.Id == projectDTO.Id);

                if (!exists)
                    throw new ArgumentException("Project doesn't exist.");

                var project = _mapper.Map<Project>(projectDTO);
                _context.Projects.Update(project);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteProject(Guid projectId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var projectToDelete = await _context.Projects
                    .Include(c => c.UsedTechnologies)
                    .FirstOrDefaultAsync(c => c.Id == projectId);

                if (projectToDelete is null)
                    throw new ArgumentException("Project doesn't exist");

                var associatedPost = await _context.Posts
                    .Include(p => p.PostComments)
                    .FirstOrDefaultAsync(p => p.ProjectId == projectToDelete.Id);

                if(associatedPost is not null) 
                    _context.Posts.Remove(associatedPost);

                _context.Projects.Remove(projectToDelete);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }       
    }
}
