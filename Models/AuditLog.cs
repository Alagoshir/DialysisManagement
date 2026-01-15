using System;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Model per audit log
    /// </summary>
    public class AuditLog
    {
        /// <summary>
        /// ID log
        /// </summary>
        public int LogId { get; set; }

        /// <summary>
        /// ID utente che ha eseguito l'azione
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Username utente
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Tipo azione (INSERT, UPDATE, DELETE, LOGIN, LOGOUT, etc.)
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Tipo entità modificata (Patient, Session, LabTest, etc.)
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// ID entità modificata
        /// </summary>
        public int? EntityId { get; set; }

        /// <summary>
        /// Valori vecchi (JSON)
        /// </summary>
        public string OldValues { get; set; }

        /// <summary>
        /// Valori nuovi (JSON)
        /// </summary>
        public string NewValues { get; set; }

        /// <summary>
        /// Indirizzo IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// User Agent
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Data e ora azione
        /// </summary>
        public DateTime? ActionDate { get; set; }
    }
}
