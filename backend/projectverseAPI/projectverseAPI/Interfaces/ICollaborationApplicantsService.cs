using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces.Common;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationApplicantsService : IDelete
    {
        Task<Guid> ApplyForCollaboration(Guid collaborationId, Guid collaborationPositionId);
        Task ChangeApplicationStatus(Guid applicantId, ChangeApplicationStatusRequestDTO applicationStateRequestDTO);
    }
}
