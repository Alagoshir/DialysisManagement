namespace DialysisManagement.Models
{
    /// <summary>
    /// Opzioni per la creazione di un backup
    /// </summary>
    public class BackupOptions
    {
        public bool Compress { get; set; } = true;

        public bool Encrypt { get; set; } = true;

        public bool UploadToCloud { get; set; } = false;

        public string CloudProvider { get; set; }

        public string Notes { get; set; }

        public string BackupType { get; set; } = "manual";
    }
}
