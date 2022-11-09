using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Fact
{
    public int Id { get; set; }
    
    [Range(1, Int32.MaxValue)]
    public int ArtistId { get; set; }
    
    [ValidateNever]
    public Artist Artist { get; set; }
    
    [Range(1, Int32.MaxValue)]
    public int ArtworkId { get; set; }
    [ValidateNever]
    public Artwork Artwork { get; set; }
    
    [Range(1, Int32.MaxValue)]
    public int ExhibitId { get; set; }
    [ValidateNever]
    public Exhibit Exhibit { get; set; }
}