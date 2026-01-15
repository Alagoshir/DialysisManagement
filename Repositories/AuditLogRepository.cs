using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;
using MySql.Data.MySqlClient;

namespace DialysisManagement.Repositories
{
    /// <summary>
    /// Repository per gestione audit log
    /// </summary>
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public AuditLogRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        /// <summary>
        /// Inserisce un nuovo record di audit log
        /// </summary>
        public async Task<int> InsertAsync(AuditLog auditLog)
        {
            if (auditLog == null)
                throw new ArgumentNullException(nameof(auditLog));

            const string sql = @"
                INSERT INTO audit_log (
                    UserId,
                    Username,
                    Action,
                    EntityType,
                    EntityId,
                    OldValues,
                    NewValues,
                    IpAddress,
                    UserAgent,
                    ActionDate
                ) VALUES (
                    @UserId,
                    @Username,
                    @Action,
                    @EntityType,
                    @EntityId,
                    @OldValues,
                    @NewValues,
                    @IpAddress,
                    @UserAgent,
                    @ActionDate
                );
                SELECT LAST_INSERT_ID();";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var id = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        auditLog.UserId,
                        auditLog.Username,
                        auditLog.Action,
                        auditLog.EntityType,
                        auditLog.EntityId,
                        auditLog.OldValues,
                        auditLog.NewValues,
                        auditLog.IpAddress,
                        auditLog.UserAgent,
                        ActionDate = auditLog.ActionDate ?? DateTime.Now
                    });

                    return id;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'inserimento del log di audit: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera log per range di date
        /// </summary>
        public async Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT 
                    LogId,
                    UserId,
                    Username,
                    Action,
                    EntityType,
                    EntityId,
                    OldValues,
                    NewValues,
                    IpAddress,
                    UserAgent,
                    ActionDate
                FROM audit_log
                WHERE ActionDate BETWEEN @StartDate AND @EndDate
                ORDER BY ActionDate DESC";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var logs = await connection.QueryAsync<AuditLog>(sql, new
                    {
                        StartDate = startDate.Date,
                        EndDate = endDate.Date.AddDays(1).AddSeconds(-1)
                    });

                    return logs;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dei log di audit: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera log per utente
        /// </summary>
        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId)
        {
            const string sql = @"
                SELECT 
                    LogId,
                    UserId,
                    Username,
                    Action,
                    EntityType,
                    EntityId,
                    OldValues,
                    NewValues,
                    IpAddress,
                    UserAgent,
                    ActionDate
                FROM audit_log
                WHERE UserId = @UserId
                ORDER BY ActionDate DESC
                LIMIT 1000";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var logs = await connection.QueryAsync<AuditLog>(sql, new { UserId = userId });
                    return logs;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dei log utente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera log per paziente
        /// </summary>
        public async Task<IEnumerable<AuditLog>> GetByPatientIdAsync(int patientId)
        {
            const string sql = @"
                SELECT 
                    LogId,
                    UserId,
                    Username,
                    Action,
                    EntityType,
                    EntityId,
                    OldValues,
                    NewValues,
                    IpAddress,
                    UserAgent,
                    ActionDate
                FROM audit_log
                WHERE EntityType = 'Patient' AND EntityId = @PatientId
                ORDER BY ActionDate DESC
                LIMIT 500";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var logs = await connection.QueryAsync<AuditLog>(sql, new { PatientId = patientId });
                    return logs;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dei log paziente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera log per tipo azione
        /// </summary>
        public async Task<IEnumerable<AuditLog>> GetByActionAsync(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
                throw new ArgumentException("L'azione non può essere vuota", nameof(action));

            const string sql = @"
                SELECT 
                    LogId,
                    UserId,
                    Username,
                    Action,
                    EntityType,
                    EntityId,
                    OldValues,
                    NewValues,
                    IpAddress,
                    UserAgent,
                    ActionDate
                FROM audit_log
                WHERE Action = @Action
                ORDER BY ActionDate DESC
                LIMIT 1000";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var logs = await connection.QueryAsync<AuditLog>(sql, new { Action = action });
                    return logs;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dei log per azione: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera tutti i log con paginazione
        /// </summary>
        public async Task<IEnumerable<AuditLog>> GetAllAsync(int pageNumber = 1, int pageSize = 100)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize < 1 || pageSize > 1000)
                pageSize = 100;

            var offset = (pageNumber - 1) * pageSize;

            const string sql = @"
                SELECT 
                    LogId,
                    UserId,
                    Username,
                    Action,
                    EntityType,
                    EntityId,
                    OldValues,
                    NewValues,
                    IpAddress,
                    UserAgent,
                    ActionDate
                FROM audit_log
                ORDER BY ActionDate DESC
                LIMIT @Limit OFFSET @Offset";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var logs = await connection.QueryAsync<AuditLog>(sql, new
                    {
                        Limit = pageSize,
                        Offset = offset
                    });

                    return logs;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dei log: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Conta totale record
        /// </summary>
        public async Task<int> GetTotalCountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM audit_log";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var count = await connection.ExecuteScalarAsync<int>(sql);
                    return count;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il conteggio dei log: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Elimina log più vecchi di X giorni
        /// </summary>
        public async Task<int> DeleteOlderThanAsync(int days)
        {
            if (days < 1)
                throw new ArgumentException("Il numero di giorni deve essere maggiore di zero", nameof(days));

            const string sql = @"
                DELETE FROM audit_log
                WHERE ActionDate < DATE_SUB(NOW(), INTERVAL @Days DAY)";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var deletedRows = await connection.ExecuteAsync(sql, new { Days = days });
                    return deletedRows;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'eliminazione dei log vecchi: {ex.Message}", ex);
            }
        }
    }
}
