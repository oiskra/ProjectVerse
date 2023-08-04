using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
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
            try
            {
                var created = await _authenticationService.RegisterUser(request);

                return CreatedAtAction("register", created);
            }
            catch (ArgumentException argE)
            {
                var error = new Dictionary<string, object>();
                if(argE.ParamName is not null)
                    error.Add(argE.ParamName, new List<string> { argE.Message });
                
                return Conflict(new ErrorResponseDTO
                {
                    Title = "Conflict",
                    Status = StatusCodes.Status409Conflict,
                    Errors = error
                });                
            }
            catch(Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDTO
                    {
                        Title = "Internal Server Error",
                        Status = StatusCodes.Status500InternalServerError,
                        Errors = null
                    });
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string?>> Login([FromBody] UserLoginDTO request)
        {
            var result = await _authenticationService.LoginUser(request);

            return result is null 
                ? Unauthorized(new ErrorResponseDTO
                    {
                        Title = "Unauthorized",
                        Status = StatusCodes.Status401Unauthorized,
                        Errors = null
                    }) 
                : Ok(new TokenResponseDTO { Token = result });
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
