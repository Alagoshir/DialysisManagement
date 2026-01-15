using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Rappresenta un esame strumentale (ECG, eco, RX, TAC, RMN, etc.)
    /// </summary>
    public class InstrumentalExam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Tipo esame: ECG, Ecocardiogramma, Ecografia, RX, TAC, RMN, etc.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string TipoEsame { get; set; }

        /// <summary>
        /// Data esecuzione esame
        /// </summary>
        [Required]
        public DateTime DataEsecuzione { get; set; }

        /// <summary>
        /// Data referto
        /// </summary>
        public DateTime? DataReferto { get; set; }

        /// <summary>
        /// Medico richiedente
        /// </summary>
        [MaxLength(200)]
        public string Medico { get; set; }

        /// <summary>
        /// Indicazione clinica/motivo richiesta
        /// </summary>
        [MaxLength(500)]
        public string IndicazioneClinica { get; set; }

        /// <summary>
        /// Esito/referto testuale
        /// </summary>
        public string Referto { get; set; }

        /// <summary>
        /// Esame eseguito internamente
        /// </summary>
        public bool EsameInterno { get; set; }

        /// <summary>
        /// Codice nomenclatore per SDO (se esame interno)
        /// </summary>
        [MaxLength(50)]
        public string CodiceNomenclatore { get; set; }

        /// <summary>
        /// Centro/ospedale dove è stato eseguito
        /// </summary>
        [MaxLength(200)]
        public string CentroEsecuzione { get; set; }

        /// <summary>
        /// Urgente
        /// </summary>
        public bool Urgente { get; set; }

        /// <summary>
        /// Esame da ripetere (flag)
        /// </summary>
        public bool DaRipetere { get; set; }

        /// <summary>
        /// Data scadenza validità esame
        /// </summary>
        public DateTime? DataScadenza { get; set; }

        /// <summary>
        /// Note
        /// </summary>
        public string Note { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual ICollection<ExamAttachment> Attachments { get; set; }
    }
}
