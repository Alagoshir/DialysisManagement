using System;
using System.ComponentModel.DataAnnotations;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Rappresenta un accesso vascolare per dialisi
    /// </summary>
    public class VascularAccess
    {
        [Key]
        public int AccessId { get; set; }

        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Tipo: FAV (Fistola Artero-Venosa), CVC (Catetere Venoso Centrale), Protesi
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string TipoAccesso { get; set; }

        /// <summary>
        /// Sede anatomica dell'accesso (es: "Avambraccio sinistro", "Giugulare destra")
        /// </summary>
        [MaxLength(200)]
        public string Sede { get; set; }

        /// <summary>
        /// Data di creazione/posizionamento dell'accesso
        /// </summary>
        [Required]
        public DateTime DataCreazione { get; set; }

        /// <summary>
        /// Data di rimozione/dismissione dell'accesso
        /// </summary>
        public DateTime? DataRimozione { get; set; }

        /// <summary>
        /// Stato: attivo, rimosso, malfunzionante, infetto
        /// </summary>
        [MaxLength(50)]
        public string Stato { get; set; }

        /// <summary>
        /// Chirurgo che ha eseguito l'intervento
        /// </summary>
        [MaxLength(200)]
        public string Chirurgo { get; set; }

        /// <summary>
        /// Ospedale dove è stato creato l'accesso
        /// </summary>
        [MaxLength(200)]
        public string Ospedale { get; set; }

        /// <summary>
        /// Portata (flusso) in ml/min per FAV
        /// </summary>
        public int? Portata { get; set; }

        /// <summary>
        /// Flebite/Infezione
        /// </summary>
        public bool Infezione { get; set; }

        /// <summary>
        /// Stenosi
        /// </summary>
        public bool Stenosi { get; set; }

        /// <summary>
        /// Trombosi
        /// </summary>
        public bool Trombosi { get; set; }

        /// <summary>
        /// Data ultima valutazione
        /// </summary>
        public DateTime? DataUltimaValutazione { get; set; }

        /// <summary>
        /// Note cliniche
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Utente che ha creato il record
        /// </summary>
        public int? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
    }
}
