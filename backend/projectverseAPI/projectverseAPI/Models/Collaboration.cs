using projectverseAPI.Interfaces.Marker;

namespace projectverseAPI.Models
{
    public class Collaboration : IAuthorizableByAuthor
    {
        public Guid Id { get; set; }
        public User? Author { get; set; }
        public Guid AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Difficulty { get; set; }
        public int PeopleInvolved { get; set; }
        public IList<Technology>? Technologies { get; set; }
        public IList<CollaborationPosition>? CollaborationPositions { get; set; }
        public IList<CollaborationApplicant>? CollaborationApplicants { get; set; } = new List<CollaborationApplicant>();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
