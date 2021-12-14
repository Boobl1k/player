using Microsoft.EntityFrameworkCore;

namespace player.DB;

public interface IDbContext<T> where T : class
{
    public DbSet<T> Items { get; set; }
    public void SaveChanges();
}
