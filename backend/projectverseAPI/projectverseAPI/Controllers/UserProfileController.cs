using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserProfileDataService _profileDataService;

        public UserProfileController(
            IAuthenticationService authenticationService,
            IUserProfileDataService profileDataService)
        {
            _authenticationService = authenticationService;
            _profileDataService = profileDataService;
        }

        [HttpGet]
        [Route("{profileId}")]
        public async Task<IActionResult> GetUserProfileData([FromRoute] Guid profileId)
        {
            try
            {
                var profileData = await _profileDataService.GetById(profileId);
                
                return Ok(profileData);
            }
            catch (ArgumentException argE)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Errors = argE
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserProfileData()
        {
            try
            {
                var curr = await _authenticationService.GetCurrentUser();
                var profileData = await _profileDataService.Create(curr);
                return Ok(profileData);
            }
            catch (ArgumentException argE)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Errors = argE
                });
            }
        }
    }
}
