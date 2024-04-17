using projectverseAPI.DTOs.UserProfileData;
using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IUserProfileDataService :
        ICreate<User, UserProfileData>,
        IGetById<UserProfileData>,
        IUpdate<UpdateUserProfileDataRequestDTO, UserProfileData>
    { }
}
