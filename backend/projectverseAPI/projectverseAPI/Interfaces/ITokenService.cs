using projectverseAPI.Models;
using System.Security.Claims;

namespace projectverseAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
    }
}
