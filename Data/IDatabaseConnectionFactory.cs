using System.Data;

namespace DialysisManagement.Data
{
    /// <summary>
    /// Factory per creare connessioni al database
    /// </summary>
    public interface IDatabaseConnectionFactory
    {
        /// <summary>
        /// Crea una nuova connessione al database
        /// </summary>
        IDbConnection CreateConnection();

        /// <summary>
        /// Ottiene la stringa di connessione
        /// </summary>
        string GetConnectionString();
    }
}
