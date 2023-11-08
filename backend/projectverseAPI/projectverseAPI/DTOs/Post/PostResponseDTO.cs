using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Post
{
    public class PostResponseDTO
    {
        public Guid Id { get; set; }
        public PostProjectDTO Project { get; set; }
        public long Views { get; set; }
        public long Likes { get; set; }
        public IList<PostCommentDTO> PostComments { get; set; }
    }
}
