using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationService
    {
        Task<List<Collaboration>> GetAllCollaborations();
        Task<Collaboration?> GetCollaborationById(Guid collaborationId);
        Task<Guid> CreateCollaboration(CreateCollaborationRequestDTO collaboration);
        Task UpdateCollaboration(UpdateCollaborationRequestDTO collaboration);
        Task<bool> DeleteCollaborationById(Guid collaborationId);
    }
}
