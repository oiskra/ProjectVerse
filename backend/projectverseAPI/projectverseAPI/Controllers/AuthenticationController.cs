using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Authentication;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/auth")]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] UserLoginDTO request)
        {
            var result = await _authenticationService.LoginUser(request);

            return result is null 
                ? Unauthorized(new ErrorResponseDTO
                    {
                        Title = "Unauthorized",
                        Status = StatusCodes.Status401Unauthorized,
                        Errors = null
                    }) 
                : Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                await _authenticationService.RevokeToken();
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = e.Message
                });
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<TokenResponseDTO>> Refresh(RefreshRequestDTO refreshRequestDTO)
        {
            try
            {
                var newTokens = await _authenticationService.RefreshToken(refreshRequestDTO);

                return Ok(newTokens);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = e.Message
                });
            }
        }
    }
}
