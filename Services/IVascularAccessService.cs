using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    public interface IVascularAccessService
    {
        Task<IEnumerable<VascularAccess>> GetAccessesByPatientAsync(int patientId);
        Task<VascularAccess> GetAccessByIdAsync(int accessId);
        Task<VascularAccess> GetActiveAccessByPatientAsync(int patientId);
        Task<(bool success, int accessId, string errorMessage)> CreateAccessAsync(VascularAccess access);
        Task<(bool success, string errorMessage)> UpdateAccessAsync(VascularAccess access);
        Task<bool> DeleteAccessAsync(int accessId);
        Task<bool> DeactivateAccessAsync(int accessId, string reason);
        Task<IEnumerable<VascularAccess>> GetAccessesByTypeAsync(string type);
        Task<IEnumerable<VascularAccess>> GetMalfunctioningAccessesAsync();
    }
}
