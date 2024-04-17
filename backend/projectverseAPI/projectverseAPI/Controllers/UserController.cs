﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.Constants;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.User;
using projectverseAPI.Interfaces;

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
        [AllowAnonymous]
        public async Task<ActionResult<List<UserResponseDTO>>> GetAllUsers([FromQuery] string? searchTerm)
        {
            var users = await _userService.GetAll(searchTerm);
            var usersResponse = _mapper.Map<List<UserResponseDTO>>(users);

            return Ok(usersResponse);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserResponseDTO>> GetUser([FromRoute] Guid userId)
        {
            try
            {
                var user = await _userService.GetById(userId);
                var userResponse = _mapper.Map<UserResponseDTO>(user);

                return Ok(userResponse);
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
        [Authorize(Policy = PolicyNameConstants.UserPersonalAccessPolicy)]
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
                var userResponse = _mapper.Map<UserResponseDTO>(updatedUser);

                return Ok(userResponse);
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
