using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Projects;
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
        private readonly IProjectService _projectService;
        private readonly IUserProfileDataService _profileDataService;

        public UserProfileController(
            IMapper mapper,
            IAuthenticationService authenticationService,
            IProjectService projectService,
            IUserProfileDataService profileDataService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _projectService = projectService;
            _profileDataService = profileDataService;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUserProfileData([FromRoute] Guid userId)
        {
            try
            {
                var usersProjects = await _projectService.GetAllByUserId(userId);
                var projectsResponse = _mapper.Map<List<ProjectResponseDTO>>(usersProjects);

                var profileData = await _profileDataService.GetById(userId);
                var profileDataResponse = _mapper.Map<UserProfileData, UserProfileDataResponseDTO>(
                    profileData,
                    opt => opt.AfterMap((src, dest) => dest.Projects = projectsResponse));
                
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
