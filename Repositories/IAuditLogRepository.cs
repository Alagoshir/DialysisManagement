using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    /// <summary>
    /// Repository per audit log
    /// </summary>
    public interface IAuditLogRepository
    {
        /// <summary>
        /// Inserisce un nuovo record di audit
        /// </summary>
        Task<int> InsertAsync(AuditLog auditLog);

        /// <summary>
        /// Recupera tutti i log per periodo
        /// </summary>
        Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Recupera log per utente
        /// </summary>
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Recupera log per paziente
        /// </summary>
        Task<IEnumerable<AuditLog>> GetByPatientIdAsync(int patientId);

        /// <summary>
        /// Recupera log per azione
        /// </summary>
        Task<IEnumerable<AuditLog>> GetByActionAsync(string action);

        /// <summary>
        /// Recupera tutti i log con paginazione
        /// </summary>
        Task<IEnumerable<AuditLog>> GetAllAsync(int pageNumber = 1, int pageSize = 100);

        /// <summary>
        /// Conta totale record
        /// </summary>
        Task<int> GetTotalCountAsync();

        /// <summary>
        /// Elimina log più vecchi di X giorni
        /// </summary>
        Task<int> DeleteOlderThanAsync(int days);
    }
}
