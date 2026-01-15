using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DialysisManagement.Data;
using DialysisManagement.Models;
using MySql.Data.MySqlClient;

namespace DialysisManagement.Repositories
{
    /// <summary>
    /// Implementazione repository utenti
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseHelper _dbHelper;

        public UserRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        /// <summary>
        /// Recupera utente per ID
        /// </summary>
        public async Task<User> GetByIdAsync(int userId)
        {
            const string sql = @"
                SELECT 
                    UserId, Username, PasswordHash, FirstName, LastName, Email, Role,
                    IsActive, FailedLoginAttempts, IsLocked, LockoutEndDate,
                    LastLoginDate, LastPasswordChangeDate, MustChangePassword,
                    CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                FROM users
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { UserId = userId });
                    return user;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dell'utente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera utente per username
        /// </summary>
        public async Task<User> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username non può essere vuoto", nameof(username));

            const string sql = @"
                SELECT 
                    UserId, Username, PasswordHash, FirstName, LastName, Email, Role,
                    IsActive, FailedLoginAttempts, IsLocked, LockoutEndDate,
                    LastLoginDate, LastPasswordChangeDate, MustChangePassword,
                    CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                FROM users
                WHERE Username = @Username";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
                    return user;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero dell'utente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera tutti gli utenti
        /// </summary>
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            const string sql = @"
                SELECT 
                    UserId, Username, PasswordHash, FirstName, LastName, Email, Role,
                    IsActive, FailedLoginAttempts, IsLocked, LockoutEndDate,
                    LastLoginDate, LastPasswordChangeDate, MustChangePassword,
                    CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                FROM users
                ORDER BY Username";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var users = await connection.QueryAsync<User>(sql);
                    return users;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero degli utenti: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera utenti attivi
        /// </summary>
        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            const string sql = @"
                SELECT 
                    UserId, Username, PasswordHash, FirstName, LastName, Email, Role,
                    IsActive, FailedLoginAttempts, IsLocked, LockoutEndDate,
                    LastLoginDate, LastPasswordChangeDate, MustChangePassword,
                    CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                FROM users
                WHERE IsActive = TRUE AND IsLocked = FALSE
                ORDER BY Username";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var users = await connection.QueryAsync<User>(sql);
                    return users;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero degli utenti attivi: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recupera utenti per ruolo
        /// </summary>
        public async Task<IEnumerable<User>> GetByRoleAsync(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Ruolo non può essere vuoto", nameof(role));

            const string sql = @"
                SELECT 
                    UserId, Username, PasswordHash, FirstName, LastName, Email, Role,
                    IsActive, FailedLoginAttempts, IsLocked, LockoutEndDate,
                    LastLoginDate, LastPasswordChangeDate, MustChangePassword,
                    CreatedDate, CreatedBy, ModifiedDate, ModifiedBy
                FROM users
                WHERE Role = @Role AND IsActive = TRUE
                ORDER BY Username";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var users = await connection.QueryAsync<User>(sql, new { Role = role });
                    return users;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il recupero degli utenti per ruolo: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Inserisce nuovo utente
        /// </summary>
        public async Task<int> InsertAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            const string sql = @"
                INSERT INTO users (
                    Username, PasswordHash, FirstName, LastName, Email, Role,
                    IsActive, MustChangePassword, CreatedDate, CreatedBy
                ) VALUES (
                    @Username, @PasswordHash, @FirstName, @LastName, @Email, @Role,
                    @IsActive, @MustChangePassword, @CreatedDate, @CreatedBy
                );
                SELECT LAST_INSERT_ID();";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var userId = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        user.Username,
                        user.PasswordHash,
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.Role,
                        user.IsActive,
                        user.MustChangePassword,
                        CreatedDate = DateTime.Now,
                        user.CreatedBy
                    });

                    return userId;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'inserimento dell'utente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Aggiorna utente esistente
        /// </summary>
        public async Task<bool> UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            const string sql = @"
                UPDATE users SET
                    Username = @Username,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    Role = @Role,
                    IsActive = @IsActive,
                    ModifiedDate = @ModifiedDate,
                    ModifiedBy = @ModifiedBy
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var rowsAffected = await connection.ExecuteAsync(sql, new
                    {
                        user.UserId,
                        user.Username,
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.Role,
                        user.IsActive,
                        ModifiedDate = DateTime.Now,
                        user.ModifiedBy
                    });

                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'aggiornamento dell'utente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Elimina utente (soft delete)
        /// </summary>
        public async Task<bool> DeleteAsync(int userId)
        {
            const string sql = @"
                UPDATE users 
                SET IsActive = FALSE, ModifiedDate = @ModifiedDate
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var rowsAffected = await connection.ExecuteAsync(sql, new
                    {
                        UserId = userId,
                        ModifiedDate = DateTime.Now
                    });

                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'eliminazione dell'utente: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Verifica se username esiste già
        /// </summary>
        public async Task<bool> UsernameExistsAsync(string username, int? excludeUserId = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            string sql = "SELECT COUNT(*) FROM users WHERE Username = @Username";

            if (excludeUserId.HasValue)
                sql += " AND UserId != @ExcludeUserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var count = await connection.ExecuteScalarAsync<int>(sql, new
                    {
                        Username = username,
                        ExcludeUserId = excludeUserId
                    });

                    return count > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante la verifica dell'username: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Aggiorna password utente
        /// </summary>
        public async Task<bool> UpdatePasswordAsync(int userId, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password hash non può essere vuoto", nameof(passwordHash));

            const string sql = @"
                UPDATE users 
                SET PasswordHash = @PasswordHash,
                    LastPasswordChangeDate = @LastPasswordChangeDate,
                    MustChangePassword = FALSE,
                    ModifiedDate = @ModifiedDate
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    var rowsAffected = await connection.ExecuteAsync(sql, new
                    {
                        UserId = userId,
                        PasswordHash = passwordHash,
                        LastPasswordChangeDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });

                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'aggiornamento della password: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Incrementa tentativi login falliti
        /// </summary>
        public async Task IncrementFailedLoginAttemptsAsync(int userId)
        {
            const string sql = @"
                UPDATE users 
                SET FailedLoginAttempts = FailedLoginAttempts + 1
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await connection.ExecuteAsync(sql, new { UserId = userId });
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'incremento dei tentativi falliti: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Reset tentativi login falliti
        /// </summary>
        public async Task ResetFailedLoginAttemptsAsync(int userId)
        {
            const string sql = @"
                UPDATE users 
                SET FailedLoginAttempts = 0
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await connection.ExecuteAsync(sql, new { UserId = userId });
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il reset dei tentativi falliti: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Blocca account utente
        /// </summary>
        public async Task LockAccountAsync(int userId, int durationMinutes)
        {
            const string sql = @"
                UPDATE users 
                SET IsLocked = TRUE,
                    LockoutEndDate = DATE_ADD(NOW(), INTERVAL @DurationMinutes MINUTE)
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        UserId = userId,
                        DurationMinutes = durationMinutes
                    });
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante il blocco dell'account: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Sblocca account utente
        /// </summary>
        public async Task UnlockAccountAsync(int userId)
        {
            const string sql = @"
                UPDATE users 
                SET IsLocked = FALSE,
                    LockoutEndDate = NULL,
                    FailedLoginAttempts = 0
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await connection.ExecuteAsync(sql, new { UserId = userId });
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante lo sblocco dell'account: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Aggiorna ultimo login
        /// </summary>
        public async Task UpdateLastLoginAsync(int userId)
        {
            const string sql = @"
                UPDATE users 
                SET LastLoginDate = @LastLoginDate
                WHERE UserId = @UserId";

            try
            {
                using (var connection = _dbHelper.GetConnection())
                {
                    await connection.ExecuteAsync(sql, new
                    {
                        UserId = userId,
                        LastLoginDate = DateTime.Now
                    });
                }
            }
            catch (MySqlException ex)
            {
                throw new DataException($"Errore durante l'aggiornamento dell'ultimo login: {ex.Message}", ex);
            }
        }
    }
}
