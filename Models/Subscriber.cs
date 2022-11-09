using System.ComponentModel.DataAnnotations;

namespace farris_art_gallery.Models;

public class Subscriber
{
    public int Id { get; set; }
    
    [EmailAddress(ErrorMessage="Must be a valid e-mail address")]
    public string EmailAddress { get; set; }
}