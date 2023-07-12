using Microsoft.AspNetCore.Identity;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;

        public AuthenticationService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterUser(User user, string password)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);

            if (userExists is not null) return false;

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
    }
}
