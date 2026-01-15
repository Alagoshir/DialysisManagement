using System;
using System.Threading.Tasks;
using BCrypt.Net;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using DialysisManagement.Utilities;
using NLog;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Implementazione servizio autenticazione
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditService _auditService;
        private readonly Logger _logger;
        private User _currentUser;

        public AuthenticationService(
            IUserRepository userRepository,
            IAuditService auditService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<(bool Success, User User, string ErrorMessage)> LoginAsync(string username, string password)
        {
            try
            {
                _logger.Info($"Tentativo login per utente: {username}");

                // Validazione input
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    return (false, null, "Username e password obbligatori");
                }

                // Recupera utente
                var user = await _userRepository.GetByUsernameAsync(username);
                if (user == null)
                {
                    _logger.Warn($"Tentativo login fallito: utente {username} non trovato");
                    await _auditService.LogAsync(null, "LOGIN_FAILED", "users", null,
                        $"Username non trovato: {username}");
                    return (false, null, "Username o password non corretti");
                }

                // Verifica account attivo
                if (!user.IsActive)
                {
                    _logger.Warn($"Tentativo login fallito: utente {username} disabilitato");
                    await _auditService.LogAsync(user.UserId, "LOGIN_FAILED", "users", user.UserId,
                        "Account disabilitato");
                    return (false, null, "Account disabilitato. Contattare l'amministratore");
                }

                // Verifica account bloccato
                if (user.IsLocked)
                {
                    var minutiRimanenti = (user.AccountLockedUntil.Value - DateTime.Now).TotalMinutes;
                    _logger.Warn($"Tentativo login fallito: utente {username} bloccato");
                    await _auditService.LogAsync(user.UserId, "LOGIN_FAILED", "users", user.UserId,
                        "Account bloccato");
                    return (false, null, $"Account bloccato per {Math.Ceiling(minutiRimanenti)} minuti");
                }

                // Verifica password
                bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                if (!passwordValid)
                {
                    _logger.Warn($"Tentativo login fallito: password errata per {username}");

                    // Incrementa tentativi falliti
                    await _userRepository.IncrementFailedLoginAttemptsAsync(user.UserId);

                    // Blocca account dopo 3 tentativi
                    var maxAttempts = ConfigurationHelper.GetAppSettingInt("MaxLoginAttempts", 3);
                    if (user.FailedLoginAttempts + 1 >= maxAttempts)
                    {
                        await _userRepository.LockAccountAsync(user.UserId, 30); // Blocca per 30 minuti
                        await _auditService.LogAsync(user.UserId, "ACCOUNT_LOCKED", "users", user.UserId,
                            "Account bloccato per troppi tentativi falliti");
                        return (false, null, "Troppi tentativi falliti. Account bloccato per 30 minuti");
                    }

                    await _auditService.LogAsync(user.UserId, "LOGIN_FAILED", "users", user.UserId,
                        "Password errata");
                    return (false, null, "Username o password non corretti");
                }

                // Login riuscito
                await _userRepository.UpdateLastLoginAsync(user.UserId);
                await _userRepository.ResetFailedLoginAttemptsAsync(user.UserId);

                _currentUser = user;

                _logger.Info($"Login riuscito per utente: {username}");
                await _auditService.LogAsync(user.UserId, "LOGIN_SUCCESS", "users", user.UserId,
                    "Login effettuato con successo");

                return (true, user, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante login per utente: {username}");
                return (false, null, "Errore durante il login. Riprovare");
            }
        }

        public async Task LogoutAsync(int userId)
        {
            try
            {
                _logger.Info($"Logout per utente ID: {userId}");
                await _auditService.LogAsync(userId, "LOGOUT", "users", userId, "Logout effettuato");
                _currentUser = null;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante logout per utente ID: {userId}");
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            try
            {
                _logger.Info($"Cambio password per utente ID: {userId}");

                // Validazione
                if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
                {
                    return false;
                }

                var minLength = ConfigurationHelper.GetAppSettingInt("PasswordMinLength", 8);
                if (newPassword.Length < minLength)
                {
                    _logger.Warn($"Password troppo corta per utente ID: {userId}");
                    return false;
                }

                // Recupera utente
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    return false;
                }

                // Verifica vecchia password
                if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
                {
                    _logger.Warn($"Vecchia password errata per utente ID: {userId}");
                    await _auditService.LogAsync(userId, "PASSWORD_CHANGE_FAILED", "users", userId,
                        "Vecchia password errata");
                    return false;
                }

                // Hash nuova password
                var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                // Aggiorna password
                var success = await _userRepository.UpdatePasswordAsync(userId, newPasswordHash);

                if (success)
                {
                    _logger.Info($"Password cambiata con successo per utente ID: {userId}");
                    await _auditService.LogAsync(userId, "PASSWORD_CHANGED", "users", userId,
                        "Password modificata con successo");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante cambio password per utente ID: {userId}");
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(int userId, string newPassword)
        {
            try
            {
                _logger.Info($"Reset password per utente ID: {userId}");

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    return false;
                }

                var minLength = ConfigurationHelper.GetAppSettingInt("PasswordMinLength", 8);
                if (newPassword.Length < minLength)
                {
                    return false;
                }

                // Hash password
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);

                // Aggiorna password
                var success = await _userRepository.UpdatePasswordAsync(userId, passwordHash);

                if (success)
                {
                    _logger.Info($"Password resettata per utente ID: {userId}");
                    await _auditService.LogAsync(_currentUser?.UserId ?? 0, "PASSWORD_RESET", "users", userId,
                        "Password resettata dall'amministratore");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante reset password per utente ID: {userId}");
                return false;
            }
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public bool IsUserInRole(string role)
        {
            return _currentUser?.Role?.Equals(role, StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public bool HasPermission(string permission)
        {
            if (_currentUser == null) return false;

            // Admin ha tutti i permessi
            if (_currentUser.Role == "admin") return true;

            // Logica permessi per ruolo
            return permission switch
            {
                "VIEW_PATIENTS" => true, // Tutti possono visualizzare
                "EDIT_PATIENTS" => _currentUser.Role == "medico" || _currentUser.Role == "admin",
                "DELETE_PATIENTS" => _currentUser.Role == "admin",
                "MANAGE_SESSIONS" => _currentUser.Role == "medico" || _currentUser.Role == "infermiere",
                "MANAGE_USERS" => _currentUser.Role == "admin",
                "VIEW_REPORTS" => _currentUser.Role == "medico" || _currentUser.Role == "admin",
                "BACKUP_RESTORE" => _currentUser.Role == "admin",
                _ => false
            };
        }
    }
}
