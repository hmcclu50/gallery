using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace farris_art_gallery.Models;

public class Fact
{
    public int Id { get; set; }
    
    public int ArtistId { get; set; }
    [ValidateNever]
    public Artist Artist { get; set; }
    
    public int ArtworkId { get; set; }
    [ValidateNever]
    public Artwork Artwork { get; set; }
    
    public int ExhibitId { get; set; }
    [ValidateNever]
    public Exhibit Exhibit { get; set; }
}