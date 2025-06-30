using System.ComponentModel.DataAnnotations;

namespace OnlineCVApp.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string ContentType { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string PublicId { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
