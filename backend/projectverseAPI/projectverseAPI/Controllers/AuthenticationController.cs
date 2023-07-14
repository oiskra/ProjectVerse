using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(
            IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<bool>> Register([FromBody] UserRegisterDTO request)
        {
            var created = await _authenticationService.RegisterUser(request);

            if (!created) return BadRequest();

            return CreatedAtAction("register", created);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string?>> Login([FromBody] UserLoginDTO request)
        {
            var result = await _authenticationService.LoginUser(request);

            return result is null ? Unauthorized("Invalid email or password") : Ok(result);
        }

        [HttpPost]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.Logout();
            return NoContent();
        }
    }
}
