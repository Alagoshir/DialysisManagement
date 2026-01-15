using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Informazioni su un backup del database
    /// </summary>
    public class BackupInfo
    {
        /// <summary>
        /// ID backup (se salvato in database)
        /// </summary>
        public int BackupId { get; set; }

        /// <summary>
        /// Nome file backup
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Percorso completo file backup
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Data e ora creazione backup
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Dimensione file in byte
        /// </summary>
        public long FileSizeBytes { get; set; }

        /// <summary>
        /// Dimensione file formattata (es: "15.3 MB")
        /// </summary>
        public string FileSizeFormatted
        {
            get
            {
                return FormatFileSize(FileSizeBytes);
            }
        }

        /// <summary>
        /// Tipo backup (Manual, Automatic, Scheduled)
        /// </summary>
        public string BackupType { get; set; }

        /// <summary>
        /// Indica se il backup è compresso
        /// </summary>
        public bool IsCompressed { get; set; }

        /// <summary>
        /// Indica se il backup è crittografato
        /// </summary>
        public bool IsEncrypted { get; set; }

        /// <summary>
        /// Note/descrizione backup
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Hash MD5 del file (per integrità)
        /// </summary>
        public string FileHash { get; set; }

        /// <summary>
        /// Utente che ha creato il backup
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Indica se il backup è stato caricato su cloud
        /// </summary>
        public bool IsUploadedToCloud { get; set; }

        /// <summary>
        /// Data upload su cloud
        /// </summary>
        public DateTime? CloudUploadDate { get; set; }

        /// <summary>
        /// Provider cloud (GoogleDrive, OneDrive, Dropbox, FTP)
        /// </summary>
        public string CloudProvider { get; set; }

        /// <summary>
        /// Stato backup
        /// </summary>
        public BackupStatus Status { get; set; }

        /// <summary>
        /// Messaggio errore (se fallito)
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Versione database al momento del backup
        /// </summary>
        public string DatabaseVersion { get; set; }

        /// <summary>
        /// Numero record totali nel backup
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        public BackupInfo()
        {
            CreatedDate = DateTime.Now;
            BackupType = "Manual";
            Status = BackupStatus.Completed;
        }

        /// <summary>
        /// Formatta dimensione file in formato leggibile
        /// </summary>
        private string FormatFileSize(long bytes)
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

        /// <summary>
        /// Restituisce rappresentazione stringa
        /// </summary>
        public override string ToString()
        {
            return $"{FileName} - {CreatedDate:dd/MM/yyyy HH:mm} - {FileSizeFormatted}";
        }
    }

    /// <summary>
    /// Stato backup
    /// </summary>
    public enum BackupStatus
    {
        /// <summary>
        /// In corso
        /// </summary>
        InProgress,

        /// <summary>
        /// Completato con successo
        /// </summary>
        Completed,

        /// <summary>
        /// Fallito
        /// </summary>
        Failed,

        /// <summary>
        /// Annullato
        /// </summary>
        Cancelled
    }
}
