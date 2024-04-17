using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IPostService :
        IGetAll<Post>,
        IDelete
    {
        Task<Guid> CreatePost(Guid projectId);

        Task LikePost(Guid postId);
        Task UnlikePost(Guid postId);

        Task RecordPostView(Guid postId);
    }
}
