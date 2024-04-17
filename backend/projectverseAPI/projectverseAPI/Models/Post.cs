namespace projectverseAPI.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
        public long ViewsCount { get; set; }
        public long LikesCount { get; set; }
        public IList<Like> Likes { get; set; }
        public IList<PostComment> PostComments { get; set; }
    }
}
