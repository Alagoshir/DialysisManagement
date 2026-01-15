using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Paziente in dialisi
    /// </summary>
    public class Patient
    {
        public int PatientId { get; set; }
        public string CodiceFiscale { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascita { get; set; }
        public string LuogoNascita { get; set; }
        public string Sesso { get; set; } // M, F
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string Cellulare { get; set; }
        public string Email { get; set; }

        // Dati sanitari
        public string CodiceSanitario { get; set; }
        public string MedicoCurante { get; set; }
        public string GruppoSanguigno { get; set; }
        public decimal? PesoSecco { get; set; }
        public int? Altezza { get; set; }
        public decimal? Bmi { get; set; }

        // Contumaciali
        public bool HbsagPositive { get; set; }
        public bool HcvPositive { get; set; }
        public bool HivPositive { get; set; }

        // Lista trapianto
        public bool InListaTrapianto { get; set; }
        public DateTime? DataInserimentoLista { get; set; }

        // Stato paziente
        public string Stato { get; set; } // attivo, sospeso, deceduto, trapiantato, trasferito
        public DateTime? DataIngresso { get; set; }
        public DateTime? DataUscita { get; set; }
        public string MotivoUscita { get; set; }

        // GDPR
        public bool ConsensoTrattamentoDati { get; set; }
        public DateTime? DataConsenso { get; set; }

        // Foto e QR
        public string FotoPath { get; set; }
        public string QrCodePath { get; set; }

        // Note
        public string Note { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        // Proprietà computate
        public string NomeCognome => $"{Nome} {Cognome}";
        public int Eta => DateTime.Now.Year - DataNascita.Year -
            (DateTime.Now.DayOfYear < DataNascita.DayOfYear ? 1 : 0);
        public bool IsContumaciale => HbsagPositive || HcvPositive || HivPositive;
        public bool IsAttivo => Stato == "attivo";
    }
}
