using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using farris_art_gallery.Models;

namespace farris_art_gallery.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<farris_art_gallery.Models.Exhibit>? Exhibit { get; set; }
    public DbSet<farris_art_gallery.Models.Image>? Image { get; set; }
    public DbSet<farris_art_gallery.Models.Artist>? Artist { get; set; }
    public DbSet<farris_art_gallery.Models.Artwork>? Artwork { get; set; }
    public DbSet<farris_art_gallery.Models.Fact>? Fact { get; set; }
}