using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Image
{
    public int Id { get; set; }
    
    public int IsStart { get; set; }
    
    public int? NorthId { get; set; }
    
    public int? SouthId { get; set; }
    
    public int? EastId { get; set; }
    
    public int? WestId { get; set; }
    
    [ValidateNever]
    public string ImageName { get; set; }
    [NotMapped, ValidateNever]
    public IFormFile ImageFile { get; set; }
    [ValidateNever]
    public Exhibit Exhibit { get; set; }
}