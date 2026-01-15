using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using NLog;

namespace DialysisManagement.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditLogRepository _auditRepository;
        private readonly IUserRepository _userRepository;
        private readonly Logger _logger;

        public AuditService(
            IAuditLogRepository auditRepository,
            IUserRepository userRepository)
        {
            _auditRepository = auditRepository ?? throw new ArgumentNullException(nameof(auditRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task LogAsync(int? userId, string azione, string tabella, int? recordId, string dettagli)
        {
            try
            {
                var username = "Sistema";
                if (userId.HasValue)
                {
                    var user = await _userRepository.GetByIdAsync(userId.Value);
                    username = user?.Username ?? "Sconosciuto";
                }

                var auditLog = new AuditLog
                {
                    UserId = userId,
                    Username = username,
                    Azione = azione,
                    Tabella = tabella,
                    RecordId = recordId,
                    Dettagli = dettagli,
                    IpAddress = GetLocalIPAddress(),
                    Timestamp = DateTime.Now
                };

                await _auditRepository.InsertAsync(auditLog);
                _logger.Debug($"Audit log creato: {azione} - {tabella} - {dettagli}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la creazione del log audit");
                // Non propago l'eccezione per non bloccare l'operazione principale
            }
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByUserAsync(int userId, int limit = 100)
        {
            try
            {
                return await _auditRepository.GetByUserIdAsync(userId, limit);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero dei log per utente ID: {userId}");
                throw;
            }
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _auditRepository.GetByDateRangeAsync(startDate, endDate);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il recupero dei log per intervallo date");
                throw;
            }
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByActionAsync(string azione)
        {
            try
            {
                return await _auditRepository.GetByAzioneAsync(azione);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero dei log per azione: {azione}");
                throw;
            }
        }

        public async Task<IEnumerable<AuditLog>> GetLogsByTableAsync(string tabella, int recordId)
        {
            try
            {
                return await _auditRepository.GetByTabellaAsync(tabella, recordId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero dei log per tabella: {tabella}");
                throw;
            }
        }

        private string GetLocalIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
