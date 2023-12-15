using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.Models
{
    public class Project : IAuthorizableByAuthor
    {
        public Guid Id { get; set; }
        public User Author { get; set; }
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectUrl { get; set; }
        public IList<Technology> UsedTechnologies { get; set; }
        public bool IsPrivate  { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
