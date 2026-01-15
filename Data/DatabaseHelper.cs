using System;
using System.Data;
using MySql.Data.MySqlClient;
using DialysisManagement.Utilities;

namespace DialysisManagement.Data
{
    /// <summary>
    /// Helper per gestione connessioni database
    /// </summary>
    public interface IDatabaseHelper
    {
        IDbConnection GetConnection();
        bool TestConnection();
        string GetConnectionString();
    }

    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = ConfigurationHelper.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(_connectionString))
            {
                // Default connection string per MySQL Portable
                var dbPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, "Database");

                _connectionString = $"Server=localhost;Port=3306;Database=dialysis_db;" +
                    $"Uid=dialysis_user;Pwd=Dialysis@2026;SslMode=none;" +
                    $"AllowUserVariables=True;UseAffectedRows=False;";
            }
        }

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    return connection.State == ConnectionState.Open;
                }
            }
            catch
            {
                return false;
            }
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
