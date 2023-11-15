using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Post
{
    public class PostResponseDTO
    {
        public Guid Id { get; set; }
        public PostProjectDTO Project { get; set; }
        public long ViewsCount { get; set; }
        public long LikesCount { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public IList<PostCommentDTO> PostComments { get; set; }
    }
}
