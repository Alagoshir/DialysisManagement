using System;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Gestisce la sessione utente corrente
    /// </summary>
    public static class SessionManager
    {
        private static User _currentUser;
        private static DateTime _loginTime;
        private static DateTime _lastActivityTime;

        /// <summary>
        /// Utente correntemente loggato
        /// </summary>
        public static User CurrentUser
        {
            get => _currentUser;
            private set => _currentUser = value;
        }

        /// <summary>
        /// Verifica se un utente è loggato
        /// </summary>
        public static bool IsLoggedIn => _currentUser != null;

        /// <summary>
        /// ID utente corrente
        /// </summary>
        public static int? CurrentUserId => _currentUser?.UserId;

        /// <summary>
        /// Username corrente
        /// </summary>
        public static string CurrentUsername => _currentUser?.Username;

        /// <summary>
        /// Ruolo utente corrente
        /// </summary>
        public static string CurrentUserRole => _currentUser?.Role;

        /// <summary>
        /// Nome completo utente corrente
        /// </summary>
        public static string CurrentUserFullName => _currentUser?.FullName;

        /// <summary>
        /// Ora login
        /// </summary>
        public static DateTime LoginTime => _loginTime;

        /// <summary>
        /// Ultima attività
        /// </summary>
        public static DateTime LastActivityTime => _lastActivityTime;

        /// <summary>
        /// Durata sessione
        /// </summary>
        public static TimeSpan SessionDuration => DateTime.Now - _loginTime;

        /// <summary>
        /// Imposta utente loggato
        /// </summary>
        public static void Login(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _currentUser = user;
            _loginTime = DateTime.Now;
            _lastActivityTime = DateTime.Now;
        }

        /// <summary>
        /// Effettua logout
        /// </summary>
        public static void Logout()
        {
            _currentUser = null;
            _loginTime = DateTime.MinValue;
            _lastActivityTime = DateTime.MinValue;
        }

        /// <summary>
        /// Aggiorna timestamp ultima attività
        /// </summary>
        public static void UpdateActivity()
        {
            _lastActivityTime = DateTime.Now;
        }

        /// <summary>
        /// Verifica se sessione è scaduta
        /// </summary>
        public static bool IsSessionExpired(int timeoutMinutes = 30)
        {
            if (!IsLoggedIn)
                return true;

            return (DateTime.Now - _lastActivityTime).TotalMinutes > timeoutMinutes;
        }

        /// <summary>
        /// Verifica se utente ha ruolo specifico
        /// </summary>
        public static bool HasRole(string role)
        {
            if (!IsLoggedIn || string.IsNullOrEmpty(role))
                return false;

            return _currentUser.Role.Equals(role, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Verifica se utente è amministratore
        /// </summary>
        public static bool IsAdmin()
        {
            return HasRole("admin");
        }

        /// <summary>
        /// Verifica se utente è medico
        /// </summary>
        public static bool IsMedico()
        {
            return HasRole("medico") || IsAdmin();
        }

        /// <summary>
        /// Verifica se utente è infermiere
        /// </summary>
        public static bool IsInfermiere()
        {
            return HasRole("infermiere") || IsMedico();
        }

        /// <summary>
        /// Verifica permesso modifica dati
        /// </summary>
        public static bool CanEdit()
        {
            return IsLoggedIn && !HasRole("readonly");
        }

        /// <summary>
        /// Verifica permesso eliminazione dati
        /// </summary>
        public static bool CanDelete()
        {
            return IsAdmin() || IsMedico();
        }

        /// <summary>
        /// Verifica permesso gestione utenti
        /// </summary>
        public static bool CanManageUsers()
        {
            return IsAdmin();
        }

        /// <summary>
        /// Verifica permesso backup/restore
        /// </summary>
        public static bool CanManageBackups()
        {
            return IsAdmin();
        }
    }
}
