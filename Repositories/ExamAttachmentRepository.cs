using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;
using NLog;

namespace DialysisManagement.Repositories
{
    public class ExamAttachmentRepository : IExamAttachmentRepository
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly Logger _logger;

        public ExamAttachmentRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<ExamAttachment> GetByIdAsync(int attachmentId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM exam_attachments 
                        WHERE AttachmentId = @AttachmentId";

                    return await connection.QueryFirstOrDefaultAsync<ExamAttachment>(
                        sql, new { AttachmentId = attachmentId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero allegato {attachmentId}");
                throw;
            }
        }

        public async Task<IEnumerable<ExamAttachment>> GetByExamIdAsync(int examId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM exam_attachments 
                        WHERE ExamId = @ExamId 
                        ORDER BY UploadDate DESC";

                    return await connection.QueryAsync<ExamAttachment>(
                        sql, new { ExamId = examId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero allegati esame {examId}");
                throw;
            }
        }

        public async Task<int> AddAsync(ExamAttachment attachment)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        INSERT INTO exam_attachments 
                        (ExamId, FileName, FilePath, MimeType, FileSize, 
                         IsEncrypted, FileHash, Description, UploadDate, UploadedBy)
                        VALUES 
                        (@ExamId, @FileName, @FilePath, @MimeType, @FileSize, 
                         @IsEncrypted, @FileHash, @Description, @UploadDate, @UploadedBy);
                        SELECT LAST_INSERT_ID();";

                    return await connection.ExecuteScalarAsync<int>(sql, attachment);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore inserimento allegato");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int attachmentId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = "DELETE FROM exam_attachments WHERE AttachmentId = @AttachmentId";
                    var rows = await connection.ExecuteAsync(sql, new { AttachmentId = attachmentId });
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione allegato {attachmentId}");
                throw;
            }
        }
    }
}
