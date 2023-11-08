using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(
            IPostService postService,
            IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateResponseDTO>> CreatePost([FromBody] CreatePostRequestDTO createPostDTO)
        {
            try
            {
                var createdPostId = await _postService.CreatePost(createPostDTO.ProjectId);

                return CreatedAtAction(
                    "CreatePost", 
                    new CreateResponseDTO { Id = createdPostId });
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
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            var postsResponse = posts.Select(p => _mapper.Map<PostResponseDTO>(p));

            return Ok(postsResponse);
        }
    }
}
