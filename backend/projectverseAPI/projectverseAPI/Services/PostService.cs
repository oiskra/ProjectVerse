using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class PostService : IPostService
    {
        private readonly ProjectVerseContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public PostService(
            ProjectVerseContext context,
            IMapper mapper,
            IAuthenticationService authenticationService)
        {
            _context = context;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<Guid> CreatePost(Guid projectId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == projectId);

                if (project is null)
                    throw new ArgumentException("Project doesn't exist.");

                if (_context.Posts.Any(p => p.ProjectId == project.Id))
                    throw new InvalidOperationException("Post with this project already exists.");

                var newPost = new Post
                {
                    Id = Guid.NewGuid(),
                    Likes = 0,
                    Views = 0,
                    ProjectId = project.Id,
                    Project = project,
                    PostComments = new List<PostComment>()
                };

                var createdPost = await _context.Posts.AddAsync(newPost);

                project.IsPublished = true;
                _context.Projects.Update(project);
                
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return createdPost.Entity.Id;
            }
            catch (ArgumentException argE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(argE.Message);
            }
            catch (InvalidOperationException ioE)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException(ioE.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }

        }

        public async Task DeletePost(Guid projectId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var postToDelete = await _context.Posts
                    .Include(p => p.PostComments)
                    .FirstOrDefaultAsync(p => p.Id == projectId);

                if (postToDelete is null)
                    throw new ArgumentException("Post doesn't exist.");

                var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == postToDelete.ProjectId);

                if (project is null)
                    throw new ArgumentException("Associated project doesn't exist.");

                project.IsPublished = false;

                _context.Posts.Remove(postToDelete);
                _context.Projects.Update(project);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (ArgumentException e)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(e.Message);
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _context.Posts
                .Include(p => p.PostComments)
                .Include(p => p.Project)
                    .ThenInclude(p => p.Author)
                .Include(p => p.Project)
                    .ThenInclude(p => p.UsedTechnologies)
                .ToListAsync();

            return posts;
        }

        public async Task RecordPostView(Guid postId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var post = await _context.Posts
                        .Include(p => p.PostComments)
                        .FirstOrDefaultAsync(p => p.Id == postId);

                if (post is null)
                    throw new ArgumentException("Post doesn't exist.");

                post.Views += 1;
                _context.Posts.Update(post);

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
