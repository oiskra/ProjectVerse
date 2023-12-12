using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.Constants;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Project;
using projectverseAPI.Interfaces;


namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(
            IAuthorizationService authorizationService,
            IProjectService projectService,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectResponseDTO>>> GetAllProjects([FromQuery] string? searchTerm)
        {
            var projects = await _projectService.GetAll(searchTerm);
            var projectsResponse = projects.Select(p => _mapper.Map<ProjectResponseDTO>(p));

            return Ok(projectsResponse);
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult<List<ProjectResponseDTO>>> GetAllProjectsByUserId([FromRoute] Guid userId)
        {
            var usersProjects = await _projectService.GetAllByUserId(userId);
            var projectsResponse = usersProjects.Select(p => _mapper.Map<ProjectResponseDTO>(p));

            return Ok(projectsResponse);
        }

        [HttpGet]
        [Route("{projectId}")]
        public async Task<ActionResult<ProjectResponseDTO>> GetProjectById([FromRoute] Guid projectId)
        {
            var project = await _projectService.GetById(projectId);
            
            if(project is null)
                return NotFound(new ErrorResponseDTO
                {
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Errors = null
                });

            var projectResponse = _mapper.Map<ProjectResponseDTO>(project);
            return Ok(projectResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponseDTO>> CreateProject([FromBody] CreateProjectRequestDTO projectDTO)
        {
            var createdProject = await _projectService.Create(projectDTO);

            var mapped = _mapper.Map<ProjectResponseDTO>(createdProject);

            return CreatedAtAction("CreateProject", mapped);
        }

        [HttpPut]
        [Route("{projectId}")]
        public async Task<IActionResult> UpdateProject([FromRoute] Guid projectId, [FromBody] UpdateProjectRequestDTO updateProjectDTO)
        {
            try
            {
                if (projectId != updateProjectDTO.Id)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                var project = await _projectService.GetById(projectId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, project, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _projectService.Update(updateProjectDTO);

                return NoContent();
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

        [HttpDelete]
        [Route("{projectId}")]
        public async Task<IActionResult> DeleteProject([FromRoute] Guid projectId)
        {
            try
            {
                var project = await _projectService.GetById(projectId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, project, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _projectService.Delete(projectId);

                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Title = "Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Errors = e.Message
                });
            }
        }
    }
}
