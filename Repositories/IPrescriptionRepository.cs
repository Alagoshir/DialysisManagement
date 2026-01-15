using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId);
        Task<Prescription> GetActivePrescriptionByPatientAsync(int patientId);
        Task<bool> DeactivateOldPrescriptionsAsync(int patientId);
    }
}
