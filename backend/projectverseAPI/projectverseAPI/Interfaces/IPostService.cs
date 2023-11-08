﻿using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts();
        //Task<Post> GetPostById(Guid postId);
        Task<Guid> CreatePost(Guid projectId);
        //Task DeletePost(Guid postId);

        //Task LikePost(Guid postId);
        //Task UnlikePost(Guid postId);

        //Task RecordPostView(Guid postId);

        //Task CreatePostComment(Guid postId, CreatePostCommentRequestDTO createPostCommentDTO);
        //Task GetAllPostCommentsFromPost(Guid postId);
        //Task UpdatePostComment(Guid postId, UpdatePostCommentRequestDTO updatePostCommentDTO);
        //Task DeletePostComment(Guid commentId);
    }
}
