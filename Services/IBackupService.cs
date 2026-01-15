using System.Threading.Tasks;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Interfaccia per servizio backup database
    /// </summary>
    public interface IBackupService
    {
        /// <summary>
        /// Crea backup database
        /// </summary>
        /// <param name="backupPath">Percorso dove salvare il backup (null = default)</param>
        /// <param name="compress">Comprimi il backup in ZIP</param>
        /// <param name="encrypt">Cripta il backup</param>
        /// <returns>Percorso completo del file backup creato</returns>
        Task<string> CreateBackupAsync(string backupPath = null, bool compress = true, bool encrypt = false);

        /// <summary>
        /// Ripristina database da file backup
        /// </summary>
        /// <param name="backupFilePath">Percorso file backup da ripristinare</param>
        /// <returns>True se ripristino completato con successo</returns>
        Task<bool> RestoreBackupAsync(string backupFilePath);

        /// <summary>
        /// Elimina backup più vecchi di X giorni
        /// </summary>
        /// <param name="retentionDays">Numero giorni di retention</param>
        /// <returns>Numero di backup eliminati</returns>
        Task<int> CleanOldBackupsAsync(int retentionDays);

        /// <summary>
        /// Upload backup su cloud storage
        /// </summary>
        /// <param name="backupFilePath">Percorso file backup da caricare</param>
        /// <returns>True se upload completato con successo</returns>
        Task<bool> UploadToCloudAsync(string backupFilePath);
    }
}
