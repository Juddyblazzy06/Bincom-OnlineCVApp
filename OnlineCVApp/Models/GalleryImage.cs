using System.ComponentModel.DataAnnotations;

namespace OnlineCVApp.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required]
        public string? FileName { get; set; }

        public string? ContentType { get; set; }

        public byte[] ImageData { get; set; } = null!;
    }
}
