using Microsoft.AspNetCore.Identity;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticationService(UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManager.
        }

        public async Task<bool> RegisterUser(User user, string password)
        {
            var userExists = _userManager.FindByEmailAsync(user.Email);

            if (userExists is not null) return false;

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
    }
}
