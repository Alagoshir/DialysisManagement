using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using NLog;

namespace DialysisManagement.Services
{
    public class DialysisSessionService : IDialysisSessionService
    {
        private readonly IDialysisSessionRepository _sessionRepository;
        private readonly IVitalSignRepository _vitalSignRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAuditService _auditService;
        private readonly IAuthenticationService _authService;
        private readonly Logger _logger;

        public DialysisSessionService(
            IDialysisSessionRepository sessionRepository,
            IVitalSignRepository vitalSignRepository,
            IPatientRepository patientRepository,
            IAuditService auditService,
            IAuthenticationService authService)
        {
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _vitalSignRepository = vitalSignRepository ?? throw new ArgumentNullException(nameof(vitalSignRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<IEnumerable<DialysisSession>> GetSessionsByPatientAsync(int patientId)
        {
            try
            {
                return await _sessionRepository.GetByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero delle sedute per paziente ID: {patientId}");
                throw;
            }
        }

        public async Task<IEnumerable<DialysisSession>> GetSessionsByDateAsync(DateTime date)
        {
            try
            {
                return await _sessionRepository.GetByDateAsync(date);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero delle sedute per data: {date:yyyy-MM-dd}");
                throw;
            }
        }

        public async Task<DialysisSession> GetSessionByIdAsync(int sessionId)
        {
            try
            {
                return await _sessionRepository.GetByIdAsync(sessionId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero della seduta ID: {sessionId}");
                throw;
            }
        }

        public async Task<(bool Success, int SessionId, string ErrorMessage)> CreateSessionAsync(DialysisSession session)
        {
            try
            {
                _logger.Info($"Creazione nuova seduta per paziente ID: {session.PatientId}");

                // Validazione
                if (session.PatientId <= 0)
                {
                    return (false, 0, "Paziente non valido");
                }

                // Verifica paziente esiste
                var patient = await _patientRepository.GetByIdAsync(session.PatientId);
                if (patient == null)
                {
                    return (false, 0, "Paziente non trovato");
                }

                // Imposta valori di default
                session.Stato = "programmata";
                session.CreatedBy = _authService.GetCurrentUser()?.UserId;
                session.UpdatedBy = _authService.GetCurrentUser()?.UserId;

                // Inserisci seduta
                var sessionId = await _sessionRepository.InsertAsync(session);

                if (sessionId > 0)
                {
                    _logger.Info($"Seduta creata con successo. ID: {sessionId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "SESSION_CREATED",
                        "dialysis_sessions",
                        sessionId,
                        $"Creata seduta per paziente ID: {session.PatientId}");

                    return (true, sessionId, null);
                }

                return (false, 0, "Errore durante la creazione della seduta");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la creazione della seduta");
                return (false, 0, $"Errore: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateSessionAsync(DialysisSession session)
        {
            try
            {
                _logger.Info($"Aggiornamento seduta ID: {session.SessionId}");

                // Verifica esistenza
                var existing = await _sessionRepository.GetByIdAsync(session.SessionId);
                if (existing == null)
                {
                    return (false, "Seduta non trovata");
                }

                // Calcola durata effettiva
                if (session.OraInizio.HasValue && session.OraFine.HasValue)
                {
                    var durata = session.OraFine.Value - session.OraInizio.Value;
                    session.DurataEffettiva = (int)durata.TotalMinutes;
                }

                session.UpdatedBy = _authService.GetCurrentUser()?.UserId;

                // Aggiorna
                var success = await _sessionRepository.UpdateAsync(session);

                if (success)
                {
                    _logger.Info($"Seduta aggiornata con successo. ID: {session.SessionId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "SESSION_UPDATED",
                        "dialysis_sessions",
                        session.SessionId,
                        $"Aggiornata seduta per paziente ID: {session.PatientId}");

                    return (true, null);
                }

                return (false, "Errore durante l'aggiornamento della seduta");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante l'aggiornamento della seduta ID: {session.SessionId}");
                return (false, $"Errore: {ex.Message}");
            }
        }

        public async Task<bool> StartSessionAsync(int sessionId)
        {
            try
            {
                _logger.Info($"Avvio seduta ID: {sessionId}");

                var session = await _sessionRepository.GetByIdAsync(sessionId);
                if (session == null)
                {
                    return false;
                }

                session.OraInizio = DateTime.Now.TimeOfDay;
                session.Stato = "in_corso";
                session.UpdatedBy = _authService.GetCurrentUser()?.UserId;

                var success = await _sessionRepository.UpdateAsync(session);

                if (success)
                {
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "SESSION_STARTED",
                        "dialysis_sessions",
                        sessionId,
                        "Seduta avviata");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante l'avvio della seduta ID: {sessionId}");
                return false;
            }
        }

        public async Task<bool> CompleteSessionAsync(int sessionId)
        {
            try
            {
                _logger.Info($"Completamento seduta ID: {sessionId}");

                var session = await _sessionRepository.GetByIdAsync(sessionId);
                if (session == null)
                {
                    return false;
                }

                session.OraFine = DateTime.Now.TimeOfDay;
                session.Stato = "completata";

                // Calcola durata effettiva
                if (session.OraInizio.HasValue)
                {
                    var durata = session.OraFine.Value - session.OraInizio.Value;
                    session.DurataEffettiva = (int)durata.TotalMinutes;
                }

                session.UpdatedBy = _authService.GetCurrentUser()?.UserId;

                var success = await _sessionRepository.UpdateAsync(session);

                if (success)
                {
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "SESSION_COMPLETED",
                        "dialysis_sessions",
                        sessionId,
                        "Seduta completata");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il completamento della seduta ID: {sessionId}");
                return false;
            }
        }

        public async Task<decimal?> CalculateKtvAsync(DialysisSession session, decimal? ureaPre, decimal? ureaPost)
        {
            try
            {
                if (!ureaPre.HasValue || !ureaPost.HasValue || ureaPre.Value == 0)
                {
                    return null;
                }

                // Formula semplificata Kt/V (Daugirdas)
                var urr = ((ureaPre.Value - ureaPost.Value) / ureaPre.Value) * 100;

                // Ottieni peso paziente
                var patient = await _patientRepository.GetByIdAsync(session.PatientId);
                if (patient?.PesoSecco == null)
                {
                    return null;
                }

                var pesoPost = session.PesoPost ?? patient.PesoSecco.Value;
                var ufLitri = (session.UfEffettuata ?? 0) / 1000.0m;

                // Formula Daugirdas seconda generazione
                var ktv = -Math.Log((decimal)(ureaPost.Value / ureaPre.Value - 0.008m * (session.DurataEffettiva ?? 240) / 60.0m))
                          + (4m - 3.5m * (ureaPost.Value / ureaPre.Value)) * (ufLitri / pesoPost);

                return Math.Round(ktv, 2);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il calcolo Kt/V");
                return null;
            }
        }

        public async Task<IEnumerable<VitalSign>> GetVitalSignsBySessionAsync(int sessionId)
        {
            try
            {
                return await _vitalSignRepository.GetBySessionIdAsync(sessionId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero dei parametri vitali per seduta ID: {sessionId}");
                throw;
            }
        }

        public async Task<bool> AddVitalSignAsync(VitalSign vitalSign)
        {
            try
            {
                _logger.Info($"Aggiunta parametri vitali per seduta ID: {vitalSign.SessionId}");

                vitalSign.CreatedBy = _authService.GetCurrentUser()?.UserId;

                var vitalId = await _vitalSignRepository.InsertAsync(vitalSign);

                if (vitalId > 0)
                {
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "VITAL_SIGN_ADDED",
                        "vital_signs",
                        vitalId,
                        $"Parametri vitali aggiunti per seduta ID: {vitalSign.SessionId}");

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante l'aggiunta dei parametri vitali");
                return false;
            }
        }
    }
}
