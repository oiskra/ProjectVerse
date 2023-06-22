namespace projectverseAPI.Models
{
    public class CollaborationPosition
    {
        public Guid Id { get; set; }
        public Collaboration Collaboration { get; set; }
        public Guid CollaborationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
