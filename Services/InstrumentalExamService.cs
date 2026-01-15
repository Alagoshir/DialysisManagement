using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using DialysisManagement.Utilities;
using NLog;

namespace DialysisManagement.Services
{
    public class InstrumentalExamService : IInstrumentalExamService
    {
        private readonly IInstrumentalExamRepository _repository;
        private readonly IExamAttachmentRepository _attachmentRepository;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IFileHelper _fileHelper;
        private readonly IAuditService _auditService;
        private readonly Logger _logger;
        private readonly string _attachmentsPath;

        public InstrumentalExamService(
            IInstrumentalExamRepository repository,
            IExamAttachmentRepository attachmentRepository,
            IEncryptionHelper encryptionHelper,
            IFileHelper fileHelper,
            IAuditService auditService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _encryptionHelper = encryptionHelper ?? throw new ArgumentNullException(nameof(encryptionHelper));
            _fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = LogManager.GetCurrentClassLogger();

            _attachmentsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Attachments");
            Directory.CreateDirectory(_attachmentsPath);
        }

        public async Task<IEnumerable<InstrumentalExam>> GetExamsByPatientAsync(int patientId)
        {
            try
            {
                return await _repository.GetByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero esami paziente {patientId}");
                throw;
            }
        }

        public async Task<InstrumentalExam> GetExamByIdAsync(int examId)
        {
            try
            {
                return await _repository.GetByIdAsync(examId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero esame {examId}");
                throw;
            }
        }

        public async Task<(bool success, int examId, string errorMessage)> CreateExamAsync(InstrumentalExam exam)
        {
            try
            {
                if (exam.PatientId <= 0)
                    return (false, 0, "ID paziente non valido");

                if (string.IsNullOrWhiteSpace(exam.TipoEsame))
                    return (false, 0, "Tipo esame obbligatorio");

                exam.CreatedDate = DateTime.Now;
                var examId = await _repository.AddAsync(exam);

                await _auditService.LogAsync("Create", "InstrumentalExam", examId,
                    $"Creato esame {exam.TipoEsame} per paziente {exam.PatientId}");

                _logger.Info($"Creato esame strumentale ID {examId}");
                return (true, examId, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore creazione esame");
                return (false, 0, ex.Message);
            }
        }

        public async Task<(bool success, string errorMessage)> UpdateExamAsync(InstrumentalExam exam)
        {
            try
            {
                exam.ModifiedDate = DateTime.Now;
                var success = await _repository.UpdateAsync(exam);

                if (success)
                {
                    await _auditService.LogAsync("Update", "InstrumentalExam", exam.ExamId,
                        $"Aggiornato esame {exam.TipoEsame}");
                }

                return (success, success ? null : "Errore aggiornamento database");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore aggiornamento esame {exam.ExamId}");
                return (false, ex.Message);
            }
        }

        public async Task<bool> DeleteExamAsync(int examId)
        {
            try
            {
                // Elimina prima gli allegati
                var attachments = await _attachmentRepository.GetByExamIdAsync(examId);
                foreach (var attachment in attachments)
                {
                    await DeleteAttachmentAsync(attachment.AttachmentId);
                }

                var success = await _repository.DeleteAsync(examId);

                if (success)
                {
                    await _auditService.LogAsync("Delete", "InstrumentalExam", examId,
                        "Eliminato esame strumentale");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione esame {examId}");
                return false;
            }
        }

        public async Task<IEnumerable<ExamAttachment>> GetAttachmentsByExamAsync(int examId)
        {
            try
            {
                return await _attachmentRepository.GetByExamIdAsync(examId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero allegati esame {examId}");
                throw;
            }
        }

        public async Task<(bool success, int attachmentId)> AddAttachmentAsync(int examId, string fileName, byte[] fileData)
        {
            try
            {
                // Genera nome file univoco
                var fileExtension = Path.GetExtension(fileName);
                var uniqueFileName = $"{examId}_{Guid.NewGuid()}{fileExtension}";
                var filePath = Path.Combine(_attachmentsPath, uniqueFileName);

                // Cripta file se configurato
                var encryptFiles = ConfigurationHelper.GetAppSettingBool("EncryptAttachments", true);
                byte[] dataToSave = fileData;

                if (encryptFiles)
                {
                    dataToSave = _encryptionHelper.Encrypt(fileData);
                }

                // Salva file
                await File.WriteAllBytesAsync(filePath, dataToSave);

                // Calcola hash
                string fileHash;
                using (var sha256 = SHA256.Create())
                {
                    var hashBytes = sha256.ComputeHash(fileData);
                    fileHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
                }

                // Crea record allegato
                var attachment = new ExamAttachment
                {
                    ExamId = examId,
                    FileName = fileName,
                    FilePath = filePath,
                    MimeType = _fileHelper.GetMimeType(fileName),
                    FileSize = fileData.Length,
                    IsEncrypted = encryptFiles,
                    FileHash = fileHash,
                    UploadDate = DateTime.Now
                };

                var attachmentId = await _attachmentRepository.AddAsync(attachment);

                await _auditService.LogAsync("Upload", "ExamAttachment", attachmentId,
                    $"Caricato file {fileName} per esame {examId}");

                _logger.Info($"Allegato {fileName} salvato per esame {examId}");
                return (true, attachmentId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore salvataggio allegato");
                return (false, 0);
            }
        }

        public async Task<bool> DeleteAttachmentAsync(int attachmentId)
        {
            try
            {
                var attachment = await _attachmentRepository.GetByIdAsync(attachmentId);
                if (attachment == null)
                    return false;

                // Elimina file fisico
                if (File.Exists(attachment.FilePath))
                {
                    File.Delete(attachment.FilePath);
                }

                // Elimina record
                var success = await _attachmentRepository.DeleteAsync(attachmentId);

                if (success)
                {
                    await _auditService.LogAsync("Delete", "ExamAttachment", attachmentId,
                        $"Eliminato allegato {attachment.FileName}");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione allegato {attachmentId}");
                return false;
            }
        }

        public async Task<byte[]> GetAttachmentDataAsync(int attachmentId)
        {
            try
            {
                var attachment = await _attachmentRepository.GetByIdAsync(attachmentId);
                if (attachment == null || !File.Exists(attachment.FilePath))
                    return null;

                var fileData = await File.ReadAllBytesAsync(attachment.FilePath);

                // Decripta se necessario
                if (attachment.IsEncrypted)
                {
                    fileData = _encryptionHelper.Decrypt(fileData);
                }

                await _auditService.LogAsync("Download", "ExamAttachment", attachmentId,
                    $"Scaricato allegato {attachment.FileName}");

                return fileData;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero dati allegato {attachmentId}");
                throw;
            }
        }

        public async Task<IEnumerable<InstrumentalExam>> GetExpiringExamsAsync(int daysThreshold)
        {
            try
            {
                var allExams = await _repository.GetAllAsync();
                var expiringDate = DateTime.Today.AddDays(daysThreshold);

                return allExams.Where(e =>
                    e.DataScadenza.HasValue &&
                    e.DataScadenza.Value <= expiringDate &&
                    e.DataScadenza.Value >= DateTime.Today);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore recupero esami in scadenza");
                throw;
            }
        }
    }
}
