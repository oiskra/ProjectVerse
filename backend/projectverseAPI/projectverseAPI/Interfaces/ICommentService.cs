using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICommentService :
        ICreateRelated<CreatePostCommentRequestDTO, PostComment>,
        IUpdate<UpdatePostCommentRequestDTO, PostComment>,
        IGetById<PostComment>,
        IDelete
    {
        Task<List<PostComment>> GetAllPostCommentsFromPost(Guid postId);
    }
}
