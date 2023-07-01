using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationService
    {
        Task<List<Collaboration>> GetAllCollaborations();
        Task<Collaboration?> GetCollaborationById(Guid collaborationId);
        Task CreateCollaboration(Collaboration collaboration);
        Task UpdateCollaboration(Guid collaborationId, Collaboration collaboration);
        Task DeleteCollaborationById(Guid collaborationId);
    }
}
