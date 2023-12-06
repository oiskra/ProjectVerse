using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Post;
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

                if (project.IsPrivate)
                    throw new InvalidOperationException("Post can't be create when project is private.");

                if (_context.Posts.Any(p => p.ProjectId == project.Id))
                    throw new InvalidOperationException("Post with this project already exists.");

                var newPost = new Post
                {
                    Id = Guid.NewGuid(),
                    LikesCount = 0,
                    ViewsCount = 0,
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
                    .Where(p => p.Id == projectId)
                    .Include(p => p.PostComments)
                    .FirstOrDefaultAsync();

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
                .Include(p => p.PostComments
                    .OrderByDescending(pc => pc.PostedAt)
                    .Take(3))
                .Include(p => p.Project)
                    .ThenInclude(p => p.Author)
                .Include(p => p.Project)
                    .ThenInclude(p => p.UsedTechnologies)
                .ToListAsync();

            return posts;
        }

        public async Task LikePost(Guid postId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var currentUser = await _authenticationService.GetCurrentUser();

                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                if (post is null)
                    throw new ArgumentException("Post doesn't exist.");

                var existingLike = await _context.Likes
                    .Where(l =>
                        (l.Post != null && l.Post.Id == postId) &&
                        (l.User != null && l.User.Id == currentUser.Id))
                    .Include(l => l.User)
                    .Include(l => l.Post)
                    .FirstOrDefaultAsync();

                if(existingLike is not null)
                    throw new InvalidOperationException("User has already liked this post.");
                
                post.LikesCount += 1;
                _context.Posts.Update(post);
                await _context.Likes.AddAsync(new Like
                {
                    Id = Guid.NewGuid(),
                    User = currentUser,
                    Post = post
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return;
            }
            catch (InvalidOperationException ioE)
            {
                await transaction.RollbackAsync();
                throw new ArgumentException(ioE.Message);
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

        public async Task UnlikePost(Guid postId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var currentUser = await _authenticationService.GetCurrentUser();

                var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
                if (post is null)
                    throw new ArgumentException("Post doesn't exist.");

                var like = await _context.Likes
                    .Where(l =>
                        (l.Post != null && l.Post.Id == postId) &&
                        (l.User != null && l.User.Id == currentUser.Id))
                    .Include(l => l.User)
                    .Include(l => l.Post)
                    .FirstOrDefaultAsync();
                if(like is null)
                    throw new ArgumentException("User hasn't liked this post.");

                post.LikesCount -= 1;
                _context.Posts.Update(post);
                _context.Likes.Remove(like);

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

        public async Task RecordPostView(Guid postId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var post = await _context.Posts
                    .Where(p => p.Id == postId)        
                    .Include(p => p.PostComments)
                    .FirstOrDefaultAsync();

                if (post is null)
                    throw new ArgumentException("Post doesn't exist.");

                post.ViewsCount += 1;
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
