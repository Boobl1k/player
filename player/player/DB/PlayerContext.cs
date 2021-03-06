using Microsoft.EntityFrameworkCore;

namespace player.DB;

public class PlayerContext : BaseDbContext
{
    protected override string Catalog => Config.PlayerCatalog;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<LikedTrack> LikedTracks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
