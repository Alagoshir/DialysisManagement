using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Vaccinazione
    /// </summary>
    public class Vaccination
    {
        public int VaccinationId { get; set; }
        public int PatientId { get; set; }
        public string TipoVaccino { get; set; } // HBV, Influenza, COVID-19, Pneumococco, Altro
        public string NomeVaccino { get; set; }
        public string Lotto { get; set; }
        public DateTime DataSomministrazione { get; set; }
        public int? DoseNumero { get; set; }
        public DateTime? DataRichiamo { get; set; }
        public string SedeInoculazione { get; set; }
        public string ReazioniAvverse { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        // Proprietà computate
        public bool RichiamoScaduto => DataRichiamo.HasValue && DataRichiamo.Value < DateTime.Now;
        public int? GiorniAlRichiamo => DataRichiamo.HasValue
            ? (DataRichiamo.Value - DateTime.Now).Days
            : null;
    }
}
