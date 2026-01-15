using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "medications")
        {
        }

        public async Task<IEnumerable<Medication>> GetAllAsync()
        {
            const string sql = "SELECT * FROM medications ORDER BY data_inizio DESC";
            return await QueryAsync(sql);
        }

        public async Task<Medication> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM medications WHERE medication_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Medication>> GetByPatientIdAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM medications 
                WHERE patient_id = @PatientId 
                ORDER BY attivo DESC, data_inizio DESC";
            return await QueryAsync(sql, new { PatientId = patientId });
        }

        public async Task<IEnumerable<Medication>> GetActiveMedicationsByPatientAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM medications 
                WHERE patient_id = @PatientId 
                  AND attivo = TRUE
                  AND (data_fine IS NULL OR data_fine >= CURDATE())
                ORDER BY nome_farmaco";
            return await QueryAsync(sql, new { PatientId = patientId });
        }

        public async Task<int> InsertAsync(Medication entity)
        {
            const string sql = @"
                INSERT INTO medications (
                    patient_id, nome_farmaco, principio_attivo, via_somministrazione,
                    dose, unita_misura, frequenza, data_inizio, data_fine, attivo, note, created_by
                ) VALUES (
                    @PatientId, @NomeFarmaco, @PrincipioAttivo, @ViaSomministrazione,
                    @Dose, @UnitaMisura, @Frequenza, @DataInizio, @DataFine, @Attivo, @Note, @CreatedBy
                );
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(Medication entity)
        {
            const string sql = @"
                UPDATE medications SET
                    nome_farmaco = @NomeFarmaco, principio_attivo = @PrincipioAttivo,
                    via_somministrazione = @ViaSomministrazione, dose = @Dose,
                    unita_misura = @UnitaMisura, frequenza = @Frequenza,
                    data_inizio = @DataInizio, data_fine = @DataFine, attivo = @Attivo, note = @Note
                WHERE medication_id = @MedicationId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM medications WHERE medication_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM medications";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
