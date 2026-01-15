using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Esami di laboratorio
    /// </summary>
    public class LabTest
    {
        public int TestId { get; set; }
        public int PatientId { get; set; }
        public DateTime DataPrelievo { get; set; }
        public DateTime? DataReferto { get; set; }

        // Emocromo
        public decimal? Hb { get; set; } // emoglobina g/dL
        public decimal? Hct { get; set; } // ematocrito %
        public decimal? Rbc { get; set; } // globuli rossi M/uL
        public decimal? Wbc { get; set; } // globuli bianchi K/uL
        public decimal? Plt { get; set; } // piastrine K/uL

        // Funzionalità renale
        public decimal? Creatinina { get; set; } // mg/dL
        public decimal? Azotemia { get; set; } // mg/dL
        public decimal? UreaPre { get; set; } // mg/dL
        public decimal? UreaPost { get; set; } // mg/dL

        // Elettroliti
        public decimal? Sodio { get; set; } // mEq/L
        public decimal? Potassio { get; set; } // mEq/L
        public decimal? Calcio { get; set; } // mg/dL
        public decimal? Fosforo { get; set; } // mg/dL
        public decimal? Magnesio { get; set; } // mg/dL
        public decimal? Cloro { get; set; } // mEq/L

        // Metabolismo minerale
        public decimal? Pth { get; set; } // pg/mL
        public decimal? VitaminaD { get; set; } // ng/mL

        // Metabolismo del ferro
        public decimal? Ferritina { get; set; } // ng/mL
        public decimal? Transferrina { get; set; } // mg/dL
        public decimal? SaturazioneTransferrina { get; set; } // %
        public decimal? FerroSierico { get; set; } // mcg/dL

        // Funzionalità epatica
        public decimal? Alt { get; set; } // U/L
        public decimal? Ast { get; set; } // U/L
        public decimal? BilirubinaTotale { get; set; } // mg/dL
        public decimal? Albumina { get; set; } // g/dL
        public decimal? ProteineTotali { get; set; } // g/dL

        // Metabolismo lipidico
        public decimal? ColesteroloTotale { get; set; } // mg/dL
        public decimal? ColesteroloHdl { get; set; } // mg/dL
        public decimal? ColesteroloLdl { get; set; } // mg/dL
        public decimal? Trigliceridi { get; set; } // mg/dL

        // Glicemia
        public decimal? Glicemia { get; set; } // mg/dL
        public decimal? EmoglobinaGlicata { get; set; } // %

        // Coagulazione
        public decimal? Pt { get; set; } // secondi
        public decimal? Inr { get; set; }
        public decimal? Aptt { get; set; } // secondi

        // Sierologie
        public string Hbsag { get; set; } // negativo, positivo, non_eseguito
        public string HcvAb { get; set; } // negativo, positivo, non_eseguito
        public string HivAb { get; set; } // negativo, positivo, non_eseguito

        // PCR e infiammazione
        public decimal? Pcr { get; set; } // mg/L

        // Note e alert
        public string Note { get; set; }
        public bool AlertGenerato { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        // Proprietà computate
        public decimal? Urr => UreaPre.HasValue && UreaPost.HasValue && UreaPre.Value > 0
            ? ((UreaPre.Value - UreaPost.Value) / UreaPre.Value) * 100
            : null;
    }
}
