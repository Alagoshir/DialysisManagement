using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IDialysisSessionRepository : IRepository<DialysisSession>
    {
        Task<IEnumerable<DialysisSession>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<DialysisSession>> GetByDateAsync(DateTime date);
        Task<IEnumerable<DialysisSession>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<DialysisSession>> GetByPatientAndDateRangeAsync(int patientId, DateTime startDate, DateTime endDate);
        Task<DialysisSession> GetLastSessionByPatientAsync(int patientId);
        Task<IEnumerable<DialysisSession>> GetSessionsByTurnoAsync(DateTime date, string turno);
        Task<bool> UpdateStatoAsync(int sessionId, string stato);
        Task<int> CountSessionsByPatientAsync(int patientId);
    }
}
