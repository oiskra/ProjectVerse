using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs;
using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces;
using projectverseAPI.Models;
using System.Runtime.CompilerServices;

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

        [HttpDelete]
        [Route("{projectId}")]
        public async Task<IActionResult> DeletePost([FromRoute] Guid projectId)
        {
            try
            {
                await _postService.DeletePost(projectId);

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

        [HttpPatch]
        [Route("{postId}/views")]
        public async Task<IActionResult> RecordPostView([FromRoute] Guid postId)
        {
            try
            {
                await _postService.RecordPostView(postId);

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
        [Route("{postId}/comments")]
        public async Task<ActionResult<List<PostCommentDTO>>> GetAllPostCommentsFromPost([FromRoute] Guid postId)
        {
            try
            {
                var comments = await _postService.GetAllPostCommentsFromPost(postId);
                var commentsResponse = comments.Select(c => _mapper.Map<PostCommentDTO>(c));

                return Ok(commentsResponse);
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
        [Route("{postId}/comments")]
        public async Task<ActionResult<CreateResponseDTO>> CreatePostComment(Guid postId, CreatePostCommentRequestDTO createPostCommentDTO)
        {
            try
            {
                var createdId = await _postService.CreatePostComment(postId, createPostCommentDTO);

                return CreatedAtAction(
                    "CreatePostComment",
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
        [Route("comments/{commentId}")]
        public async Task<IActionResult> DeletePostComment([FromRoute] Guid commentId)
        {
            try
            {
                await _postService.DeletePostComment(commentId);

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

        [HttpPut]
        [Route("{commentId}")]
        public async Task<IActionResult> UpdatePostComment([FromRoute] Guid commentId, UpdatePostCommentRequestDTO updatePostDTO)
        {
            try
            {
                if (commentId != updatePostDTO.Id)
                    return BadRequest(new ErrorResponseDTO
                    {
                        Title = "Bad Request",
                        Status = StatusCodes.Status400BadRequest,
                        Errors = new
                        {
                            Id = new List<string> { "Route id and object id don't match." }
                        }
                    });

                await _postService.UpdatePostComment(updatePostDTO);
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
