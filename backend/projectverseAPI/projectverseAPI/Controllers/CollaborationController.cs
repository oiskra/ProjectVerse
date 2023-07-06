using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs.Collaboration;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("collaborations")]
    [Produces("application/json")]
    public class CollaborationController : ControllerBase
    {
        private readonly ICollaborationService _collaborationService;
        private readonly IMapper _mapper;

        public CollaborationController(ICollaborationService collaborationService, IMapper mapper)
        {
            _collaborationService = collaborationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IList<CollaborationDTO>>> GetAllCollabortions() 
        {
            var collaborations = await _collaborationService.GetAllCollaborations();

            var collaborationsResponse = collaborations.Select(c => _mapper.Map<CollaborationDTO>(c));

            return Ok(collaborationsResponse);
        }

        [HttpGet]
        [Route("{collaborationId}")]
        public async Task<ActionResult<CollaborationDTO>> GetCollaborationById([FromRoute] Guid collaborationId)
        {
            var collaboration = await _collaborationService.GetCollaborationById(collaborationId);

            if(collaboration is null) { return NotFound(); }

            var collaborationResponse = _mapper.Map<CollaborationDTO>(collaboration);

            return Ok(collaborationResponse);
        }

        [HttpPost]
        public async Task<ActionResult<CollaborationDTO>> CreateCollaboration([FromBody] CreateCollaborationDTO createCollaborationDTO)
        {
            var collaboration = _mapper.Map<Collaboration>(createCollaborationDTO);
            var createdCollaboration = await _collaborationService.CreateCollaboration(collaboration);

            var collaborationResponse = _mapper.Map<CollaborationDTO>(createdCollaboration);
            
            return Created("CreateCollaboration", collaborationResponse);
        }

        [HttpPut]
        [Route("{collaborationId}")]
        public async Task<IActionResult> UpdateCollaboration([FromRoute] Guid collaborationId, [FromBody] UpdateCollaborationDTO updateCollaborationDTO)
        {
            if(collaborationId != updateCollaborationDTO.Id) { return BadRequest(); }
            
            var collaboration = _mapper.Map<Collaboration>(updateCollaborationDTO);
            await _collaborationService.UpdateCollaboration(collaboration);

            return Ok();
        }

        [HttpDelete]
        [Route("{collaborationId}")]
        public async Task<IActionResult> DeleteCollaboration([FromRoute] Guid collaborationId)
        {
            await _collaborationService.DeleteCollaborationById(collaborationId);
            
            return NoContent();
        }
    }
}
