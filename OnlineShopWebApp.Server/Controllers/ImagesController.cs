using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Models.Domain;
using OnlineShop.API.Models.DTO;
using OnlineShop.API.Repositories;

namespace OnlineShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;
        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request)
        {
            ValidateUpload(request);

            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await _imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateUpload(ImageUploadRequestDTO request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
           if(allowedExtensions.Contains(Path.GetExtension(request.File.FileName).ToLower()) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extention");
                throw new Exception("Invalid file extension");
            }
           if( request.File.Length > 10 * 1024)
            {
                ModelState.AddModelError("file", "File size is too big");
            }
        }
    }
}
