using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DialysisManagement.Services
{
    public interface IAlertService
    {
        Task<List<string>> CheckLabAlertsAsync();
        Task<List<string>> CheckMaintenanceAlertsAsync();
        Task<List<string>> CheckVaccinationAlertsAsync();
        Task<Dictionary<string, List<string>>> GetAllAlertsAsync();
    }
}
