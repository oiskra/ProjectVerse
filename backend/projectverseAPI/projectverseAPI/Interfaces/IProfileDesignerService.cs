using projectverseAPI.DTOs.Designer;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IProfileDesignerService : 
        ICreate<Guid, ProfileDesigner>,
        IGetById<ProfileDesigner>,
        IUpdate<UpdateProfileDesignerRequestDTO, ProfileDesigner>
    {
    }
}
