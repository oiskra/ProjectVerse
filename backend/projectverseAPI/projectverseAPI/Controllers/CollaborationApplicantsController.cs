using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.DTOs;
using projectverseAPI.Interfaces;
using AutoMapper;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/applicants")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CollaborationApplicantsController : ControllerBase
    {
        private readonly ICollaborationApplicantsService _applicantsService;
        private readonly IMapper _mapper;

        public CollaborationApplicantsController(
            ICollaborationApplicantsService applicantsService,
            IMapper mapper)
        {
            _applicantsService = applicantsService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("{collaborationId}/collaborationPositions/{collaborationPositionId}")]
        public async Task<ActionResult> Apply([FromRoute] Guid collaborationId, [FromRoute] Guid collaborationPositionId)
        {
            try
            {
                var createdApplicantId = await _applicantsService.ApplyForCollaboration(collaborationId, collaborationPositionId);

                return CreatedAtAction("Apply", new CreateResponseDTO { Id = createdApplicantId });
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
            catch (InvalidOperationException e)
            {
                return BadRequest(new ErrorResponseDTO
                {
                    Title = "Bad Request",
                    Status = StatusCodes.Status400BadRequest,
                    Errors = e.Message
                });
            }
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDTO
                    {
                        Title = "Internal Server Error",
                        Status = StatusCodes.Status500InternalServerError,
                        Errors = e.Message
                    });
            }
        }

        [HttpGet]
        [Route("{collaborationId}")]
        [Authorize(Policy = "CollaborationOwner")]
        public async Task<ActionResult> GetCollaborationApplicants([FromRoute] Guid collaborationId)
        {
            var collaborationApplicants = await _applicantsService.GetCollaborationApplicants(collaborationId);

            var collaborationApplicantsResponse = collaborationApplicants.Select(a => _mapper.Map<CollaborationApplicantDTO>(a));

            return Ok(collaborationApplicantsResponse);
        }

        [HttpPatch]
        [Route("{applicantId}/accept")]
        public async Task<IActionResult> AcceptApplication([FromRoute] Guid applicantId)
        {
            try
            {
                await _applicantsService.AcceptApplicant(applicantId);

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
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDTO
                    {
                        Title = "Internal Server Error",
                        Status = StatusCodes.Status500InternalServerError,
                        Errors = e.Message
                    });
            }
        }

        [HttpDelete]
        [Route("{applicantId}")]
        [Authorize(Policy = "CollaborationOwner")]
        public async Task<IActionResult> DeleteApplicantById([FromRoute] Guid applicantId)
        {
            try
            {
                await _applicantsService.RemoveApplicantForCollaboration(applicantId);

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
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDTO
                    {
                        Title = "Internal Server Error",
                        Status = 500,
                        Errors = null
                    });
            }
        }
    }
}
