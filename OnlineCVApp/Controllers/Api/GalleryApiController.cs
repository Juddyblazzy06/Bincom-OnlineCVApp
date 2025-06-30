using Microsoft.AspNetCore.Mvc;
using OnlineCVApp.Data;
using OnlineCVApp.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace OnlineCVApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class GalleryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Cloudinary _cloudinary;

        public GalleryApiController(ApplicationDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        /// <summary>
        /// Get all gallery images
        /// </summary>
        /// <returns>List of gallery images</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GalleryImage>), StatusCodes.Status200OK)]
        public IActionResult GetImages()
        {
            var images = _context.GalleryImages.ToList();
            return Ok(images);
        }

        /// <summary>
        /// Upload a new image to the gallery
        /// </summary>
        /// <param name="image">The image file to upload</param>
        /// <returns>The uploaded image details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(GalleryImage), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream()),
                PublicId = $"gallery/{Guid.NewGuid()}"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == HttpStatusCode.OK)
            {
                var galleryImage = new GalleryImage
                {
                    FileName = image.FileName,
                    ContentType = image.ContentType,
                    ImageUrl = uploadResult.Url.ToString(),
                    CreatedAt = DateTime.UtcNow
                };

                _context.GalleryImages.Add(galleryImage);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetImages), galleryImage);
            }

            return BadRequest("Failed to upload image");
        }
    }
}
