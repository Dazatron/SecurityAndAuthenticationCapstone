using BCrypt.Net;
using Microsoft.Data.SqlClient;

namespace SecurityAndAuthenticationCapstone.Services
{
    public class AuthService
    {
        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool RegisterUser(string username, string email, string password, string role = "user")
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            using var connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO Users (Username, Email, PasswordHash, Role)
                             VALUES (@username, @email, @passwordHash, @role)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@passwordHash", passwordHash);
            command.Parameters.AddWithValue("@role", role);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool AuthenticateUser(string username, string password, out string role)
        {
            role = "";

            using var connection = new SqlConnection(_connectionString);

            string query = @"SELECT PasswordHash, Role FROM Users WHERE Username = @username";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return false;

            string storedHash = reader.GetString(0);
            role = reader.GetString(1);

            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}