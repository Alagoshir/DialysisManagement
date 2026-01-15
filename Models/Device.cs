using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Dispositivo (monitor dialisi)
    /// </summary>
    public class Device
    {
        public int DeviceId { get; set; }
        public int? StationId { get; set; }
        public string TipoDispositivo { get; set; } // Monitor, Osmosi, Altro
        public string Marca { get; set; }
        public string Modello { get; set; }
        public string Matricola { get; set; }
        public int? AnnoAcquisto { get; set; }
        public DateTime? DataInstallazione { get; set; }
        public DateTime? DataDismissione { get; set; }
        public string Stato { get; set; } // operativo, manutenzione, guasto, dismesso
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Proprietà computate
        public bool IsOperativo => Stato == "operativo";
        public int? AnniServizio => AnnoAcquisto.HasValue
            ? DateTime.Now.Year - AnnoAcquisto.Value
            : null;
    }
}
