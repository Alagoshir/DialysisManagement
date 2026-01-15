using System.Collections.Generic;
using System.Threading.Tasks;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Interfaccia per gestione file
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Salva file su disco
        /// </summary>
        Task<string> SaveFileAsync(byte[] fileData, string fileName, string destinationPath);

        /// <summary>
        /// Carica file da disco
        /// </summary>
        Task<byte[]> LoadFileAsync(string filePath);

        /// <summary>
        /// Elimina file
        /// </summary>
        Task<bool> DeleteFileAsync(string filePath);

        /// <summary>
        /// Verifica se file esiste
        /// </summary>
        bool FileExists(string filePath);

        /// <summary>
        /// Ottiene dimensione file in byte
        /// </summary>
        long GetFileSize(string filePath);

        /// <summary>
        /// Ottiene estensione file
        /// </summary>
        string GetFileExtension(string fileName);

        /// <summary>
        /// Valida estensione file
        /// </summary>
        bool IsValidFileExtension(string fileName, string[] allowedExtensions);

        /// <summary>
        /// Calcola hash MD5 del file
        /// </summary>
        Task<string> CalculateFileMd5HashAsync(string filePath);

        /// <summary>
        /// Copia file
        /// </summary>
        Task<bool> CopyFileAsync(string sourceFilePath, string destinationFilePath);

        /// <summary>
        /// Sposta file
        /// </summary>
        Task<bool> MoveFileAsync(string sourceFilePath, string destinationFilePath);

        /// <summary>
        /// Ottiene lista file in directory
        /// </summary>
        List<string> GetFilesInDirectory(string directoryPath, string searchPattern = "*.*");

        /// <summary>
        /// Crea directory se non esiste
        /// </summary>
        void EnsureDirectoryExists(string directoryPath);

        /// <summary>
        /// Pulisce directory (elimina tutti i file)
        /// </summary>
        Task<int> CleanDirectoryAsync(string directoryPath);

        /// <summary>
        /// Ottiene nome file sicuro (rimuove caratteri non validi)
        /// </summary>
        string GetSafeFileName(string fileName);
    }
}
