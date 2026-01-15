using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    public interface IDialysisSessionService
    {
        Task<IEnumerable<DialysisSession>> GetSessionsByPatientAsync(int patientId);
        Task<IEnumerable<DialysisSession>> GetSessionsByDateAsync(DateTime date);
        Task<DialysisSession> GetSessionByIdAsync(int sessionId);
        Task<(bool Success, int SessionId, string ErrorMessage)> CreateSessionAsync(DialysisSession session);
        Task<(bool Success, string ErrorMessage)> UpdateSessionAsync(DialysisSession session);
        Task<bool> StartSessionAsync(int sessionId);
        Task<bool> CompleteSessionAsync(int sessionId);
        Task<decimal?> CalculateKtvAsync(DialysisSession session, decimal? ureaPre, decimal? ureaPost);
        Task<IEnumerable<VitalSign>> GetVitalSignsBySessionAsync(int sessionId);
        Task<bool> AddVitalSignAsync(VitalSign vitalSign);
    }
}
