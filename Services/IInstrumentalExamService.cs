using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    public interface IInstrumentalExamService
    {
        Task<IEnumerable<InstrumentalExam>> GetExamsByPatientAsync(int patientId);
        Task<InstrumentalExam> GetExamByIdAsync(int examId);
        Task<(bool success, int examId, string errorMessage)> CreateExamAsync(InstrumentalExam exam);
        Task<(bool success, string errorMessage)> UpdateExamAsync(InstrumentalExam exam);
        Task<bool> DeleteExamAsync(int examId);
        Task<IEnumerable<ExamAttachment>> GetAttachmentsByExamAsync(int examId);
        Task<(bool success, int attachmentId)> AddAttachmentAsync(int examId, string fileName, byte[] fileData);
        Task<bool> DeleteAttachmentAsync(int attachmentId);
        Task<byte[]> GetAttachmentDataAsync(int attachmentId);
        Task<IEnumerable<InstrumentalExam>> GetExpiringExamsAsync(int daysThreshold);
    }
}
