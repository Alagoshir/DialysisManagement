using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using NLog;

namespace DialysisManagement.Services
{
    public class VascularAccessService : IVascularAccessService
    {
        private readonly IVascularAccessRepository _repository;
        private readonly IAuditService _auditService;
        private readonly Logger _logger;

        public VascularAccessService(
            IVascularAccessRepository repository,
            IAuditService auditService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<IEnumerable<VascularAccess>> GetAccessesByPatientAsync(int patientId)
        {
            try
            {
                return await _repository.GetByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero accessi vascolari paziente {patientId}");
                throw;
            }
        }

        public async Task<VascularAccess> GetAccessByIdAsync(int accessId)
        {
            try
            {
                return await _repository.GetByIdAsync(accessId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero accesso vascolare {accessId}");
                throw;
            }
        }

        public async Task<VascularAccess> GetActiveAccessByPatientAsync(int patientId)
        {
            try
            {
                var accesses = await _repository.GetByPatientIdAsync(patientId);
                return accesses.FirstOrDefault(a => a.Stato == "attivo" && !a.DataRimozione.HasValue);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero accesso attivo paziente {patientId}");
                throw;
            }
        }

        public async Task<(bool success, int accessId, string errorMessage)> CreateAccessAsync(VascularAccess access)
        {
            try
            {
                // Validazione
                if (access.PatientId <= 0)
                    return (false, 0, "ID paziente non valido");

                if (string.IsNullOrWhiteSpace(access.TipoAccesso))
                    return (false, 0, "Tipo accesso obbligatorio");

                // Se nuovo accesso attivo, disattiva precedente
                if (access.Stato == "attivo")
                {
                    var activeAccess = await GetActiveAccessByPatientAsync(access.PatientId);
                    if (activeAccess != null)
                    {
                        await DeactivateAccessAsync(activeAccess.AccessId, "Nuovo accesso creato");
                    }
                }

                access.CreatedDate = DateTime.Now;
                var accessId = await _repository.AddAsync(access);

                await _auditService.LogAsync("Create", "VascularAccess", accessId,
                    $"Creato accesso vascolare {access.TipoAccesso} per paziente {access.PatientId}");

                _logger.Info($"Creato accesso vascolare ID {accessId}");
                return (true, accessId, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore creazione accesso vascolare");
                return (false, 0, ex.Message);
            }
        }

        public async Task<(bool success, string errorMessage)> UpdateAccessAsync(VascularAccess access)
        {
            try
            {
                access.ModifiedDate = DateTime.Now;
                var success = await _repository.UpdateAsync(access);

                if (success)
                {
                    await _auditService.LogAsync("Update", "VascularAccess", access.AccessId,
                        $"Aggiornato accesso vascolare {access.TipoAccesso}");
                }

                return (success, success ? null : "Errore aggiornamento database");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore aggiornamento accesso {access.AccessId}");
                return (false, ex.Message);
            }
        }

        public async Task<bool> DeleteAccessAsync(int accessId)
        {
            try
            {
                var success = await _repository.DeleteAsync(accessId);

                if (success)
                {
                    await _auditService.LogAsync("Delete", "VascularAccess", accessId,
                        "Eliminato accesso vascolare");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore eliminazione accesso {accessId}");
                return false;
            }
        }

        public async Task<bool> DeactivateAccessAsync(int accessId, string reason)
        {
            try
            {
                var access = await _repository.GetByIdAsync(accessId);
                if (access == null)
                    return false;

                access.Stato = "rimosso";
                access.DataRimozione = DateTime.Now;
                access.Note = $"{access.Note}\n[{DateTime.Now:dd/MM/yyyy}] Disattivato: {reason}".Trim();

                return await _repository.UpdateAsync(access);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore disattivazione accesso {accessId}");
                return false;
            }
        }

        public async Task<IEnumerable<VascularAccess>> GetAccessesByTypeAsync(string type)
        {
            try
            {
                var allAccesses = await _repository.GetAllAsync();
                return allAccesses.Where(a => a.TipoAccesso.Equals(type, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore recupero accessi tipo {type}");
                throw;
            }
        }

        public async Task<IEnumerable<VascularAccess>> GetMalfunctioningAccessesAsync()
        {
            try
            {
                var allAccesses = await _repository.GetAllAsync();
                return allAccesses.Where(a =>
                    a.Stato == "attivo" &&
                    (a.Infezione || a.Stenosi || a.Trombosi || a.Stato == "malfunzionante"));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore recupero accessi malfunzionanti");
                throw;
            }
        }
    }
}
