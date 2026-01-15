using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    public interface IAuditService
    {
        Task LogAsync(int? userId, string azione, string tabella, int? recordId, string dettagli);
        Task<IEnumerable<AuditLog>> GetLogsByUserAsync(int userId, int limit = 100);
        Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<AuditLog>> GetLogsByActionAsync(string azione);
        Task<IEnumerable<AuditLog>> GetLogsByTableAsync(string tabella, int recordId);
    }
}
