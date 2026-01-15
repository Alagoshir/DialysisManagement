using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Prescrizione dialitica
    /// </summary>
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int PatientId { get; set; }
        public DateTime DataPrescrizione { get; set; }
        public DateTime DataInizioValidita { get; set; }
        public DateTime? DataFineValidita { get; set; }
        public bool Attiva { get; set; }

        // Parametri dialisi
        public int FrequenzaSettimanale { get; set; } = 3;
        public int DurataSeduta { get; set; } = 240; // minuti
        public string TipoTrattamento { get; set; } // HD, HDF, HF
        public decimal? PesoSeccoTarget { get; set; }
        public int? UfTarget { get; set; } // ml

        // Parametri tecnici
        public int? QbPrescritto { get; set; } // ml/min
        public int? QdPrescritto { get; set; } // ml/min
        public string TipoFiltro { get; set; }

        // Bagno dialitico
        public decimal? Sodio { get; set; }
        public decimal? Potassio { get; set; }
        public decimal? Calcio { get; set; }
        public decimal? Bicarbonato { get; set; }
        public decimal? Temperatura { get; set; }

        // Anticoagulazione
        public string Anticoagulante { get; set; }
        public string DosaggioAnticoagulante { get; set; }

        // Note
        public string Note { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        // Proprietà computate
        public bool IsValida => Attiva &&
            DataInizioValidita <= DateTime.Now &&
            (!DataFineValidita.HasValue || DataFineValidita.Value >= DateTime.Now);
    }
}
