using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IMedicationRepository : IRepository<Medication>
    {
        Task<IEnumerable<Medication>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<Medication>> GetActiveMedicationsByPatientAsync(int patientId);
    }
}
