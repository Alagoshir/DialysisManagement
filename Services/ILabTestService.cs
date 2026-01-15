using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    public interface ILabTestService
    {
        Task<IEnumerable<LabTest>> GetTestsByPatientAsync(int patientId);
        Task<LabTest> GetTestByIdAsync(int testId);
        Task<LabTest> GetLastTestByPatientAsync(int patientId);
        Task<(bool Success, int TestId, string ErrorMessage)> CreateLabTestAsync(LabTest labTest);
        Task<(bool Success, string ErrorMessage)> UpdateLabTestAsync(LabTest labTest);
        Task<bool> CheckAlertsAsync(LabTest labTest);
        Task<Dictionary<string, (decimal? Value, bool OutOfRange)>> GetParametersStatusAsync(LabTest labTest);
    }
}
