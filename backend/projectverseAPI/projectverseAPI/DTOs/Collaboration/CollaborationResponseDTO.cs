using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Collaboration
{
    public class CollaborationResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public CollaborationAuthorDTO Author { get; set; }
        public IList<Technology> Technologies { get; set; }
        public IList<CollaborationPositionDTO> CollaborationPositions { get; set; }
        
    }
}
