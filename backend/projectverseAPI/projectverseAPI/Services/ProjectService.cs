using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Project;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

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
        
        public async Task<List<Project>> GetAll(string? searchTerm)
        {
            IQueryable<Project> projectQuery = _context.Projects;

            if (searchTerm is not null)
            {
                projectQuery = projectQuery.Where(
                    p => p.Name.Contains(searchTerm));
            }

            var projects = await projectQuery
                .Include(p => p.UsedTechnologies)
                .Include(p => p.Author)
                .ToListAsync();

            return projects;
        }

        public async Task<List<Project>> GetAllByUserId(Guid userId)
        {
            var usersProjects = await _context.Projects
                .Where(p => p.AuthorId == userId)
                .Include(p => p.Author)
                .Include(p => p.UsedTechnologies)
                .ToListAsync();

            return usersProjects;
        }

        public async Task<Project> GetById(Guid projectId)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .Where(p => p.Id == projectId)
                .Include(p => p.Author)
                .Include(p => p.UsedTechnologies)
                .FirstOrDefaultAsync();

            return project;
        }

        public async Task<Project> Create(CreateProjectRequestDTO projectDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var project = _mapper.Map<Project>(projectDTO);
                var currentUser = await _authenticationService.GetCurrentUser();
                
                project.Author = currentUser;
                project.AuthorId = Guid.Parse(currentUser.Id);

                if (projectDTO.IsPublished.Equals(false))
                {
                    var addedProject = await _context.Projects.AddAsync(project);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                
                    return addedProject.Entity;
                }

                var addedPublishedProject = await _context.Projects.AddAsync(project);
                
                var post = new Post
                {
                    Id = Guid.NewGuid(),
                    Project = project,
                    ProjectId = project.Id,
                    ViewsCount = 0,
                    LikesCount = 0,
                    PostComments = new List<PostComment>()
                };

                await _context.Posts.AddAsync(post);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return addedPublishedProject.Entity;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }
        public async Task<Project> Update(UpdateProjectRequestDTO projectDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var project = await _context.Projects
                    .AsNoTracking()
                    .Include(p => p.UsedTechnologies)
                    .FirstOrDefaultAsync(p => p.Id == projectDTO.Id);

                if (project is null)
                    throw new ArgumentException("Project doesn't exist.");

                project.Name = projectDTO.Name;
                project.Description = projectDTO.Description;
                project.ProjectUrl = projectDTO.ProjectUrl;
                project.UsedTechnologies = project.UsedTechnologies
                    .Join(
                        projectDTO.UsedTechnologies,
                        t1 => t1.Id,
                        t2 => t2.Id,
                        (t1,t2) => new Technology { Name = t2.Name, Id = t1.Id})
                    .ToList();
                project.IsPrivate = projectDTO.IsPrivate;
                project.IsPublished = projectDTO.IsPublished;

                _context.Projects.Update(project);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return project;
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

        public async Task Delete(Guid projectId)
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
