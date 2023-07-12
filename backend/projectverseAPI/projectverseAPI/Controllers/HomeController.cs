using Microsoft.AspNetCore.Mvc;
using projectverseAPI.Data;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : Controller
    {
        [HttpGet(Name = "GetTest")]
        public IActionResult Index()
        {
            return Ok("home");
        }
    }
}
