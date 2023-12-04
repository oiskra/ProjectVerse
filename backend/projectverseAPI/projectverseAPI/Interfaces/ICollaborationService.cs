using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationService : 
        IGetAll<Collaboration>, 
        IGetAllByUserId<Collaboration>,
        IGetById<Collaboration>,
        ICreate<CreateCollaborationRequestDTO, Collaboration>,
        IUpdate<UpdateCollaborationRequestDTO, Collaboration>,
        IDelete

    {
        /*Task<List<Collaboration>> GetAllCollaborations();
        Task<List<Collaboration>> GetAllCollaborationsByUserId(Guid userId);
        Task<Collaboration?> GetCollaborationById(Guid collaborationId);
        Task<Guid> CreateCollaboration(CreateCollaborationRequestDTO collaboration);
        Task UpdateCollaboration(UpdateCollaborationRequestDTO collaboration);
        Task<bool> DeleteCollaborationById(Guid collaborationId);*/

        Task<Guid> AddCollaborationPosition(Guid collaborationId, CreateCollaborationPositionDTO collaborationPositionDTO);
        Task DeleteCollaborationPositionById(Guid collaborationId, Guid collaborationPositionId);
    }
}
