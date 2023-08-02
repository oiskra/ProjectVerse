using Microsoft.AspNetCore.Identity;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterUser(UserRegisterDTO userRegisterDTO);
        Task<string?> LoginUser(UserLoginDTO userLoginDto);
        Task<User?> GetCurrentUser();
        Task Logout();
    }
}
