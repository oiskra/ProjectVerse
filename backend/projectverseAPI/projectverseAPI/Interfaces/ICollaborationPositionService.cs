using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface ICollaborationPositionService : 
        ICreateRelated<CreateCollaborationPositionDTO, CollaborationPosition>,
        IUpdate<UpdateCollaborationPositionDTO, CollaborationPosition>,
        IDelete
    {
    }
}
