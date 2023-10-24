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
        private readonly IMapper _mapper;

        public CollaborationController(
            ICollaborationService collaborationService,
            IMapper mapper)
        {
            _collaborationService = collaborationService;
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
        public async Task<ActionResult<CollaborationResponseDTO>> GetCollaborationById([FromRoute] Guid collaborationId)
        {
            var collaboration = await _collaborationService.GetCollaborationById(collaborationId);

            if (collaboration is null)  
                return NotFound(new ErrorResponseDTO 
                { 
                    Title = "Not Found", 
                    Status = StatusCodes.Status404NotFound, 
                    Errors = null 
                });

            var collaborationResponse = _mapper.Map<CollaborationResponseDTO>(collaboration);

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
        [Route("{collaborationId}/collaborationPositions/{collaborationPositionId}/applicants")]
        public async Task<ActionResult> Apply([FromRoute] Guid collaborationId, [FromRoute] Guid collaborationPositionId)
        {
            try
            {
                var createdApplicantId = await _collaborationService.ApplyForCollaboration(collaborationId, collaborationPositionId);
                
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
            catch (Exception e)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ErrorResponseDTO
                    {
                        Title = "Internal Server Error",
                        Status = 500,
                        Errors = e.Message
                    });
            }

        }

        [HttpGet]
        [Route("{collaborationId}/applicants")]
        public async Task<ActionResult> GetCollaborationApplicants([FromRoute] Guid collaborationId)
        {
            var collaborationApplicants = await _collaborationService.GetCollaborationApplicants(collaborationId);                                              

            var collaborationApplicantsResponse = collaborationApplicants.Select(a => _mapper.Map<CollaborationApplicantDTO>(a));

            return Ok(collaborationApplicantsResponse);
        }
    }
}
