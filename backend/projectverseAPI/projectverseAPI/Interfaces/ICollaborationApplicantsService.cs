using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationApplicantsService
    {
        Task<Guid> ApplyForCollaboration(Guid collaborationId, Guid collaborationPositionId);
        Task RemoveApplicantForCollaboration(Guid applicantId);
        Task<List<CollaborationApplicant>> GetCollaborationApplicants(Guid collaborationId);
        Task ChangeApplicationStatus(Guid applicantId, ChangeApplicationStatusRequestDTO applicationStateRequestDTO);
    }
}
