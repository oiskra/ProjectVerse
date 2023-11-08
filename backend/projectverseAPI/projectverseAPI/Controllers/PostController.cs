using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
