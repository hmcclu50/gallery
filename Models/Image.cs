using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Image
{
    public int Id { get; set; }
    public int Position { get; set; }
    public int? Forward { get; set; }
    public int? Backward { get; set; }
    public int? Left { get; set; }
    public int? Right { get; set; }
    [ValidateNever]
    public string ImageName { get; set; }
    [NotMapped, ValidateNever]
    public IFormFile ImageFile { get; set; }
    [ValidateNever]
    public Exhibit Exhibit { get; set; }
}