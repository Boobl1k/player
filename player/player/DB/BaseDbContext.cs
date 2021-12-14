using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace player.DB;

public abstract class BaseDbContext<T> : DbContext, IDbContext<T> where T : class
{
    protected abstract string Catalog { get; }
    private string ConnectionString => $"Data Source=localhost;Initial Catalog={Catalog};Integrated Security=True";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(config => config.AddConsole()));
        optionsBuilder.UseSqlServer(ConnectionString);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<T> Items { get; set; }

    public new void SaveChanges() => base.SaveChanges();
}
