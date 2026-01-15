using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using DialysisManagement.Data;
using DialysisManagement.Utilities;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Servizio per backup e ripristino database usando mysqldump
    /// NON include definizione interfaccia (è in IBackupService.cs)
    /// </summary>
    public class BackupService : IBackupService
    {
        private readonly IDatabaseHelper _dbHelper;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly string _defaultBackupPath;
        private readonly string _mysqldumpPath;
        private readonly string _mysqlPath;

        public BackupService(
            IDatabaseHelper dbHelper,
            IEncryptionHelper encryptionHelper = null)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _encryptionHelper = encryptionHelper;

            // Percorso backup di default
            _defaultBackupPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Backups");

            // Percorsi mysqldump e mysql
            var mysqlBinPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Database", "MySQL", "bin");

            _mysqldumpPath = Path.Combine(mysqlBinPath, "mysqldump.exe");
            _mysqlPath = Path.Combine(mysqlBinPath, "mysql.exe");

            // Crea cartella backup se non esiste
            if (!Directory.Exists(_defaultBackupPath))
            {
                Directory.CreateDirectory(_defaultBackupPath);
            }
        }

        /// <summary>
        /// Crea backup del database usando mysqldump
        /// </summary>
        public async Task<string> CreateBackupAsync(string backupPath = null, bool compress = true, bool encrypt = false)
        {
            try
            {
                // Usa percorso default se non specificato
                if (string.IsNullOrWhiteSpace(backupPath))
                {
                    backupPath = _defaultBackupPath;
                }

                // Assicura che la directory esista
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }

                // Nome file backup con timestamp
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var backupFileName = $"dialysis_backup_{timestamp}.sql";
                var backupFilePath = Path.Combine(backupPath, backupFileName);

                // Estrai parametri connessione
                var connectionString = _dbHelper.GetConnectionString();
                var builder = new MySqlConnectionStringBuilder(connectionString);

                // Verifica se mysqldump esiste
                if (!File.Exists(_mysqldumpPath))
                {
                    // Fallback: usa metodo alternativo senza mysqldump
                    await CreateBackupWithoutMySqlDumpAsync(backupFilePath, builder);
                }
                else
                {
                    // Usa mysqldump
                    await CreateBackupWithMySqlDumpAsync(backupFilePath, builder);
                }

                // Comprimi se richiesto
                if (compress)
                {
                    backupFilePath = await CompressBackupAsync(backupFilePath);
                }

                // Cripta se richiesto
                if (encrypt && _encryptionHelper != null)
                {
                    backupFilePath = await EncryptBackupAsync(backupFilePath);
                }

                return backupFilePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la creazione del backup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Crea backup usando mysqldump.exe
        /// </summary>
        private async Task CreateBackupWithMySqlDumpAsync(string backupFilePath, MySqlConnectionStringBuilder builder)
        {
            var arguments = new StringBuilder();
            arguments.Append($"-u {builder.UserID} ");

            if (!string.IsNullOrEmpty(builder.Password))
            {
                arguments.Append($"-p\"{builder.Password}\" ");
            }

            arguments.Append($"-h {builder.Server} ");
            arguments.Append($"-P {builder.Port} ");
            arguments.Append("--single-transaction ");
            arguments.Append("--routines ");
            arguments.Append("--triggers ");
            arguments.Append("--add-drop-database ");
            arguments.Append($"--databases {builder.Database} ");
            arguments.Append($"--result-file=\"{backupFilePath}\"");

            var processInfo = new ProcessStartInfo
            {
                FileName = _mysqldumpPath,
                Arguments = arguments.ToString(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                var output = await process.StandardOutput.ReadToEndAsync();
                var error = await process.StandardError.ReadToEndAsync();

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Errore mysqldump: {error}");
                }
            }
        }

        /// <summary>
        /// Crea backup senza mysqldump (metodo alternativo)
        /// </summary>
        private async Task CreateBackupWithoutMySqlDumpAsync(string backupFilePath, MySqlConnectionStringBuilder builder)
        {
            var connectionString = builder.ToString();

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var sb = new StringBuilder();
                sb.AppendLine($"-- Dialysis Management System Backup");
                sb.AppendLine($"-- Data: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                sb.AppendLine($"-- Database: {builder.Database}");
                sb.AppendLine();
                sb.AppendLine($"DROP DATABASE IF EXISTS `{builder.Database}`;");
                sb.AppendLine($"CREATE DATABASE `{builder.Database}` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;");
                sb.AppendLine($"USE `{builder.Database}`;");
                sb.AppendLine();

                // Ottieni lista tabelle
                var tablesCommand = new MySqlCommand("SHOW TABLES", connection);
                var tables = new System.Collections.Generic.List<string>();

                using (var reader = await tablesCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }

                // Per ogni tabella, genera CREATE TABLE e INSERT
                foreach (var table in tables)
                {
                    // CREATE TABLE
                    var createCommand = new MySqlCommand($"SHOW CREATE TABLE `{table}`", connection);
                    using (var reader = await createCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sb.AppendLine($"DROP TABLE IF EXISTS `{table}`;");
                            sb.AppendLine(reader.GetString(1) + ";");
                            sb.AppendLine();
                        }
                    }

                    // INSERT DATA
                    var selectCommand = new MySqlCommand($"SELECT * FROM `{table}`", connection);
                    using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            var columnCount = reader.FieldCount;
                            var columnNames = new string[columnCount];

                            for (int i = 0; i < columnCount; i++)
                            {
                                columnNames[i] = reader.GetName(i);
                            }

                            sb.AppendLine($"INSERT INTO `{table}` ({string.Join(", ", columnNames.Select(c => $"`{c}`"))}) VALUES");

                            bool first = true;
                            while (await reader.ReadAsync())
                            {
                                if (!first) sb.Append(",");
                                sb.AppendLine();
                                sb.Append("(");

                                for (int i = 0; i < columnCount; i++)
                                {
                                    if (i > 0) sb.Append(", ");

                                    if (reader.IsDBNull(i))
                                    {
                                        sb.Append("NULL");
                                    }
                                    else
                                    {
                                        var value = reader.GetValue(i);
                                        sb.Append($"'{MySqlHelper.EscapeString(value.ToString())}'");
                                    }
                                }

                                sb.Append(")");
                                first = false;
                            }

                            sb.AppendLine(";");
                            sb.AppendLine();
                        }
                    }
                }

                // Scrivi su file
                await File.WriteAllTextAsync(backupFilePath, sb.ToString(), Encoding.UTF8);
            }
        }

        /// <summary>
        /// Ripristina database da backup
        /// </summary>
        public async Task<bool> RestoreBackupAsync(string backupFilePath)
        {
            try
            {
                if (!File.Exists(backupFilePath))
                {
                    throw new FileNotFoundException("File di backup non trovato", backupFilePath);
                }

                var fileToRestore = backupFilePath;

                // Decripta se necessario
                if (Path.GetExtension(backupFilePath).Equals(".encrypted", StringComparison.OrdinalIgnoreCase))
                {
                    if (_encryptionHelper == null)
                        throw new InvalidOperationException("EncryptionHelper non disponibile per decrittare");

                    fileToRestore = await DecryptBackupAsync(backupFilePath);
                }

                // Decomprimi se necessario
                if (Path.GetExtension(fileToRestore).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    fileToRestore = await DecompressBackupAsync(fileToRestore);
                }

                // Ripristina usando mysql.exe
                var connectionString = _dbHelper.GetConnectionString();
                var builder = new MySqlConnectionStringBuilder(connectionString);

                if (File.Exists(_mysqlPath))
                {
                    await RestoreWithMySqlExeAsync(fileToRestore, builder);
                }
                else
                {
                    await RestoreWithoutMySqlExeAsync(fileToRestore, builder);
                }

                // Pulisci file temporanei
                if (fileToRestore != backupFilePath && File.Exists(fileToRestore))
                {
                    File.Delete(fileToRestore);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante il ripristino del backup: {ex.Message}", ex);
            }
        }

        private async Task RestoreWithMySqlExeAsync(string backupFilePath, MySqlConnectionStringBuilder builder)
        {
            var arguments = new StringBuilder();
            arguments.Append($"-u {builder.UserID} ");

            if (!string.IsNullOrEmpty(builder.Password))
            {
                arguments.Append($"-p\"{builder.Password}\" ");
            }

            arguments.Append($"-h {builder.Server} ");
            arguments.Append($"-P {builder.Port} ");

            var processInfo = new ProcessStartInfo
            {
                FileName = _mysqlPath,
                Arguments = arguments.ToString(),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                var sql = await File.ReadAllTextAsync(backupFilePath);
                await process.StandardInput.WriteAsync(sql);
                process.StandardInput.Close();

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    var error = await process.StandardError.ReadToEndAsync();
                    throw new Exception($"Errore ripristino: {error}");
                }
            }
        }

        private async Task RestoreWithoutMySqlExeAsync(string backupFilePath, MySqlConnectionStringBuilder builder)
        {
            var sql = await File.ReadAllTextAsync(backupFilePath, Encoding.UTF8);
            var connectionString = builder.ToString();

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new MySqlCommand(sql, connection);
                command.CommandTimeout = 300; // 5 minuti
                await command.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Elimina backup più vecchi di X giorni
        /// </summary>
        public async Task<int> CleanOldBackupsAsync(int retentionDays)
        {
            try
            {
                if (!Directory.Exists(_defaultBackupPath))
                {
                    return 0;
                }

                var cutoffDate = DateTime.Now.AddDays(-retentionDays);
                var deletedCount = 0;

                await Task.Run(() =>
                {
                    var backupFiles = Directory.GetFiles(_defaultBackupPath, "dialysis_backup_*.*");

                    foreach (var file in backupFiles)
                    {
                        var fileInfo = new FileInfo(file);
                        if (fileInfo.CreationTime < cutoffDate)
                        {
                            File.Delete(file);
                            deletedCount++;
                        }
                    }
                });

                return deletedCount;
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore durante la pulizia dei backup: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Upload backup su cloud storage (placeholder)
        /// </summary>
        public async Task<bool> UploadToCloudAsync(string backupFilePath)
        {
            // TODO: Implementare upload specifico provider
            await Task.Delay(100);
            return true;
        }

        #region Helper Methods

        private async Task<string> CompressBackupAsync(string filePath)
        {
            var compressedPath = Path.ChangeExtension(filePath, ".zip");

            await Task.Run(() =>
            {
                using (var zipArchive = ZipFile.Open(compressedPath, ZipArchiveMode.Create))
                {
                    zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath), CompressionLevel.Optimal);
                }
            });

            File.Delete(filePath);
            return compressedPath;
        }

        private async Task<string> DecompressBackupAsync(string zipFilePath)
        {
            var extractPath = Path.Combine(Path.GetDirectoryName(zipFilePath), "temp_restore");

            if (Directory.Exists(extractPath))
                Directory.Delete(extractPath, true);

            await Task.Run(() =>
            {
                ZipFile.ExtractToDirectory(zipFilePath, extractPath);
            });

            var sqlFiles = Directory.GetFiles(extractPath, "*.sql");
            if (sqlFiles.Length == 0)
            {
                throw new FileNotFoundException("File SQL non trovato nell'archivio");
            }

            return sqlFiles[0];
        }

        private async Task<string> EncryptBackupAsync(string filePath)
        {
            var encryptedPath = filePath + ".encrypted";

            await Task.Run(() =>
            {
                var fileContent = File.ReadAllBytes(filePath);
                var encryptedContent = _encryptionHelper.Encrypt(fileContent);
                File.WriteAllBytes(encryptedPath, encryptedContent);
            });

            File.Delete(filePath);
            return encryptedPath;
        }

        private async Task<string> DecryptBackupAsync(string encryptedFilePath)
        {
            var decryptedPath = encryptedFilePath.Replace(".encrypted", "");

            await Task.Run(() =>
            {
                var encryptedContent = File.ReadAllBytes(encryptedFilePath);
                var decryptedContent = _encryptionHelper.Decrypt(encryptedContent);
                File.WriteAllBytes(decryptedPath, decryptedContent);
            });

            return decryptedPath;
        }

        #endregion
    }
}
