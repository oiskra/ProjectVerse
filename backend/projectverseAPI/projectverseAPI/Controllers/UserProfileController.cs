using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.UserProfileData;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using System.Runtime.CompilerServices;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserProfileDataService _profileDataService;

        public UserProfileController(
            IMapper mapper,
            IAuthenticationService authenticationService,
            IUserProfileDataService profileDataService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _profileDataService = profileDataService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserProfileData([FromRoute] Guid userId)
        {
            try
            {
                var profileData = await _profileDataService.GetById(userId);
                var profileDataResponse = _mapper.Map<UserProfileDataResponseDTO>(profileData);
                
                return Ok(profileDataResponse);
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

        [HttpPut]
        [Route("{profileId}")]
        public async Task<IActionResult> UpdateUserProfileData(
            [FromRoute] Guid profileId,
            [FromBody] UpdateUserProfileDataRequestDTO dto)
        {
            try
            {
                if (profileId != dto.Id)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                var updatedProfileData = await _profileDataService.Update(dto);
                var updatedProfileDataResponse = _mapper.Map<UserProfileDataResponseDTO>(updatedProfileData);

                return Ok(updatedProfileDataResponse);
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
            // ----------------- TESTING ONLY --------------------
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
