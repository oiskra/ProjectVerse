using Microsoft.AspNetCore.Mvc;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet(Name = "GetTest")]
        public IActionResult Index()
        {
            return Ok("Test");
        }
    }
}
