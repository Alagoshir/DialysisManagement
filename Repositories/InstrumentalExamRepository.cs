using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;
using NLog;

namespace DialysisManagement.Repositories
{
    public class InstrumentalExamRepository : IInstrumentalExamRepository
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly Logger _logger;

        public InstrumentalExamRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<InstrumentalExam> GetByIdAsync(int examId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM instrumental_exams 
                        WHERE ExamId = @ExamId";

                    return await connection.QueryFirstOrDefaultAsync<InstrumentalExam>(
                        sql, new { ExamId = examId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero esame {examId}");
                throw;
            }
        }

        public async Task<IEnumerable<InstrumentalExam>> GetAllAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM instrumental_exams 
                        ORDER BY DataEsecuzione DESC";

                    return await connection.QueryAsync<InstrumentalExam>(sql);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore recupero tutti esami");
                throw;
            }
        }

        public async Task<IEnumerable<InstrumentalExam>> GetByPatientIdAsync(int patientId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM instrumental_exams 
                        WHERE PatientId = @PatientId 
                        ORDER BY DataEsecuzione DESC";

                    return await connection.QueryAsync<InstrumentalExam>(
                        sql, new { PatientId = patientId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero esami paziente {patientId}");
                throw;
            }
        }

        public async Task<int> AddAsync(InstrumentalExam exam)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        INSERT INTO instrumental_exams 
                        (PatientId, TipoEsame, DataEsecuzione, DataReferto, Medico, 
                         IndicazioneClinica, Referto, EsameInterno, CodiceNomenclatore, 
                         CentroEsecuzione, Urgente, DaRipetere, DataScadenza, Note, 
                         CreatedBy, CreatedDate)
                        VALUES 
                        (@PatientId, @TipoEsame, @DataEsecuzione, @DataReferto, @Medico, 
                         @IndicazioneClinica, @Referto, @EsameInterno, @CodiceNomenclatore, 
                         @CentroEsecuzione, @Urgente, @DaRipetere, @DataScadenza, @Note, 
                         @CreatedBy, @CreatedDate);
                        SELECT LAST_INSERT_ID();";

                    return await connection.ExecuteScalarAsync<int>(sql, exam);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore inserimento esame");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(InstrumentalExam exam)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        UPDATE instrumental_exams SET
                            TipoEsame = @TipoEsame,
                            DataEsecuzione = @DataEsecuzione,
                            DataReferto = @DataReferto,
                            Medico = @Medico,
                            IndicazioneClinica = @IndicazioneClinica,
                            Referto = @Referto,
                            EsameInterno = @EsameInterno,
                            CodiceNomenclatore = @CodiceNomenclatore,
                            CentroEsecuzione = @CentroEsecuzione,
                            Urgente = @Urgente,
                            DaRipetere = @DaRipetere,
                            DataScadenza = @DataScadenza,
                            Note = @Note,
                            ModifiedBy = @ModifiedBy,
                            ModifiedDate = @ModifiedDate
                        WHERE ExamId = @ExamId";

                    var rows = await connection.ExecuteAsync(sql, exam);
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore aggiornamento esame {exam.ExamId}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int examId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = "DELETE FROM instrumental_exams WHERE ExamId = @ExamId";
                    var rows = await connection.ExecuteAsync(sql, new { ExamId = examId });
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione esame {examId}");
                throw;
            }
        }
    }
}
