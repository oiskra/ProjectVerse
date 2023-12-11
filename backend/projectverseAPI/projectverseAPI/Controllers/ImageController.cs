using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System;
using projectverseAPI.Interfaces;

namespace projectverseAPI.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(
            IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadUsersAvatar(IFormFile file)
        {
            var createdAvatar = await _imageService.UploadUsersAvatar(file);

            return File(createdAvatar, file.ContentType);
        }

    }
}
