using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Exhibit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }
    [ValidateNever]
    public string? ThumbnailName { get; set; }
    [NotMapped, ValidateNever]
    public IFormFile? ThumbnailFile { get; set; }
    
    public ICollection<Image>? Images { get; set; }
}