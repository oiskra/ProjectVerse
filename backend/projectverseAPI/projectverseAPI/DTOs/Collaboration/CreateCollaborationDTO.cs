using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Collaboration
{
    public class CreateCollaborationDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public IList<Technology> Technologies { get; set; }
        public IList<CreateCollaborationPositionDTO>? CollaborationPositions { get; set; }

    }
}
