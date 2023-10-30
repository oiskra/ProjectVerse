using Microsoft.AspNetCore.Identity;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterUser(UserRegisterDTO userRegisterDTO);
        Task<TokenResponseDTO?> LoginUser(UserLoginDTO userLoginDto);
        Task<User?> GetCurrentUser();
        Task<TokenResponseDTO> RefreshToken(RefreshRequestDTO refreshRequestDTO);
        Task RevokeToken();
    }
}
