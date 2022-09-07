using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Artwork
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [ValidateNever]
    public string ThumbnailName { get; set; }
    
    [NotMapped, ValidateNever]
    public IFormFile ThumbnailFile { get; set; }
    
    public ICollection<Fact>? Facts { get; set; }
    
}