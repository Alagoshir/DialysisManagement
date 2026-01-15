using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "prescriptions")
        {
        }

        public async Task<IEnumerable<Prescription>> GetAllAsync()
        {
            const string sql = "SELECT * FROM prescriptions ORDER BY data_prescrizione DESC";
            return await QueryAsync(sql);
        }

        public async Task<Prescription> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM prescriptions WHERE prescription_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM prescriptions 
                WHERE patient_id = @PatientId 
                ORDER BY data_prescrizione DESC";
            return await QueryAsync(sql, new { PatientId = patientId });
        }

        public async Task<Prescription> GetActivePrescriptionByPatientAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM prescriptions 
                WHERE patient_id = @PatientId 
                  AND attiva = TRUE
                  AND data_inizio_validita <= CURDATE()
                  AND (data_fine_validita IS NULL OR data_fine_validita >= CURDATE())
                ORDER BY data_prescrizione DESC 
                LIMIT 1";
            return await QueryFirstOrDefaultAsync(sql, new { PatientId = patientId });
        }

        public async Task<bool> DeactivateOldPrescriptionsAsync(int patientId)
        {
            const string sql = @"
                UPDATE prescriptions 
                SET attiva = FALSE
                WHERE patient_id = @PatientId 
                  AND attiva = TRUE";

            var rows = await ExecuteAsync(sql, new { PatientId = patientId });
            return rows > 0;
        }

        public async Task<int> InsertAsync(Prescription entity)
        {
            const string sql = @"
                INSERT INTO prescriptions (
                    patient_id, data_prescrizione, data_inizio_validita, data_fine_validita, attiva,
                    frequenza_settimanale, durata_seduta, tipo_trattamento, peso_secco_target, uf_target,
                    qb_prescritto, qd_prescritto, tipo_filtro,
                    sodio, potassio, calcio, bicarbonato, temperatura,
                    anticoagulante, dosaggio_anticoagulante, note, created_by
                ) VALUES (
                    @PatientId, @DataPrescrizione, @DataInizioValidita, @DataFineValidita, @Attiva,
                    @FrequenzaSettimanale, @DurataSeduta, @TipoTrattamento, @PesoSeccoTarget, @UfTarget,
                    @QbPrescritto, @QdPrescritto, @TipoFiltro,
                    @Sodio, @Potassio, @Calcio, @Bicarbonato, @Temperatura,
                    @Anticoagulante, @DosaggioAnticoagulante, @Note, @CreatedBy
                );
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(Prescription entity)
        {
            const string sql = @"
                UPDATE prescriptions SET
                    data_prescrizione = @DataPrescrizione, 
                    data_inizio_validita = @DataInizioValidita, 
                    data_fine_validita = @DataFineValidita, attiva = @Attiva,
                    frequenza_settimanale = @FrequenzaSettimanale, 
                    durata_seduta = @DurataSeduta, 
                    tipo_trattamento = @TipoTrattamento, 
                    peso_secco_target = @PesoSeccoTarget, uf_target = @UfTarget,
                    qb_prescritto = @QbPrescritto, qd_prescritto = @QdPrescritto, 
                    tipo_filtro = @TipoFiltro,
                    sodio = @Sodio, potassio = @Potassio, calcio = @Calcio, 
                    bicarbonato = @Bicarbonato, temperatura = @Temperatura,
                    anticoagulante = @Anticoagulante, 
                    dosaggio_anticoagulante = @DosaggioAnticoagulante, note = @Note
                WHERE prescription_id = @PrescriptionId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM prescriptions WHERE prescription_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM prescriptions";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
