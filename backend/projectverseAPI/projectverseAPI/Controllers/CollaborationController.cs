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
        private readonly ICollaborationPositionService _collaborationPositionService;
        private readonly IMapper _mapper;

        public CollaborationController(
            IAuthorizationService authorizationService,
            ICollaborationService collaborationService,
            ICollaborationApplicantsService collaborationApplicantsService,
            ICollaborationPositionService collaborationPositionService,
            IMapper mapper)
        {
            _authorizationService = authorizationService;
            _collaborationService = collaborationService;
            _collaborationApplicantsService = collaborationApplicantsService;
            _collaborationPositionService = collaborationPositionService;
            _mapper = mapper;
        }

        #region collaborations
        [HttpPost]
        public async Task<ActionResult<CollaborationResponseDTO>> CreateCollaboration([FromBody] CreateCollaborationRequestDTO createCollaborationDTO)
        {
            var createdCollaboration = await _collaborationService.Create(createCollaborationDTO);

            var mapped = _mapper.Map<CollaborationResponseDTO>(createdCollaboration);
            return CreatedAtAction(
                nameof(CreateCollaboration), 
                mapped);
        }

        [HttpGet]
        public async Task<ActionResult<IList<CollaborationResponseDTO>>> GetAllCollabortions(
            [FromQuery] Guid? userId)
        {
            var collaborations = userId is null 
                ? await _collaborationService.GetAll()
                : await _collaborationService.GetAllByUserId((Guid)userId);
            var collaborationsResponse = _mapper.Map<List<CollaborationResponseDTO>>(collaborations);

            return Ok(collaborationsResponse);
        }

        [HttpGet]
        [Route("{collaborationId}")]
        public async Task<ActionResult<SingleCollaborationWithApplicantsResponseDTO>> GetCollaborationById([FromRoute] Guid collaborationId)
        {
            var collaboration = await _collaborationService.GetById(collaborationId);

            if (collaboration is null)  
                return NotFound(new ErrorResponseDTO 
                { 
                    Title = "Not Found", 
                    Status = StatusCodes.Status404NotFound, 
                    Errors = null 
                });

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
            if (!authorizationResult.Succeeded)
                return Forbid();

            var collaborationResponse = _mapper.Map<SingleCollaborationWithApplicantsResponseDTO>(collaboration);

            return Ok(collaborationResponse);
        }

        [HttpPut]
        [Route("{collaborationId}")]
        public async Task<ActionResult<CollaborationResponseDTO>> UpdateCollaboration([FromRoute] Guid collaborationId, [FromBody] UpdateCollaborationRequestDTO updateCollaborationDTO)
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

                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                var updated = await _collaborationService.Update(updateCollaborationDTO);
                var mapped = _mapper.Map<CollaborationResponseDTO>(updated);
                
                return Ok(mapped);
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
                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _collaborationService.Delete(collaborationId);

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
#endregion

        #region collaboration-applicants
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
                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
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
                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
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
        #endregion

        #region collaboration-positions
        [HttpPost]
        [Route("{collaborationId}/collaboration-positions/")]
        public async Task<ActionResult<CollaborationPositionDTO>> AddCollaborationPosition(
            [FromRoute] Guid collaborationId,
            [FromBody] CreateCollaborationPositionDTO collaborationPositionDTO)
        {
            try
            {
                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                var createdEntity = await _collaborationPositionService.CreateRelated(collaborationId, collaborationPositionDTO);

                var mapped = _mapper.Map<CollaborationPositionDTO>(createdEntity);

                return CreatedAtAction(
                    nameof(AddCollaborationPosition),
                    mapped);
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
                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _collaborationPositionService.Delete(collaborationPositionId);

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

        [HttpPut]
        [Route("{collaborationId}/collaboration-positions/{collaborationPositionId}")]
        public async Task<ActionResult<CollaborationPositionDTO>> UpdateCollaborationPosition(
            [FromRoute] Guid collaborationId,
            [FromRoute] Guid collaborationPositionId,
            [FromBody] UpdateCollaborationPositionDTO dto)
        {
            try
            {
                if (collaborationPositionId != dto.Id)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                var collaboration = await _collaborationService.GetById(collaborationId);
                var authorizationResult = await _authorizationService.AuthorizeAsync(User, collaboration, PolicyNameConstants.SameAuthorPolicy);
                if (!authorizationResult.Succeeded)
                    return Forbid();

                await _collaborationPositionService.Update(dto);

                return Ok();
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

        #endregion
    }
}
