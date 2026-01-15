using System;
using System.ComponentModel.DataAnnotations;

namespace DialysisManagement.Models
{
    /// <summary>
    /// Rappresenta un utente dell'applicazione
    /// </summary>
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        /// <summary>
        /// Password hash (BCrypt)
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Ruolo: admin, medico, infermiere, readonly
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Tentativi di login falliti consecutivi
        /// </summary>
        public int FailedLoginAttempts { get; set; } = 0;

        /// <summary>
        /// Account bloccato per troppi tentativi
        /// </summary>
        public bool IsLocked { get; set; } = false;

        public DateTime? LockoutEndDate { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime? LastPasswordChangeDate { get; set; }

        /// <summary>
        /// Forza cambio password al prossimo login
        /// </summary>
        public bool MustChangePassword { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }
    }
}
