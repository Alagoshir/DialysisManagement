using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public interface IExamAttachmentRepository
    {
        Task<ExamAttachment> GetByIdAsync(int attachmentId);
        Task<IEnumerable<ExamAttachment>> GetByExamIdAsync(int examId);
        Task<int> AddAsync(ExamAttachment attachment);
        Task<bool> DeleteAsync(int attachmentId);
    }
}
