using Microsoft.AspNetCore.Identity;
using projectverseAPI.Models;

namespace projectverseAPI.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> RegisterUser(User user, string password);
    }
}
