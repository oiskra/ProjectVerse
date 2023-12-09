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

    { }
}
