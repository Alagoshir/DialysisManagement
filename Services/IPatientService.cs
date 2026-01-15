using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int patientId);
        Task<Patient> GetPatientByCodiceFiscaleAsync(string codiceFiscale);
        Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm);
        Task<IEnumerable<Patient>> GetActivePatientsAsync();
        Task<IEnumerable<Patient>> GetContumacialPatientsAsync();
        Task<(bool Success, int PatientId, string ErrorMessage)> CreatePatientAsync(Patient patient);
        Task<(bool Success, string ErrorMessage)> UpdatePatientAsync(Patient patient);
        Task<(bool Success, string ErrorMessage)> DeletePatientAsync(int patientId);
        Task<bool> UpdatePesoSeccoAsync(int patientId, decimal pesoSecco);
        Task<string> GenerateQRCodeAsync(int patientId);
        Task<bool> ValidatePatientDataAsync(Patient patient);
    }
}
