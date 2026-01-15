using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<Patient> GetByCodiceFiscaleAsync(string codiceFiscale);
        Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm);
        Task<IEnumerable<Patient>> GetActivePatientsAsync();
        Task<IEnumerable<Patient>> GetByStatoAsync(string stato);
        Task<IEnumerable<Patient>> GetContumacialPatientsAsync();
        Task<IEnumerable<Patient>> GetPatientsInListaTrapiantoAsync();
        Task<bool> UpdatePesoSeccoAsync(int patientId, decimal pesoSecco);
        Task<bool> UpdateStatoAsync(int patientId, string stato);
    }
}
