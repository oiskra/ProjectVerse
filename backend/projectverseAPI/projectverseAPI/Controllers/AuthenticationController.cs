using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Interfaces;
using projectverseAPI.Migrations;
using projectverseAPI.Models;
using System.Security.Cryptography;

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
        public async Task<ActionResult<IdentityResult>> Register(UserRegisterDTO request)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,                
            };

            var response = await _authenticationService.RegisterUser(user, request.Password);

            if (!response.Succeeded) return BadRequest(response);

            return CreatedAtAction("register", response);
        }


    }
}
