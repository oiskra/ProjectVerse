using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using projectverseAPI.DTOs.Post;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _projectService;
        private readonly IMapper _mapper;

        public PostController(
            IPostService projectService,
            IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllPosts()
        {
            var posts = await _projectService.GetAllPosts();
            var postsResponse = posts.Select(p => _mapper.Map<PostResponseDTO>(p));

            return Ok(postsResponse);
        }
    }
}
