using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Image
{
    public int Id { get; set; }
    [ValidateNever]
    public int Position { get; set; }
    [ValidateNever]
    public string ImageName { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    
    public Exhibit Exhibit { get; set; }
}