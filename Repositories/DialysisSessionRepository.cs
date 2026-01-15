using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class DialysisSessionRepository : BaseRepository<DialysisSession>, IDialysisSessionRepository
    {
        public DialysisSessionRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "dialysis_sessions")
        {
        }

        public async Task<IEnumerable<DialysisSession>> GetAllAsync()
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                ORDER BY data_seduta DESC, turno";
            return await QueryAsync(sql);
        }

        public async Task<DialysisSession> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM dialysis_sessions WHERE session_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<DialysisSession>> GetByPatientIdAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                WHERE patient_id = @PatientId 
                ORDER BY data_seduta DESC";
            return await QueryAsync(sql, new { PatientId = patientId });
        }

        public async Task<IEnumerable<DialysisSession>> GetByDateAsync(DateTime date)
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                WHERE data_seduta = @Date 
                ORDER BY turno, station_id";
            return await QueryAsync(sql, new { Date = date.Date });
        }

        public async Task<IEnumerable<DialysisSession>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                WHERE data_seduta BETWEEN @StartDate AND @EndDate 
                ORDER BY data_seduta DESC, turno";
            return await QueryAsync(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });
        }

        public async Task<IEnumerable<DialysisSession>> GetByPatientAndDateRangeAsync(
            int patientId, DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                WHERE patient_id = @PatientId 
                  AND data_seduta BETWEEN @StartDate AND @EndDate 
                ORDER BY data_seduta DESC";

            return await QueryAsync(sql, new
            {
                PatientId = patientId,
                StartDate = startDate.Date,
                EndDate = endDate.Date
            });
        }

        public async Task<DialysisSession> GetLastSessionByPatientAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                WHERE patient_id = @PatientId 
                  AND stato = 'completata'
                ORDER BY data_seduta DESC, ora_fine DESC 
                LIMIT 1";
            return await QueryFirstOrDefaultAsync(sql, new { PatientId = patientId });
        }

        public async Task<IEnumerable<DialysisSession>> GetSessionsByTurnoAsync(DateTime date, string turno)
        {
            const string sql = @"
                SELECT * FROM dialysis_sessions 
                WHERE data_seduta = @Date 
                  AND turno = @Turno 
                ORDER BY station_id";
            return await QueryAsync(sql, new { Date = date.Date, Turno = turno });
        }

        public async Task<int> InsertAsync(DialysisSession entity)
        {
            const string sql = @"
                INSERT INTO dialysis_sessions (
                    patient_id, data_seduta, turno, room_id, station_id, device_id,
                    medico_id, infermiere_id, access_id,
                    ora_inizio, ora_fine, durata_effettiva, durata_prevista,
                    peso_pre, pa_sistolica_pre, pa_diastolica_pre, fc_pre, temperatura_pre,
                    peso_post, pa_sistolica_post, pa_diastolica_post, fc_post, temperatura_post,
                    uf_programmata, uf_effettuata,
                    tipo_trattamento, tipo_filtro, superficie_filtro, qb, qd,
                    bicarbonato, sodio, calcio, potassio,
                    anticoagulante, dose_anticoagulante,
                    ktv, urr, complicanze, stato, note, created_by, updated_by
                ) VALUES (
                    @PatientId, @DataSeduta, @Turno, @RoomId, @StationId, @DeviceId,
                    @MedicoId, @InfermiereId, @AccessId,
                    @OraInizio, @OraFine, @DurataEffettiva, @DurataPrevista,
                    @PesoPre, @PaSistolicaPre, @PaDiastolicaPre, @FcPre, @TemperaturaPre,
                    @PesoPost, @PaSistolicaPost, @PaDiastolicaPost, @FcPost, @TemperaturaPost,
                    @UfProgrammata, @UfEffettuata,
                    @TipoTrattamento, @TipoFiltro, @SuperficieFiltro, @Qb, @Qd,
                    @Bicarbonato, @Sodio, @Calcio, @Potassio,
                    @Anticoagulante, @DoseAnticoagulante,
                    @Ktv, @Urr, @Complicanze, @Stato, @Note, @CreatedBy, @UpdatedBy
                );
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(DialysisSession entity)
        {
            const string sql = @"
                UPDATE dialysis_sessions SET
                    data_seduta = @DataSeduta, turno = @Turno,
                    room_id = @RoomId, station_id = @StationId, device_id = @DeviceId,
                    medico_id = @MedicoId, infermiere_id = @InfermiereId, access_id = @AccessId,
                    ora_inizio = @OraInizio, ora_fine = @OraFine, 
                    durata_effettiva = @DurataEffettiva, durata_prevista = @DurataPrevista,
                    peso_pre = @PesoPre, pa_sistolica_pre = @PaSistolicaPre, 
                    pa_diastolica_pre = @PaDiastolicaPre, fc_pre = @FcPre, 
                    temperatura_pre = @TemperaturaPre,
                    peso_post = @PesoPost, pa_sistolica_post = @PaSistolicaPost, 
                    pa_diastolica_post = @PaDiastolicaPost, fc_post = @FcPost, 
                    temperatura_post = @TemperaturaPost,
                    uf_programmata = @UfProgrammata, uf_effettuata = @UfEffettuata,
                    tipo_trattamento = @TipoTrattamento, tipo_filtro = @TipoFiltro, 
                    superficie_filtro = @SuperficieFiltro, qb = @Qb, qd = @Qd,
                    bicarbonato = @Bicarbonato, sodio = @Sodio, 
                    calcio = @Calcio, potassio = @Potassio,
                    anticoagulante = @Anticoagulante, dose_anticoagulante = @DoseAnticoagulante,
                    ktv = @Ktv, urr = @Urr, complicanze = @Complicanze,
                    stato = @Stato, note = @Note, updated_by = @UpdatedBy
                WHERE session_id = @SessionId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> UpdateStatoAsync(int sessionId, string stato)
        {
            const string sql = @"
                UPDATE dialysis_sessions 
                SET stato = @Stato
                WHERE session_id = @SessionId";

            var rows = await ExecuteAsync(sql, new { SessionId = sessionId, Stato = stato });
            return rows > 0;
        }

        public async Task<int> CountSessionsByPatientAsync(int patientId)
        {
            const string sql = @"
                SELECT COUNT(*) FROM dialysis_sessions 
                WHERE patient_id = @PatientId 
                  AND stato = 'completata'";
            return await ExecuteScalarAsync<int>(sql, new { PatientId = patientId });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM dialysis_sessions WHERE session_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM dialysis_sessions";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
