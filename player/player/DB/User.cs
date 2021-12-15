using System.ComponentModel.DataAnnotations;

namespace player.DB;

// ReSharper disable once ClassNeverInstantiated.Global
public class User
{
    public int Id { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}
