using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Register(UserRegisterDTO request)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email, 
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var created = await _authenticationService.RegisterUser(user, request.Password);

            if (!created) return BadRequest();

            return CreatedAtAction("register", created);
        }

    }
}
