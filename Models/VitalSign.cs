using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Parametri vitali intra-dialisi
    /// </summary>
    public class VitalSign
    {
        public int VitalId { get; set; }
        public int SessionId { get; set; }
        public TimeSpan OraRilevazione { get; set; }
        public int? PaSistolica { get; set; }
        public int? PaDiastolica { get; set; }
        public int? FrequenzaCardiaca { get; set; }
        public decimal? Temperatura { get; set; }
        public int? SaturazioneO2 { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        // Proprietà computate
        public string PaFormattata => PaSistolica.HasValue && PaDiastolica.HasValue
            ? $"{PaSistolica}/{PaDiastolica}"
            : "N/D";
    }
}
