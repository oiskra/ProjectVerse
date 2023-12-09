using AutoMapper;
using Microsoft.EntityFrameworkCore;
using projectverseAPI.Data;
using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ProjectVerseContext _context;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public CommentService(
            ProjectVerseContext context,
            IAuthenticationService authenticationService,
            IMapper mapper)
        {
            _context = context;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<PostComment> CreateRelated(Guid postId, CreatePostCommentRequestDTO createPostCommentDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var comment = _mapper.Map<PostComment>(createPostCommentDTO);

                var currentUser = await _authenticationService.GetCurrentUser();

                var post = await _context.Posts
                    .FirstOrDefaultAsync(p => p.Id == postId);

                if (post is null)
                    throw new ArgumentException("Post doesn't exist.");

                comment.Post = post;
                comment.PostId = post.Id;
                comment.Author = currentUser;
                comment.AuthorId = Guid.Parse(currentUser.Id);

                var createdPost = await _context.PostComments.AddAsync(comment);
                await transaction.CommitAsync();
                await _context.SaveChangesAsync();

                return createdPost.Entity;
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

        public async Task<List<PostComment>> GetAllPostCommentsFromPost(Guid postId)
        {
            var existingPost = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == postId);

            if (existingPost is null)
                throw new ArgumentException("Post doesn't exist.");

            var comments = await _context.PostComments
                .Where(pc => pc.PostId == postId)
                .Include(pc => pc.Author)
                .ToListAsync();

            return comments;
        }

        public async Task<PostComment> GetById(Guid commentId)
        {
            var project = await _context.PostComments
                .Where(pc => pc.Id == commentId)
                .Include(pc => pc.Author)
                .FirstOrDefaultAsync();

            if (project is null)
                throw new ArgumentException("Comment doesn't exist.");

            return project;
        }

        public async Task<PostComment> Update(UpdatePostCommentRequestDTO updatePostCommentDTO)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var comment = await _context.PostComments
                    .FirstOrDefaultAsync(pc => pc.Id == updatePostCommentDTO.Id);

                if (comment is null)
                    throw new ArgumentException("Comment doesn't exist.");

                comment.Body = updatePostCommentDTO.Body;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return comment;
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


        public async Task Delete(Guid commentId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var commentToDelete = await _context.PostComments
                    .FirstOrDefaultAsync(pc => pc.Id == commentId);

                if (commentToDelete is null)
                    throw new ArgumentException("Comment doesn't exist.");

                _context.PostComments.Remove(commentToDelete);

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
    }
}
