using Microsoft.AspNetCore.Mvc;
using projectverseAPI.Interfaces;
using System.Net;
using System.Net.Mime;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(
            IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("users/{userId}/profile-image")]
        public async Task<IActionResult> UploadUsersProfileImage(
            [FromRoute] Guid userId,
            IFormFile file)
        {
            await _imageService.UploadUsersProfileImage(userId, file);

            return Ok();
        }

        [HttpGet]
        [Route("users/{userId}/profile-image")]
        public async Task<IActionResult> GetUsersProfileImage([FromRoute] Guid userId)
        {
            var response = await _imageService.GetUsersProfileImage(userId);

            return File(
                response.ResponseStream, 
                response.Headers.ContentType);
        }

    }
}
