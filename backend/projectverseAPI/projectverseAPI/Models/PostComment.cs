namespace projectverseAPI.Models
{
    public class PostComment
    {
        public Guid Id { get; set; }
        public Post Post { get; set; }
        public Guid PostId { get; set; }
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Body { get; set; }
        public DateTime PostedAt { get; set; }
    }
}
