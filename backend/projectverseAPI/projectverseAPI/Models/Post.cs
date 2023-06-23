namespace projectverseAPI.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
        public long Views { get; set; }
        public long Likes { get; set; }
        public IList<PostComment> PostComments { get; set; }
    }
}
