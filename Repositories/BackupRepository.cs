using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;
using NLog;

namespace DialysisManagement.Repositories
{
    public interface IBackupRepository
    {
        Task<BackupInfo> GetByIdAsync(int backupId);
        Task<IEnumerable<BackupInfo>> GetAllAsync();
        Task<int> AddAsync(BackupInfo backup);
        Task<bool> UpdateAsync(BackupInfo backup);
        Task<bool> DeleteAsync(int backupId);
        Task<IEnumerable<BackupInfo>> GetOlderThanAsync(int days);
    }

    public class BackupRepository : IBackupRepository
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly Logger _logger;

        public BackupRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<BackupInfo> GetByIdAsync(int backupId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = "SELECT * FROM backups WHERE BackupId = @BackupId";
                    return await connection.QueryFirstOrDefaultAsync<BackupInfo>(
                        sql, new { BackupId = backupId });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero backup {backupId}");
                throw;
            }
        }

        public async Task<IEnumerable<BackupInfo>> GetAllAsync()
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = "SELECT * FROM backups ORDER BY CreatedDate DESC";
                    return await connection.QueryAsync<BackupInfo>(sql);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore recupero tutti backup");
                throw;
            }
        }

        public async Task<int> AddAsync(BackupInfo backup)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        INSERT INTO backups 
                        (FileName, FilePath, CreatedDate, FileSizeMB, IsCompressed, 
                         IsEncrypted, UploadedToCloud, CloudProvider, CloudFileId, 
                         CloudUploadDate, BackupType, Notes, FileHash, CreatedBy)
                        VALUES 
                        (@FileName, @FilePath, @CreatedDate, @FileSizeMB, @IsCompressed, 
                         @IsEncrypted, @UploadedToCloud, @CloudProvider, @CloudFileId, 
                         @CloudUploadDate, @BackupType, @Notes, @FileHash, @CreatedBy);
                        SELECT LAST_INSERT_ID();";

                    return await connection.ExecuteScalarAsync<int>(sql, backup);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore inserimento backup");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(BackupInfo backup)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = @"
                        UPDATE backups SET
                            UploadedToCloud = @UploadedToCloud,
                            CloudProvider = @CloudProvider,
                            CloudFileId = @CloudFileId,
                            CloudUploadDate = @CloudUploadDate,
                            Notes = @Notes,
                            RestoreSuccess = @RestoreSuccess,
                            RestoreDate = @RestoreDate
                        WHERE BackupId = @BackupId";

                    var rows = await connection.ExecuteAsync(sql, backup);
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore aggiornamento backup {backup.BackupId}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int backupId)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var sql = "DELETE FROM backups WHERE BackupId = @BackupId";
                    var rows = await connection.ExecuteAsync(sql, new { BackupId = backupId });
                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione backup {backupId}");
                throw;
            }
        }

        public async Task<IEnumerable<BackupInfo>> GetOlderThanAsync(int days)
        {
            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var cutoffDate = DateTime.Now.AddDays(-days);
                    var sql = @"
                        SELECT * FROM backups 
                        WHERE CreatedDate < @CutoffDate 
                        AND UploadedToCloud = 0
                        ORDER BY CreatedDate";

                    return await connection.QueryAsync<BackupInfo>(
                        sql, new { CutoffDate = cutoffDate });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore recupero backup vecchi");
                throw;
            }
        }
    }
}
