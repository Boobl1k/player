using Npgsql;

namespace player.Backend
{
    public static class Connector
    {
        private static NpgsqlConnection connection = new NpgsqlConnection("Server=localhost;Port=5444;User Id=postgres;Password=1;Database=playerDb");

        public static bool Authenticate(string login, string password) => 
            Authentication.IsCorrect(connection, login, password);

        public static bool Register(string login, string password) =>
            Registration.RegisterNew(connection, login, password);
    }
}
