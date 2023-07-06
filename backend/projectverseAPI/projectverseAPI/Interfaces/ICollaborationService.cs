using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationService
    {
        Task<List<Collaboration>> GetAllCollaborations();
        Task<Collaboration?> GetCollaborationById(Guid collaborationId);
        Task<Collaboration?> CreateCollaboration(Collaboration collaboration);
        Task UpdateCollaboration(Collaboration collaboration);
        Task DeleteCollaborationById(Guid collaborationId);
    }
}
