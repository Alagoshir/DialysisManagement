using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class LabTestRepository : BaseRepository<LabTest>, ILabTestRepository
    {
        public LabTestRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "lab_tests")
        {
        }

        public async Task<IEnumerable<LabTest>> GetAllAsync()
        {
            const string sql = "SELECT * FROM lab_tests ORDER BY data_prelievo DESC";
            return await QueryAsync(sql);
        }

        public async Task<LabTest> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM lab_tests WHERE test_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<LabTest>> GetByPatientIdAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM lab_tests 
                WHERE patient_id = @PatientId 
                ORDER BY data_prelievo DESC";
            return await QueryAsync(sql, new { PatientId = patientId });
        }

        public async Task<LabTest> GetLastTestByPatientAsync(int patientId)
        {
            const string sql = @"
                SELECT * FROM lab_tests 
                WHERE patient_id = @PatientId 
                ORDER BY data_prelievo DESC 
                LIMIT 1";
            return await QueryFirstOrDefaultAsync(sql, new { PatientId = patientId });
        }

        public async Task<IEnumerable<LabTest>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            const string sql = @"
                SELECT * FROM lab_tests 
                WHERE data_prelievo BETWEEN @StartDate AND @EndDate 
                ORDER BY data_prelievo DESC";
            return await QueryAsync(sql, new { StartDate = startDate.Date, EndDate = endDate.Date });
        }

        public async Task<IEnumerable<LabTest>> GetTestsWithAlertsAsync()
        {
            const string sql = @"
                SELECT * FROM lab_tests 
                WHERE alert_generato = TRUE 
                ORDER BY data_prelievo DESC";
            return await QueryAsync(sql);
        }

        public async Task<int> InsertAsync(LabTest entity)
        {
            const string sql = @"
                INSERT INTO lab_tests (
                    patient_id, data_prelievo, data_referto,
                    hb, hct, rbc, wbc, plt,
                    creatinina, azotemia, urea_pre, urea_post,
                    sodio, potassio, calcio, fosforo, magnesio, cloro,
                    pth, vitamina_d,
                    ferritina, transferrina, saturazione_transferrina, ferro_sierico,
                    alt, ast, bilirubina_totale, albumina, proteine_totali,
                    colesterolo_totale, colesterolo_hdl, colesterolo_ldl, trigliceridi,
                    glicemia, emoglobina_glicata,
                    pt, inr, aptt,
                    hbsag, hcv_ab, hiv_ab,
                    pcr, note, alert_generato, created_by
                ) VALUES (
                    @PatientId, @DataPrelievo, @DataReferto,
                    @Hb, @Hct, @Rbc, @Wbc, @Plt,
                    @Creatinina, @Azotemia, @UreaPre, @UreaPost,
                    @Sodio, @Potassio, @Calcio, @Fosforo, @Magnesio, @Cloro,
                    @Pth, @VitaminaD,
                    @Ferritina, @Transferrina, @SaturazioneTransferrina, @FerroSierico,
                    @Alt, @Ast, @BilirubinaTotale, @Albumina, @ProteineTotali,
                    @ColesteroloTotale, @ColesteroloHdl, @ColesteroloLdl, @Trigliceridi,
                    @Glicemia, @EmoglobinaGlicata,
                    @Pt, @Inr, @Aptt,
                    @Hbsag, @HcvAb, @HivAb,
                    @Pcr, @Note, @AlertGenerato, @CreatedBy
                );
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(LabTest entity)
        {
            const string sql = @"
                UPDATE lab_tests SET
                    data_prelievo = @DataPrelievo, data_referto = @DataReferto,
                    hb = @Hb, hct = @Hct, rbc = @Rbc, wbc = @Wbc, plt = @Plt,
                    creatinina = @Creatinina, azotemia = @Azotemia, 
                    urea_pre = @UreaPre, urea_post = @UreaPost,
                    sodio = @Sodio, potassio = @Potassio, calcio = @Calcio, 
                    fosforo = @Fosforo, magnesio = @Magnesio, cloro = @Cloro,
                    pth = @Pth, vitamina_d = @VitaminaD,
                    ferritina = @Ferritina, transferrina = @Transferrina, 
                    saturazione_transferrina = @SaturazioneTransferrina, 
                    ferro_sierico = @FerroSierico,
                    alt = @Alt, ast = @Ast, bilirubina_totale = @BilirubinaTotale, 
                    albumina = @Albumina, proteine_totali = @ProteineTotali,
                    colesterolo_totale = @ColesteroloTotale, colesterolo_hdl = @ColesteroloHdl, 
                    colesterolo_ldl = @ColesteroloLdl, trigliceridi = @Trigliceridi,
                    glicemia = @Glicemia, emoglobina_glicata = @EmoglobinaGlicata,
                    pt = @Pt, inr = @Inr, aptt = @Aptt,
                    hbsag = @Hbsag, hcv_ab = @HcvAb, hiv_ab = @HivAb,
                    pcr = @Pcr, note = @Note, alert_generato = @AlertGenerato
                WHERE test_id = @TestId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM lab_tests WHERE test_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM lab_tests";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
