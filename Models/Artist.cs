namespace farris_art_gallery.Models;

public class Artist
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Biography { get; set; }
    
    public string WebsiteUrl { get; set; }
    
    public ICollection<Fact>? Facts { get; set; }
}