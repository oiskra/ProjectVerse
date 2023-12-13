using projectverseAPI.Interfaces.Common;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IUserProfileDataService :
        IGetById<UserProfileData>,
        IUpdate<UpdateUserProfileData, UserProfileData>
    { }
}
