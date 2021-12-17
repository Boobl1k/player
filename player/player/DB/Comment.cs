using System.ComponentModel.DataAnnotations;

namespace player.DB;

public class Comment
{
    [Required] public int Id { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public string Text { get; set; }
}
