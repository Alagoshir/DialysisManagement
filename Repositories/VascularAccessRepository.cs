using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;
using MySql.Data.MySqlClient;
using NLog;

namespace DialysisManagement.Repositories
{
    public class VascularAccessRepository : IVascularAccessRepository
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly Logger _logger;

        public VascularAccessRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<VascularAccess> GetByIdAsync(int accessId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM vascular_accesses 
                        WHERE AccessId = @AccessId";

                    return await connection.QueryFirstOrDefaultAsync<VascularAccess>(
                        sql, new { AccessId = accessId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero accesso vascolare {accessId}");
                throw;
            }
        }

        public async Task<IEnumerable<VascularAccess>> GetAllAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM vascular_accesses 
                        ORDER BY CreatedDate DESC";

                    return await connection.QueryAsync<VascularAccess>(sql);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore recupero tutti accessi vascolari");
                throw;
            }
        }

        public async Task<IEnumerable<VascularAccess>> GetByPatientIdAsync(int patientId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        SELECT * FROM vascular_accesses 
                        WHERE PatientId = @PatientId 
                        ORDER BY DataCreazione DESC";

                    return await connection.QueryAsync<VascularAccess>(
                        sql, new { PatientId = patientId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero accessi paziente {patientId}");
                throw;
            }
        }

        public async Task<int> AddAsync(VascularAccess access)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        INSERT INTO vascular_accesses 
                        (PatientId, TipoAccesso, Sede, DataCreazione, DataRimozione, 
                         Stato, Chirurgo, Ospedale, Portata, Infezione, Stenosi, 
                         Trombosi, DataUltimaValutazione, Note, CreatedBy, CreatedDate)
                        VALUES 
                        (@PatientId, @TipoAccesso, @Sede, @DataCreazione, @DataRimozione, 
                         @Stato, @Chirurgo, @Ospedale, @Portata, @Infezione, @Stenosi, 
                         @Trombosi, @DataUltimaValutazione, @Note, @CreatedBy, @CreatedDate);
                        SELECT LAST_INSERT_ID();";

                    return await connection.ExecuteScalarAsync<int>(sql, access);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore inserimento accesso vascolare");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(VascularAccess access)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        UPDATE vascular_accesses SET
                            TipoAccesso = @TipoAccesso,
                            Sede = @Sede,
                            DataCreazione = @DataCreazione,
                            DataRimozione = @DataRimozione,
                            Stato = @Stato,
                            Chirurgo = @Chirurgo,
                            Ospedale = @Ospedale,
                            Portata = @Portata,
                            Infezione = @Infezione,
                            Stenosi = @Stenosi,
                            Trombosi = @Trombosi,
                            DataUltimaValutazione = @DataUltimaValutazione,
                            Note = @Note,
                            ModifiedBy = @ModifiedBy,
                            ModifiedDate = @ModifiedDate
                        WHERE AccessId = @AccessId";

                    var rows = await connection.ExecuteAsync(sql, access);
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore aggiornamento accesso {access.AccessId}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int accessId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = "DELETE FROM vascular_accesses WHERE AccessId = @AccessId";
                    var rows = await connection.ExecuteAsync(sql, new { AccessId = accessId });
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione accesso {accessId}");
                throw;
            }
        }
    }
}
