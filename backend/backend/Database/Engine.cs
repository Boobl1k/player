using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace backend.Database
{
    public class SelectResult
    { 
        
        public int ColumnsCount { get; set; }
        public int RowCount => Data.Count;
        public readonly List<List<object>> Data;
        public readonly List<string> Header;

        public SelectResult(int rowCount, List<List<object>> list, List<string> header)
        {
            ColumnsCount = rowCount;
            Data = list;
            Header = header;
        }
    }
    
    public static class Engine
    {
        private static string connectionString = "Server=localhost;Database=DownChecker;Trusted_Connection=True;";

        public static void CheckConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");

                // Вывод информации о подключении
                Console.WriteLine("Свойства подключения:");
                Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                Console.WriteLine("\tБаза данных: {0}", connection.Database);
                Console.WriteLine("\tСервер: {0}", connection.DataSource);
                Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                Console.WriteLine("\tСостояние: {0}", connection.State);
                Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
            }

            Console.WriteLine("Подключение закрыто...");
        }

        public static SelectResult Select(string sqlExpression)
        {
            var result = new SelectResult(0, new List<List<object>>(), new List<string>());
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    result.ColumnsCount = reader.FieldCount;
                    
                    for (int i = 0; i < result.ColumnsCount; i++)
                    {
                        result.Header.Add(reader.GetName(i));
                    }
                    
                    while (reader.Read())
                    {
                        var tempRow = new List<object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tempRow.Add(reader.GetValue(i));
                        }
                        result.Data.Add(tempRow);
                    }
                }
                reader.Close();
            }

            return result;
        }

        public static void Insert(string sqlExpression) => NonQueryCommand(sqlExpression, "Добавлено объектов");

        public static void Update(string sqlExpression) => NonQueryCommand(sqlExpression, "Обновлено объектов");

        public static void Delete(string sqlExpression) => NonQueryCommand(sqlExpression, "Удалено объектов");

        private static void NonQueryCommand(string sqlExpression, string logMessage)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                Console.WriteLine($"{logMessage}: {number}");
            }
        }
    }
}