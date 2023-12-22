using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.Constants;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Designer;
using projectverseAPI.DTOs.Projects;
using projectverseAPI.DTOs.UserProfileData;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/profiles")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;
        private readonly IUserProfileDataService _profileDataService;
        private readonly IProfileDesignerService _profileDesignerService;

        public UserProfileController(
            IMapper mapper,
            IProjectService projectService,
            IUserProfileDataService profileDataService,
            IProfileDesignerService profileDesignerService)
        {
            _mapper = mapper;
            _projectService = projectService;
            _profileDataService = profileDataService;
            _profileDesignerService = profileDesignerService;
        }

        [HttpGet]
        [Route("{userId}/data")]
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

        [HttpGet]
        [Route("{userId}/designer")]
        public async Task<IActionResult> GetProfileDesigner([FromRoute] Guid userId)
        {
            try
            {
                var designer = await _profileDesignerService.GetById(userId);

                var designerResponse = _mapper.Map<ProfileDesignerResponseDTO>(designer);

                return Ok(designerResponse);
            }
            catch (ArgumentException)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Errors = null
                });
            }
        }

        [HttpPut]
        [Route("{userId}/designer/{designerId}")]
        [Authorize(Policy = PolicyNameConstants.UserPersonalAccessPolicy)]
        public async Task<IActionResult> UpdateProfileDesigner(
            [FromRoute] Guid userId,
            [FromRoute] Guid designerId,
            [FromBody] UpdateProfileDesignerRequestDTO dto)
        {
            try
            {
                if (designerId != dto.Id && userId != dto.UserId)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                var updatedDesigner = await _profileDesignerService.Update(dto);
                var updatedDesignerResponse = _mapper.Map<ProfileDesignerResponseDTO>(updatedDesigner);

                return Ok(updatedDesignerResponse);
            }
            catch (ArgumentException)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Errors = null
                });
            }
        }

        [HttpPut]
        [Route("{profileId}")]
        [Authorize(Policy = PolicyNameConstants.UserPersonalAccessPolicy)]
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
    }
}
