using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Servizio per autenticazione e gestione sessioni
    /// </summary>
    public interface IAuthenticationService
    {
        Task<(bool Success, User User, string ErrorMessage)> LoginAsync(string username, string password);
        Task LogoutAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<bool> ResetPasswordAsync(int userId, string newPassword);
        User GetCurrentUser();
        bool IsUserInRole(string role);
        bool HasPermission(string permission);
    }
}
