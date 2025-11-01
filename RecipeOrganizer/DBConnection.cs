using MySql.Data.MySqlClient;

namespace RecipeOrganizer
{
    public class DBConnection
    {
        private MySqlConnection connection;
        private string connectionString;

        public DBConnection()
        {
          
            string server = "localhost";
            string database = "recipe_db"; 
            string user = "root";
            string password = "0930"; 

            connectionString = $"Server={server};Database={database};User ID={user};Password={password};";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
