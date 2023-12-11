using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.User;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers([FromQuery] string? searchTerm)
        {
            var users = await _userService.GetAll();
            var mapped = _mapper.Map<List<UserResponseDTO>>(users);

            return Ok(mapped);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userService.GetById(userId);
                var mapped = _mapper.Map<UserResponseDTO>(user);

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

        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> UpdateUser([FromRoute] Guid userId, [FromBody] UpdateUserRequestDTO userDTO)
        {
            try
            {
                if (userId != userDTO.Id)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                var updatedUser = await _userService.Update(userDTO);
                var mapped = _mapper.Map<UserResponseDTO>(updatedUser);

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

        [HttpPatch]
        [Route("{userId}/ban")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BanUser([FromRoute] Guid userId)
        {
            return BadRequest("Not implemented.");
        }

        [HttpPatch]
        [Route("{userId}/unban")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnbanUser([FromRoute] Guid userId)
        {
            return BadRequest("Not implemented.");
        }
    }
}
