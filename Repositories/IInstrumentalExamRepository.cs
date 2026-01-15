using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IInstrumentalExamRepository
    {
        Task<InstrumentalExam> GetByIdAsync(int examId);
        Task<IEnumerable<InstrumentalExam>> GetAllAsync();
        Task<IEnumerable<InstrumentalExam>> GetByPatientIdAsync(int patientId);
        Task<int> AddAsync(InstrumentalExam exam);
        Task<bool> UpdateAsync(InstrumentalExam exam);
        Task<bool> DeleteAsync(int examId);
    }
}
