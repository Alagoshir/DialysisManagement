using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Farmaco
    /// </summary>
    public class Medication
    {
        public int MedicationId { get; set; }
        public int PatientId { get; set; }
        public string NomeFarmaco { get; set; }
        public string PrincipioAttivo { get; set; }
        public string ViaSomministrazione { get; set; } // intra_dialitica, extra_dialitica, orale, intramuscolare, sottocutanea
        public string Dose { get; set; }
        public string UnitaMisura { get; set; }
        public string Frequenza { get; set; }
        public DateTime DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
        public bool Attivo { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        // Proprietà computate
        public string DoseCompleta => !string.IsNullOrEmpty(Dose) && !string.IsNullOrEmpty(UnitaMisura)
            ? $"{Dose} {UnitaMisura}"
            : Dose ?? "N/D";
    }
}
