using Microsoft.AspNetCore.Identity;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthenticationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> RegisterUser(User user, string password)
        {
            var userExists = await _userManager.FindByNameAsync(user.UserName);

            if (userExists is not null) return false;

            var result = await _userManager.CreateAsync(user, password);
            
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            

            return result.Succeeded;
        }
    }
}
