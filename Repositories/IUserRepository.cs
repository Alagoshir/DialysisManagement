using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Repositories
{
    /// <summary>
    /// Repository per gestione utenti
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Recupera utente per ID
        /// </summary>
        Task<User> GetByIdAsync(int userId);

        /// <summary>
        /// Recupera utente per username
        /// </summary>
        Task<User> GetByUsernameAsync(string username);

        /// <summary>
        /// Recupera tutti gli utenti
        /// </summary>
        Task<IEnumerable<User>> GetAllAsync();

        /// <summary>
        /// Recupera utenti attivi
        /// </summary>
        Task<IEnumerable<User>> GetActiveUsersAsync();

        /// <summary>
        /// Recupera utenti per ruolo
        /// </summary>
        Task<IEnumerable<User>> GetByRoleAsync(string role);

        /// <summary>
        /// Inserisce nuovo utente
        /// </summary>
        Task<int> InsertAsync(User user);

        /// <summary>
        /// Aggiorna utente esistente
        /// </summary>
        Task<bool> UpdateAsync(User user);

        /// <summary>
        /// Elimina utente (soft delete)
        /// </summary>
        Task<bool> DeleteAsync(int userId);

        /// <summary>
        /// Verifica se username esiste già
        /// </summary>
        Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null);

        /// <summary>
        /// Aggiorna password utente
        /// </summary>
        Task<bool> UpdatePasswordAsync(int userId, string passwordHash);

        /// <summary>
        /// Incrementa tentativi login falliti
        /// </summary>
        Task IncrementFailedLoginAttemptsAsync(int userId);

        /// <summary>
        /// Reset tentativi login falliti
        /// </summary>
        Task ResetFailedLoginAttemptsAsync(int userId);

        /// <summary>
        /// Blocca account utente
        /// </summary>
        Task LockAccountAsync(int userId, int durationMinutes);

        /// <summary>
        /// Sblocca account utente
        /// </summary>
        Task UnlockAccountAsync(int userId);

        /// <summary>
        /// Aggiorna ultimo login
        /// </summary>
        Task UpdateLastLoginAsync(int userId);
    }
}
