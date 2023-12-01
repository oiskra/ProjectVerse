using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.Constants;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/collaborations")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CollaborationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ICollaborationService _collaborationService;
        private readonly ICollaborationApplicantsService _collaborationApplicantsService;
        private readonly IMapper _mapper;

        public CollaborationController(
            IAuthorizationService authorizationService,
            ICollaborationService collaborationService,
            ICollaborationApplicantsService collaborationApplicantsService,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _collaborationService = collaborationService;
            _collaborationApplicantsService = collaborationApplicantsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateResponseDTO>> CreateCollaboration([FromBody] CreateCollaborationRequestDTO createCollaborationDTO)
        {
            var createdCollaborationId = await _collaborationService.CreateCollaboration(createCollaborationDTO);
            return CreatedAtAction(
                nameof(CreateCollaboration), 
                new CreateResponseDTO { Id = createdCollaborationId });
        }

        [HttpGet]
        public async Task<ActionResult<IList<CollaborationResponseDTO>>> GetAllCollabortions(
            [FromQuery] Guid? userId)
        {
            var collaborations = userId is null 
                ? await _collaborationService.GetAllCollaborations()
                : await _collaborationService.GetAllCollaborationsByUserId((Guid)userId);
            var collaborationsResponse = _mapper.Map<List<CollaborationResponseDTO>>(collaborations);

            return Ok(collaborationsResponse);
        }

        [HttpGet]
        [Route("{collaborationId}")]
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

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
            if (!authorizationResult.Succeeded)
                return Forbid();

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

                var collaboration = await _collaborationService.GetCollaborationById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

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
        }

        [HttpDelete]
        [Route("{collaborationId}")]
        public async Task<ActionResult> DeleteCollaboration([FromRoute] Guid collaborationId)
        {
            try
            {
                var collaboration = await _collaborationService.GetCollaborationById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

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
        }

        [HttpPatch]
        [Route("{collaborationId}/applicants/{applicantId}/change-application-status")]
        public async Task<IActionResult> ChangeApplicationStatus(
            [FromRoute] Guid collaborationId,
            [FromRoute] Guid applicantId,
            [FromBody] ChangeApplicationStatusRequestDTO applicationStateRequestDTO)
        {
            try
            {
                var collaboration = await _collaborationService.GetCollaborationById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

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
        }

        [HttpDelete]
        [Route("{collaborationId}/applicants/{applicantId}")]
        public async Task<IActionResult> DeleteApplicantById([FromRoute] Guid collaborationId, [FromRoute] Guid applicantId)
        {
            try
            {
                var collaboration = await _collaborationService.GetCollaborationById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _collaborationApplicantsService.RemoveApplicantForCollaboration(applicantId);

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

        [HttpPost]
        [Route("{collaborationId}/collaboration-positions/")]
        public async Task<ActionResult<CreateResponseDTO>> AddCollaborationPosition(
            [FromRoute] Guid collaborationId,
            [FromBody] CreateCollaborationPositionDTO collaborationPositionDTO)
        {
            try
            {
                var collaboration = await _collaborationService.GetCollaborationById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                var createdId = await _collaborationService.AddCollaborationPosition(collaborationId, collaborationPositionDTO);

                return CreatedAtAction(
                    nameof(AddCollaborationPosition),
                    new CreateResponseDTO { Id = createdId });
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

        [HttpDelete]
        [Route("{collaborationId}/collaboration-positions/{collaborationPositionId}")]
        public async Task<IActionResult> DeleteCollaborationPosition(
            [FromRoute] Guid collaborationId,
            [FromRoute] Guid collaborationPositionId)
        {
            try
            {
                var collaboration = await _collaborationService.GetCollaborationById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _collaborationService.DeleteCollaborationPositionById(collaborationId, collaborationPositionId);

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
