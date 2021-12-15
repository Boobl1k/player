using Microsoft.EntityFrameworkCore;

namespace player.DB;

public class PlayerContext : BaseDbContext
{
    protected override string Catalog => Config.PlayerCatalog;
    
    public DbSet<User> Users { get; set; }
}
