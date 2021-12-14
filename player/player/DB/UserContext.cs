namespace player.DB;

public class UserContext : BaseDbContext<User>
{
    protected override string Catalog => "player";
}
