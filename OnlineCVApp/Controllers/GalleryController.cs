using Microsoft.AspNetCore.Mvc;
using OnlineCVApp.Data;
using OnlineCVApp.Models;

namespace OnlineCVApp.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GalleryController(ApplicationDbContext context)
        {
            _context = context;
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
            if (image != null && image.Length > 0)
            {
                using var stream = new MemoryStream();
                await image.CopyToAsync(stream);

                var galleryImage = new GalleryImage
                {
                    FileName = image.FileName,
                    ContentType = image.ContentType,
                    ImageData = stream.ToArray()
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

            return File(image.ImageData, image.ContentType);
        }
    }
}
