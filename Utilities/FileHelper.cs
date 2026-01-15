using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per gestione file
    /// </summary>
    public class FileHelper : IFileHelper
    {
        private readonly IEncryptionHelper _encryptionHelper;

        // Estensioni file consentite di default
        private static readonly string[] DefaultAllowedExtensions = new[]
        {
            ".pdf", ".jpg", ".jpeg", ".png", ".gif", ".bmp",
            ".doc", ".docx", ".xls", ".xlsx", ".txt",
            ".dcm", ".zip", ".rar"
        };

        // Dimensione massima file (50 MB)
        private const long MaxFileSizeBytes = 52428800; // 50 * 1024 * 1024

        public FileHelper(IEncryptionHelper encryptionHelper = null)
        {
            _encryptionHelper = encryptionHelper;
        }

        #region File Save/Load

        /// <summary>
        /// Salva file su disco
        /// </summary>
        public async Task<string> SaveFileAsync(byte[] fileData, string fileName, string destinationPath)
        {
            if (fileData == null || fileData.Length == 0)
                throw new ArgumentException("I dati del file non possono essere vuoti", nameof(fileData));

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Il nome del file non può essere vuoto", nameof(fileName));

            if (string.IsNullOrWhiteSpace(destinationPath))
                throw new ArgumentException("Il percorso di destinazione non può essere vuoto", nameof(destinationPath));

            try
            {
                // Verifica dimensione file
                if (fileData.Length > MaxFileSizeBytes)
                {
                    throw new InvalidOperationException(
                        $"Il file supera la dimensione massima consentita di {MaxFileSizeBytes / 1024 / 1024} MB");
                }

                // Crea directory se non esiste
                EnsureDirectoryExists(destinationPath);

                // Nome file sicuro
                var safeFileName = GetSafeFileName(fileName);

                // Path completo
                var fullPath = Path.Combine(destinationPath, safeFileName);

                // Se file esiste, aggiungi timestamp
                if (File.Exists(fullPath))
                {
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(safeFileName);
                    var extension = Path.GetExtension(safeFileName);
                    var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    safeFileName = $"{fileNameWithoutExt}_{timestamp}{extension}";
                    fullPath = Path.Combine(destinationPath, safeFileName);
                }

                // Salva file
                await File.WriteAllBytesAsync(fullPath, fileData);

                return fullPath;
            }
            catch (Exception ex)
            {
                throw new IOException($"Errore durante il salvataggio del file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Carica file da disco
        /// </summary>
        public async Task<byte[]> LoadFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Il percorso del file non può essere vuoto", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File non trovato", filePath);

            try
            {
                return await File.ReadAllBytesAsync(filePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Errore durante il caricamento del file: {ex.Message}", ex);
            }
        }

        #endregion

        #region File Operations

        /// <summary>
        /// Elimina file
        /// </summary>
        public async Task<bool> DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            try
            {
                await Task.Run(() =>
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Copia file
        /// </summary>
        public async Task<bool> CopyFileAsync(string sourceFilePath, string destinationFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath))
                throw new ArgumentException("Il percorso di origine non può essere vuoto", nameof(sourceFilePath));

            if (string.IsNullOrWhiteSpace(destinationFilePath))
                throw new ArgumentException("Il percorso di destinazione non può essere vuoto", nameof(destinationFilePath));

            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("File di origine non trovato", sourceFilePath);

            try
            {
                await Task.Run(() =>
                {
                    // Crea directory di destinazione se non esiste
                    var destinationDir = Path.GetDirectoryName(destinationFilePath);
                    EnsureDirectoryExists(destinationDir);

                    File.Copy(sourceFilePath, destinationFilePath, overwrite: true);
                });

                return true;
            }
            catch (Exception ex)
            {
                throw new IOException($"Errore durante la copia del file: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sposta file
        /// </summary>
        public async Task<bool> MoveFileAsync(string sourceFilePath, string destinationFilePath)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath))
                throw new ArgumentException("Il percorso di origine non può essere vuoto", nameof(sourceFilePath));

            if (string.IsNullOrWhiteSpace(destinationFilePath))
                throw new ArgumentException("Il percorso di destinazione non può essere vuoto", nameof(destinationFilePath));

            if (!File.Exists(sourceFilePath))
                throw new FileNotFoundException("File di origine non trovato", sourceFilePath);

            try
            {
                await Task.Run(() =>
                {
                    // Crea directory di destinazione se non esiste
                    var destinationDir = Path.GetDirectoryName(destinationFilePath);
                    EnsureDirectoryExists(destinationDir);

                    // Elimina file destinazione se esiste
                    if (File.Exists(destinationFilePath))
                    {
                        File.Delete(destinationFilePath);
                    }

                    File.Move(sourceFilePath, destinationFilePath);
                });

                return true;
            }
            catch (Exception ex)
            {
                throw new IOException($"Errore durante lo spostamento del file: {ex.Message}", ex);
            }
        }

        #endregion

        #region File Info

        /// <summary>
        /// Verifica se file esiste
        /// </summary>
        public bool FileExists(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            return File.Exists(filePath);
        }

        /// <summary>
        /// Ottiene dimensione file in byte
        /// </summary>
        public long GetFileSize(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File non trovato", filePath);

            var fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        /// <summary>
        /// Ottiene estensione file
        /// </summary>
        public string GetFileExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return string.Empty;

            return Path.GetExtension(fileName).ToLowerInvariant();
        }

        /// <summary>
        /// Valida estensione file
        /// </summary>
        public bool IsValidFileExtension(string fileName, string[] allowedExtensions = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var extension = GetFileExtension(fileName);
            var extensionsToCheck = allowedExtensions ?? DefaultAllowedExtensions;

            return extensionsToCheck.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Calcola hash MD5 del file
        /// </summary>
        public async Task<string> CalculateFileMd5HashAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File non trovato", filePath);

            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        var hash = await Task.Run(() => md5.ComputeHash(stream));
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"Errore durante il calcolo dell'hash: {ex.Message}", ex);
            }
        }

        #endregion

        #region Directory Operations

        /// <summary>
        /// Ottiene lista file in directory
        /// </summary>
        public List<string> GetFilesInDirectory(string directoryPath, string searchPattern = "*.*")
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return new List<string>();

            if (!Directory.Exists(directoryPath))
                return new List<string>();

            try
            {
                return Directory.GetFiles(directoryPath, searchPattern).ToList();
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// Crea directory se non esiste
        /// </summary>
        public void EnsureDirectoryExists(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("Il percorso della directory non può essere vuoto", nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Pulisce directory (elimina tutti i file)
        /// </summary>
        public async Task<int> CleanDirectoryAsync(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                return 0;

            if (!Directory.Exists(directoryPath))
                return 0;

            try
            {
                var deletedCount = 0;

                await Task.Run(() =>
                {
                    var files = Directory.GetFiles(directoryPath);
                    foreach (var file in files)
                    {
                        try
                        {
                            File.Delete(file);
                            deletedCount++;
                        }
                        catch
                        {
                            // Ignora errori su singoli file
                        }
                    }
                });

                return deletedCount;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion

        #region Utility

        /// <summary>
        /// Ottiene nome file sicuro (rimuove caratteri non validi)
        /// </summary>
        public string GetSafeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Il nome del file non può essere vuoto", nameof(fileName));

            // Rimuove caratteri non validi
            var invalidChars = Path.GetInvalidFileNameChars();
            var safeFileName = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

            // Rimuove spazi multipli
            safeFileName = Regex.Replace(safeFileName, @"\s+", " ");

            // Limita lunghezza (max 200 caratteri)
            if (safeFileName.Length > 200)
            {
                var extension = Path.GetExtension(safeFileName);
                var nameWithoutExt = Path.GetFileNameWithoutExtension(safeFileName);
                nameWithoutExt = nameWithoutExt.Substring(0, 200 - extension.Length);
                safeFileName = nameWithoutExt + extension;
            }

            return safeFileName.Trim();
        }

        /// <summary>
        /// Ottiene MIME type da estensione file
        /// </summary>
        public string GetMimeType(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "application/octet-stream";

            var extension = GetFileExtension(fileName);

            var mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { ".pdf", "application/pdf" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".bmp", "image/bmp" },
                { ".doc", "application/msword" },
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".txt", "text/plain" },
                { ".dcm", "application/dicom" },
                { ".zip", "application/zip" },
                { ".rar", "application/x-rar-compressed" }
            };

            return mimeTypes.TryGetValue(extension, out var mimeType)
                ? mimeType
                : "application/octet-stream";
        }

        /// <summary>
        /// Formatta dimensione file in formato leggibile
        /// </summary>
        public string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }

        #endregion
    }
}
