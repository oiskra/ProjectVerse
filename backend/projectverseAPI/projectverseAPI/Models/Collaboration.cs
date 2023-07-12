namespace projectverseAPI.Models
{
    public class Collaboration
    {
        public Guid Id { get; set; }
        public User? Author { get; set; }
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Difficulty { get; set; }
        public IList<Technology>? Technologies { get; set; }
        public IList<CollaborationPosition>? CollaborationPositions { get; set; }
    }
}
