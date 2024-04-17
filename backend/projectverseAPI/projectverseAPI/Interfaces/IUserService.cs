using projectverseAPI.DTOs.User;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IUserService : 
        IGetAll<User>,
        IGetById<User>,
        IUpdate<UpdateUserRequestDTO, User>
    {
    }
}
