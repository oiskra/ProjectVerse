using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Post
{
    public class PostCommentDTO
    {
        public Guid Id { get; set; }
        public PostCommentAuthorDTO Author { get; set; }
        public string Body { get; set; }
        public DateTime PostedAt { get; set; }
    }

    public class PostCommentAuthorDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
