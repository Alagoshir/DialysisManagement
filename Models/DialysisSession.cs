using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Seduta dialitica
    /// </summary>
    public class DialysisSession
    {
        public int SessionId { get; set; }
        public int PatientId { get; set; }
        public DateTime DataSeduta { get; set; }
        public string Turno { get; set; } // mattina, pomeriggio, sera

        // Assegnazione risorse
        public int? RoomId { get; set; }
        public int? StationId { get; set; }
        public int? DeviceId { get; set; }
        public int? MedicoId { get; set; }
        public int? InfermiereId { get; set; }

        // Orari
        public TimeSpan? OraInizio { get; set; }
        public TimeSpan? OraFine { get; set; }
        public int? DurataEffettiva { get; set; } // minuti
        public int DurataPrevista { get; set; } = 240; // minuti

        // Accesso vascolare
        public int? AccessId { get; set; }

        // Parametri pre-dialisi
        public decimal? PesoPre { get; set; }
        public int? PaSistolicaPre { get; set; }
        public int? PaDiastolicaPre { get; set; }
        public int? FcPre { get; set; }
        public decimal? TemperaturaPre { get; set; }

        // Parametri post-dialisi
        public decimal? PesoPost { get; set; }
        public int? PaSistolicaPost { get; set; }
        public int? PaDiastolicaPost { get; set; }
        public int? FcPost { get; set; }
        public decimal? TemperaturaPost { get; set; }

        // Ultrafiltrazione
        public int? UfProgrammata { get; set; } // ml
        public int? UfEffettuata { get; set; } // ml

        // Parametri tecnici
        public string TipoTrattamento { get; set; } // HD, HDF, HF, Altro
        public string TipoFiltro { get; set; }
        public int? SuperficieFiltro { get; set; }
        public int? Qb { get; set; } // flusso sangue ml/min
        public int? Qd { get; set; } // flusso dialisato ml/min
        public decimal? Bicarbonato { get; set; }
        public decimal? Sodio { get; set; }
        public decimal? Calcio { get; set; }
        public decimal? Potassio { get; set; }

        // Anticoagulazione
        public string Anticoagulante { get; set; }
        public string DoseAnticoagulante { get; set; }

        // Kt/V
        public decimal? Ktv { get; set; }
        public decimal? Urr { get; set; } // Urea Reduction Ratio

        // Complicanze
        public string Complicanze { get; set; }

        // Stato seduta
        public string Stato { get; set; } // programmata, in_corso, completata, annullata, interrotta
        public string MotivoAnnullamento { get; set; }

        // Note
        public string Note { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        // Proprietà computate
        public decimal? CaloPeso => PesoPre.HasValue && PesoPost.HasValue
            ? PesoPre.Value - PesoPost.Value
            : null;
        public bool IsCompletata => Stato == "completata";
        public bool IsInCorso => Stato == "in_corso";
    }
}
