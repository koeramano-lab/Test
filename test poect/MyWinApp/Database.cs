using MySql.Data.MySqlClient;

namespace MyWinApp
{
    public class Database
    {
        private readonly string connectionString =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=velsuede;" +
            "Uid=root;" +
            "Pwd=";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}