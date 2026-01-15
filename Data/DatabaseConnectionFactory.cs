using System;
using System.Data;
using MySql.Data.MySqlClient;
using DialysisManagement.Utilities;
using NLog;

namespace DialysisManagement.Data
{
    /// <summary>
    /// Factory per creare connessioni MySQL
    /// </summary>
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public DatabaseConnectionFactory()
        {
            _connectionString = ConfigurationHelper.GetConnectionString("DialysisDB");

            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'DialysisDB' non trovata in App.config");
            }

            Logger.Debug("DatabaseConnectionFactory inizializzato");
        }

        /// <summary>
        /// Crea una nuova connessione MySQL
        /// </summary>
        public IDbConnection CreateConnection()
        {
            try
            {
                var connection = new MySqlConnection(_connectionString);
                Logger.Trace("Nuova connessione MySQL creata");
                return connection;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Errore durante la creazione della connessione MySQL");
                throw;
            }
        }

        /// <summary>
        /// Ottiene la stringa di connessione
        /// </summary>
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
