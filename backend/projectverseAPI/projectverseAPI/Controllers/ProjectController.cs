using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Project;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using projectverseAPI.Services;
using System.Runtime.CompilerServices;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/projects")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectController(
            IProjectService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectResponseDTO>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjects();
            var projectsResponse = projects.Select(p => _mapper.Map<ProjectResponseDTO>(p));

            return Ok(projectsResponse);
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult<List<ProjectResponseDTO>>> GetAllProjectsByUserId([FromRoute] Guid userId)
        {
            var usersProjects = await _projectService.GetAllProjectsByUserID(userId);
            var projectsResponse = usersProjects.Select(p => _mapper.Map<ProjectResponseDTO>(p));

            return Ok(projectsResponse);
        }

        [HttpGet]
        [Route("{projectId}")]
        public async Task<ActionResult<ProjectResponseDTO>> GetProjectById([FromRoute] Guid projectId)
        {
            var project = await _projectService.GetProjectById(projectId);
            
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
        public async Task<ActionResult<CreateResponseDTO>> CreateProject([FromBody] CreateProjectRequestDTO projectDTO)
        {
            var createdProjectId = await _projectService.CreateProject(projectDTO);

            return CreatedAtAction("CreateProject", new CreateResponseDTO { Id = createdProjectId });
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

                await _projectService.UpdateProject(updateProjectDTO);

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
                await _projectService.DeleteProject(projectId);

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
