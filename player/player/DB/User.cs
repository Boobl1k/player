using System.ComponentModel.DataAnnotations.Schema;

namespace player.DB;

// ReSharper disable once ClassNeverInstantiated.Global
[Table($"{nameof(User)}s")]
public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}
