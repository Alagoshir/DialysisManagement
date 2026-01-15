using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface ILabTestRepository : IRepository<LabTest>
    {
        Task<IEnumerable<LabTest>> GetByPatientIdAsync(int patientId);
        Task<LabTest> GetLastTestByPatientAsync(int patientId);
        Task<IEnumerable<LabTest>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<LabTest>> GetTestsWithAlertsAsync();
    }
}
