using Microsoft.EntityFrameworkCore;
using OnlineCVApp.Models;

namespace OnlineCVApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<GalleryImage> GalleryImages { get; set; }
    }
}