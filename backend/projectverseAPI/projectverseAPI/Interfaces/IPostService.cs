using projectverseAPI.DTOs.Post;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPosts();
        Task<Guid> CreatePost(Guid projectId);
        Task DeletePost(Guid projectId);

        Task LikePost(Guid postId);
        Task UnlikePost(Guid postId);

        Task RecordPostView(Guid postId);
    }
}
