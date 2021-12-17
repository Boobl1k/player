using System.ComponentModel.DataAnnotations;

namespace player.DB;

public class LikedTrack
{
    [Required] public int Id { get; set; }
    [Required] public int UserId { get; set; }
    [Required] public int TrackId { get; set; }
}
