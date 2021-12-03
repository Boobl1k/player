using Npgsql;
using System.Data;

namespace player.Backend
{
    public static class Authentication
    {
        public static bool IsCorrect(NpgsqlConnection connection, string login, string password)
        {
            connection.Open();
            var command = new NpgsqlCommand($"select * from AuthUsers where login = \'{login}\' and password = \'{password}\'", connection);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());
            connection.Close();
            return (dt.Rows.Count == 0);
        }
    }
}
