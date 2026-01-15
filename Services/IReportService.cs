using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DialysisManagement.Services
{
    public interface IReportService
    {
        Task<string> GeneratePatientReportAsync(int patientId, DateTime? startDate = null, DateTime? endDate = null);
        Task<string> GenerateSessionsReportAsync(DateTime startDate, DateTime endDate);
        Task<string> GenerateLabTestsReportAsync(int patientId);
        Task<string> GenerateMonthlyStatisticsAsync(int year, int month);
        Task<string> ExportToExcelAsync(string reportType, Dictionary<string, object> parameters);
        Task<byte[]> GeneratePdfReportAsync(string reportType, Dictionary<string, object> parameters);
    }
}
