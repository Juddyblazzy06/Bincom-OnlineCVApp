using Microsoft.AspNetCore.Mvc;
using OnlineCVApp.Data;
using OnlineCVApp.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net.Http.Headers;
using System.Net;

namespace OnlineCVApp.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cloudinary _cloudinary;

        public GalleryController(ApplicationDbContext context, Cloudinary cloudinary)
        {
            _context = context;
            _cloudinary = cloudinary;
        }

        public IActionResult Index()
        {
            var images = _context.GalleryImages.ToList();
            return View(images);
        }

        public IActionResult Upload() => View();

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile image)
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
                    PublicId = uploadResult.PublicId
                };

                _context.GalleryImages.Add(galleryImage);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult DisplayImage(int id)
        {
            var image = _context.GalleryImages.Find(id);
            if (image == null)
                return NotFound();

            return Redirect(image.ImageUrl);
        }
    }
}
