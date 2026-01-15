using System;
using System.ComponentModel.DataAnnotations;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Rappresenta un file allegato ad un esame strumentale
    /// </summary>
    public class ExamAttachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Required]
        public int ExamId { get; set; }

        /// <summary>
        /// Nome file originale
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        /// <summary>
        /// Percorso file sul filesystem (crittografato)
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; }

        /// <summary>
        /// Tipo MIME
        /// </summary>
        [MaxLength(100)]
        public string MimeType { get; set; }

        /// <summary>
        /// Dimensione file in bytes
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// File crittografato
        /// </summary>
        public bool IsEncrypted { get; set; }

        /// <summary>
        /// Hash SHA256 del file per verifica integrità
        /// </summary>
        [MaxLength(64)]
        public string FileHash { get; set; }

        /// <summary>
        /// Descrizione/note sul file
        /// </summary>
        [MaxLength(500)]
        public string Description { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        public int? UploadedBy { get; set; }

        // Navigation properties
        public virtual InstrumentalExam Exam { get; set; }
    }
}
