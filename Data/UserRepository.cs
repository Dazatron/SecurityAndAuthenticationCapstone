using Microsoft.Data.SqlClient;

namespace SecurityAndAuthenticationCapstone.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(string username, string email)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Users (Username, Email)
                             VALUES (@username, @email)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public string? GetUser(string username)
        {
            using var connection = new SqlConnection(_connectionString);

            string query = @"SELECT Username 
                             FROM Users 
                             WHERE Username = @username";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.Read())
                return reader.GetString(0);

            return null;
        }
    }
}