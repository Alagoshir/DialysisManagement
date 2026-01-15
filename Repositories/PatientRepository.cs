using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(IDatabaseConnectionFactory connectionFactory)
            : base(connectionFactory, "patients")
        {
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            const string sql = "SELECT * FROM patients ORDER BY cognome, nome";
            return await QueryAsync(sql);
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            const string sql = "SELECT * FROM patients WHERE patient_id = @Id";
            return await QueryFirstOrDefaultAsync(sql, new { Id = id });
        }

        public async Task<Patient> GetByCodiceFiscaleAsync(string codiceFiscale)
        {
            const string sql = "SELECT * FROM patients WHERE codice_fiscale = @CodiceFiscale";
            return await QueryFirstOrDefaultAsync(sql, new { CodiceFiscale = codiceFiscale });
        }

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm)
        {
            const string sql = @"
                SELECT * FROM patients 
                WHERE cognome LIKE @Search 
                   OR nome LIKE @Search 
                   OR codice_fiscale LIKE @Search
                ORDER BY cognome, nome";

            return await QueryAsync(sql, new { Search = $"%{searchTerm}%" });
        }

        public async Task<IEnumerable<Patient>> GetActivePatientsAsync()
        {
            const string sql = "SELECT * FROM patients WHERE stato = 'attivo' ORDER BY cognome, nome";
            return await QueryAsync(sql);
        }

        public async Task<IEnumerable<Patient>> GetByStatoAsync(string stato)
        {
            const string sql = "SELECT * FROM patients WHERE stato = @Stato ORDER BY cognome, nome";
            return await QueryAsync(sql, new { Stato = stato });
        }

        public async Task<IEnumerable<Patient>> GetContumacialPatientsAsync()
        {
            const string sql = @"
                SELECT * FROM patients 
                WHERE hbsag_positive = TRUE 
                   OR hcv_positive = TRUE 
                   OR hiv_positive = TRUE
                ORDER BY cognome, nome";

            return await QueryAsync(sql);
        }

        public async Task<IEnumerable<Patient>> GetPatientsInListaTrapiantoAsync()
        {
            const string sql = @"
                SELECT * FROM patients 
                WHERE in_lista_trapianto = TRUE 
                ORDER BY data_inserimento_lista";

            return await QueryAsync(sql);
        }

        public async Task<int> InsertAsync(Patient entity)
        {
            const string sql = @"
                INSERT INTO patients (
                    codice_fiscale, cognome, nome, data_nascita, luogo_nascita, sesso,
                    indirizzo, citta, cap, provincia, telefono, cellulare, email,
                    codice_sanitario, medico_curante, gruppo_sanguigno, peso_secco, altezza, bmi,
                    hbsag_positive, hcv_positive, hiv_positive,
                    in_lista_trapianto, data_inserimento_lista,
                    stato, data_ingresso, consenso_trattamento_dati, data_consenso,
                    note, created_by, updated_by
                ) VALUES (
                    @CodiceFiscale, @Cognome, @Nome, @DataNascita, @LuogoNascita, @Sesso,
                    @Indirizzo, @Citta, @Cap, @Provincia, @Telefono, @Cellulare, @Email,
                    @CodiceSanitario, @MedicoCurante, @GruppoSanguigno, @PesoSecco, @Altezza, @Bmi,
                    @HbsagPositive, @HcvPositive, @HivPositive,
                    @InListaTrapianto, @DataInserimentoLista,
                    @Stato, @DataIngresso, @ConsensoTrattamentoDati, @DataConsenso,
                    @Note, @CreatedBy, @UpdatedBy
                );
                SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, entity);
        }

        public async Task<bool> UpdateAsync(Patient entity)
        {
            const string sql = @"
                UPDATE patients SET
                    codice_fiscale = @CodiceFiscale, cognome = @Cognome, nome = @Nome,
                    data_nascita = @DataNascita, luogo_nascita = @LuogoNascita, sesso = @Sesso,
                    indirizzo = @Indirizzo, citta = @Citta, cap = @Cap, provincia = @Provincia,
                    telefono = @Telefono, cellulare = @Cellulare, email = @Email,
                    codice_sanitario = @CodiceSanitario, medico_curante = @MedicoCurante,
                    gruppo_sanguigno = @GruppoSanguigno, peso_secco = @PesoSecco, 
                    altezza = @Altezza, bmi = @Bmi,
                    hbsag_positive = @HbsagPositive, hcv_positive = @HcvPositive, 
                    hiv_positive = @HivPositive,
                    in_lista_trapianto = @InListaTrapianto, 
                    data_inserimento_lista = @DataInserimentoLista,
                    stato = @Stato, note = @Note, updated_by = @UpdatedBy
                WHERE patient_id = @PatientId";

            var rows = await ExecuteAsync(sql, entity);
            return rows > 0;
        }

        public async Task<bool> UpdatePesoSeccoAsync(int patientId, decimal pesoSecco)
        {
            const string sql = @"
                UPDATE patients 
                SET peso_secco = @PesoSecco
                WHERE patient_id = @PatientId";

            var rows = await ExecuteAsync(sql, new { PatientId = patientId, PesoSecco = pesoSecco });
            return rows > 0;
        }

        public async Task<bool> UpdateStatoAsync(int patientId, string stato)
        {
            const string sql = @"
                UPDATE patients 
                SET stato = @Stato
                WHERE patient_id = @PatientId";

            var rows = await ExecuteAsync(sql, new { PatientId = patientId, Stato = stato });
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM patients WHERE patient_id = @Id";
            var rows = await ExecuteAsync(sql, new { Id = id });
            return rows > 0;
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM patients";
            return await ExecuteScalarAsync<int>(sql);
        }
    }
}
