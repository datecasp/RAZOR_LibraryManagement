using System.Net;
using Microsoft.AspNetCore.Mvc;
using RAZOR_LibraryManagement.Domain.Interfaces;

namespace RAZOR_LibraryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            var imageUrl = await _imageService.UploadImageAsyncService(file);
            if(imageUrl == null)
            {
                return Problem("Someething went wrong", null, (int)HttpStatusCode.InternalServerError);
            }
            return Json(new { link = imageUrl });
        }
    }
}
