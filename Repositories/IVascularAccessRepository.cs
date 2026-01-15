using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IVascularAccessRepository
    {
        Task<VascularAccess> GetByIdAsync(int accessId);
        Task<IEnumerable<VascularAccess>> GetAllAsync();
        Task<IEnumerable<VascularAccess>> GetByPatientIdAsync(int patientId);
        Task<int> AddAsync(VascularAccess access);
        Task<bool> UpdateAsync(VascularAccess access);
        Task<bool> DeleteAsync(int accessId);
    }
}
