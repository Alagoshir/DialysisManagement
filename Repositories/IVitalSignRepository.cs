using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IVitalSignRepository : IRepository<VitalSign>
    {
        Task<IEnumerable<VitalSign>> GetBySessionIdAsync(int sessionId);
    }
}
