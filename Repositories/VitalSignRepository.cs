using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class VitalSignRepository : BaseRepository<VitalSign>, IVitalSignRepository
    {
        public VitalSignRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "vital_signs")
        {
        }

        public async Task<IEnumerable<VitalSign>> GetAllAsync()
        {
            const string sql = "SELECT * FROM vital_signs ORDER BY session_id, ora_rilevazione";
            return await QueryAsync(sql);
        }

        public async Task<VitalSign> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM vital_signs WHERE vital_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<VitalSign>> GetBySessionIdAsync(int sessionId)
        {
            const string sql = @"
                SELECT * FROM vital_signs 
                WHERE session_id = @SessionId 
                ORDER BY ora_rilevazione";
            return await QueryAsync(sql, new { SessionId = sessionId });
        }

        public async Task<int> InsertAsync(VitalSign entity)
        {
            const string sql = @"
                INSERT INTO vital_signs (
                    session_id, ora_rilevazione, pa_sistolica, pa_diastolica, 
                    frequenza_cardiaca, temperatura, saturazione_o2, note, created_by
                ) VALUES (
                    @SessionId, @OraRilevazione, @PaSistolica, @PaDiastolica, 
                    @FrequenzaCardiaca, @Temperatura, @SaturazioneO2, @Note, @CreatedBy
                );
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(VitalSign entity)
        {
            const string sql = @"
                UPDATE vital_signs SET
                    ora_rilevazione = @OraRilevazione, pa_sistolica = @PaSistolica, 
                    pa_diastolica = @PaDiastolica, frequenza_cardiaca = @FrequenzaCardiaca, 
                    temperatura = @Temperatura, saturazione_o2 = @SaturazioneO2, note = @Note
                WHERE vital_id = @VitalId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM vital_signs WHERE vital_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM vital_signs";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
