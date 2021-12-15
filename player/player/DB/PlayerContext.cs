using Microsoft.EntityFrameworkCore;

namespace player.DB;

public class PlayerContext : BaseDbContext
{
    protected override string Catalog => "player";
    
    public DbSet<User> Users { get; set; }
}
