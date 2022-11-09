using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Image
{
    public int Id { get; set; }
    
    [Range(0,1)]
    public int IsStart { get; set; }
    
    [Range(1,Int32.MaxValue)]
    public int? NorthId { get; set; }
    
    [Range(1,Int32.MaxValue)]
    public int? SouthId { get; set; }
    
    [Range(1,Int32.MaxValue)]
    public int? EastId { get; set; }
    
    [Range(1,Int32.MaxValue)]
    public int? WestId { get; set; }
    
    [ValidateNever]
    public string ImageName { get; set; }
    [NotMapped, ValidateNever]
    public IFormFile ImageFile { get; set; }
    [ValidateNever]
    public Exhibit Exhibit { get; set; }
}