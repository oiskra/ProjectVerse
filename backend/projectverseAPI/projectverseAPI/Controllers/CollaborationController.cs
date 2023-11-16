using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/collaborations")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationService _collaborationService;
        private readonly ICollaborationApplicantsService _collaborationApplicantsService;
        private readonly IMapper _mapper;

        public CollaborationController(
            ICollaborationService collaborationService,
            ICollaborationApplicantsService collaborationApplicantsService,
            IMapper mapper)
        {
            _collaborationService = collaborationService;
            _collaborationApplicantsService = collaborationApplicantsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateResponseDTO>> CreateCollaboration([FromBody] CreateCollaborationRequestDTO createCollaborationDTO)
        {
            try
            {
                var createdCollaborationId = await _collaborationService.CreateCollaboration(createCollaborationDTO);
                return CreatedAtAction("CreateCollaboration", new CreateResponseDTO { Id = createdCollaborationId });
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

        [HttpGet]
        public async Task<ActionResult<IList<CollaborationResponseDTO>>> GetAllCollabortions()
        {
            var collaborations = await _collaborationService.GetAllCollaborations();

            var collaborationsResponse = collaborations.Select(c => _mapper.Map<CollaborationResponseDTO>(c));

            return Ok(collaborationsResponse);
        }

        [HttpGet]
        [Route("{collaborationId}")]
        [Authorize(Policy = "CollaborationOwner")]
        public async Task<ActionResult<SingleCollaborationWithApplicantsResponseDTO>> GetCollaborationById([FromRoute] Guid collaborationId)
        {
            var collaboration = await _collaborationService.GetCollaborationById(collaborationId);

            if (collaboration is null)  
                return NotFound(new ErrorResponseDTO 
                { 
                    Title = "Not Found", 
                    Status = StatusCodes.Status404NotFound, 
                    Errors = null 
                });

            var collaborationResponse = _mapper.Map<SingleCollaborationWithApplicantsResponseDTO>(collaboration);

            return Ok(collaborationResponse);
        }

        [HttpPut]
        [Route("{collaborationId}")]
        public async Task<ActionResult> UpdateCollaboration([FromRoute] Guid collaborationId, [FromBody] UpdateCollaborationRequestDTO updateCollaborationDTO)
        {
            try
            {
                if (collaborationId != updateCollaborationDTO.Id)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                await _collaborationService.UpdateCollaboration(updateCollaborationDTO);

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

        [HttpDelete]
        [Route("{collaborationId}")]
        public async Task<ActionResult> DeleteCollaboration([FromRoute] Guid collaborationId)
        {
            try
            {
                await _collaborationService.DeleteCollaborationById(collaborationId);

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

        [HttpPost]
        [Route("{collaborationId}/collaboration-positions/{collaborationPositionId}/apply")]
        public async Task<ActionResult> Apply([FromRoute] Guid collaborationId, [FromRoute] Guid collaborationPositionId)
        {
            try
            {
                var createdApplicantId = await _collaborationApplicantsService.ApplyForCollaboration(collaborationId, collaborationPositionId);

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

        [HttpPatch]
        [Authorize(Policy = "CollaborationOwner")]
        [Route("applicants/{applicantId}/change-application-status")]
        public async Task<IActionResult> ChangeApplicationStatus(
            [FromRoute] Guid applicantId,
            [FromBody] ChangeApplicationStatusRequestDTO applicationStateRequestDTO)
        {
            try
            {
                await _collaborationApplicantsService.ChangeApplicationStatus(applicantId, applicationStateRequestDTO);

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
        [Route("applicants/{applicantId}")]
        [Authorize(Policy = "CollaborationOwner")]
        public async Task<IActionResult> DeleteApplicantById([FromRoute] Guid applicantId)
        {
            try
            {
                await _collaborationApplicantsService.RemoveApplicantForCollaboration(applicantId);

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
