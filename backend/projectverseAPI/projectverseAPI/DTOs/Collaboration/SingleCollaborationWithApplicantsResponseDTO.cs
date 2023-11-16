using projectverseAPI.Models;

namespace projectverseAPI.DTOs.Collaboration
{
    public class SingleCollaborationWithApplicantsResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Difficulty { get; set; }
        public int PeopleInvolved { get; set; }
        public CollaborationAuthorDTO Author { get; set; }
        public IList<Technology> Technologies { get; set; }
        public IList<CollaborationPositionDTO> CollaborationPositions { get; set; }
        public IList<CollaborationApplicantDTO>? CollaborationApplicants { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
