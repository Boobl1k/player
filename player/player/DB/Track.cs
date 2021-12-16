using System.ComponentModel.DataAnnotations;

namespace player.DB;

public class Track
{
    public int Id { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int TimeInSeconds { get; set; }
}
