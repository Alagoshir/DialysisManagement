using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using NLog;

namespace DialysisManagement.Repositories
{
    /// <summary>
    /// Classe base per i repository con Dapper
    /// </summary>
    /// <typeparam name="T">Tipo entità</typeparam>
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly IDatabaseConnectionFactory ConnectionFactory;
        protected readonly Logger Logger;
        protected readonly string TableName;

        protected BaseRepository(IDatabaseConnectionFactory connectionFactory, string tableName)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Esegue una query e restituisce una lista di entità
        /// </summary>
        protected async Task<IEnumerable<T>> QueryAsync(string sql, object param = null)
        {
            try
            {
                using var connection = ConnectionFactory.CreateConnection();
                return await connection.QueryAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Errore durante QueryAsync: {sql}");
                throw;
            }
        }

        /// <summary>
        /// Esegue una query e restituisce una singola entità
        /// </summary>
        protected async Task<T> QueryFirstOrDefaultAsync(string sql, object param = null)
        {
            try
            {
                using var connection = ConnectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Errore durante QueryFirstOrDefaultAsync: {sql}");
                throw;
            }
        }

        /// <summary>
        /// Esegue un comando (INSERT, UPDATE, DELETE)
        /// </summary>
        protected async Task<int> ExecuteAsync(string sql, object param = null)
        {
            try
            {
                using var connection = ConnectionFactory.CreateConnection();
                return await connection.ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Errore durante ExecuteAsync: {sql}");
                throw;
            }
        }

        /// <summary>
        /// Esegue una query scalare (COUNT, SUM, etc.)
        /// </summary>
        protected async Task<TResult> ExecuteScalarAsync<TResult>(string sql, object param = null)
        {
            try
            {
                using var connection = ConnectionFactory.CreateConnection();
                return await connection.ExecuteScalarAsync<TResult>(sql, param);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Errore durante ExecuteScalarAsync: {sql}");
                throw;
            }
        }

        /// <summary>
        /// Esegue operazioni multiple in una transazione
        /// </summary>
        protected async Task<bool> ExecuteInTransactionAsync(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using var connection = ConnectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                await action(connection, transaction);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Errore durante la transazione, rollback in corso");
                transaction.Rollback();
                throw;
            }
        }
    }
}
