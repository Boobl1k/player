using Npgsql;
using System.Data;

namespace player.Backend
{
    public static class Registration
    {
        public static bool RegisterNew(NpgsqlConnection connection, string login, string password)
        {
            connection.Open();
            var command = new NpgsqlCommand($"select login from AuthUsers where login = '{login}'", connection);
            DataTable dt = new DataTable();
            dt.Load(command.ExecuteReader());

            if (dt.Rows.Count != 0)
            {
                connection.Close();
                return false;
            }

            new NpgsqlCommand($"insert into AuthUsers values('{login}', '{password}')", connection).ExecuteNonQuery();
            connection.Close();
            return true;
        }
    }
}
