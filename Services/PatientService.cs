using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using DialysisManagement.Utilities;
using NLog;

namespace DialysisManagement.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAuditService _auditService;
        private readonly IValidationHelper _validationHelper;
        private readonly IQRCodeGenerator _qrCodeGenerator;
        private readonly IAuthenticationService _authService;
        private readonly Logger _logger;

        public PatientService(
            IPatientRepository patientRepository,
            IAuditService auditService,
            IValidationHelper validationHelper,
            IQRCodeGenerator qrCodeGenerator,
            IAuthenticationService authService)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
            _qrCodeGenerator = qrCodeGenerator ?? throw new ArgumentNullException(nameof(qrCodeGenerator));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            try
            {
                return await _patientRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il recupero di tutti i pazienti");
                throw;
            }
        }

        public async Task<Patient> GetPatientByIdAsync(int patientId)
        {
            try
            {
                return await _patientRepository.GetByIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero del paziente ID: {patientId}");
                throw;
            }
        }

        public async Task<Patient> GetPatientByCodiceFiscaleAsync(string codiceFiscale)
        {
            try
            {
                return await _patientRepository.GetByCodiceFiscaleAsync(codiceFiscale);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero del paziente CF: {codiceFiscale}");
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetAllPatientsAsync();
                }

                return await _patientRepository.SearchPatientsAsync(searchTerm);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante la ricerca pazienti: {searchTerm}");
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetActivePatientsAsync()
        {
            try
            {
                return await _patientRepository.GetActivePatientsAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il recupero dei pazienti attivi");
                throw;
            }
        }

        public async Task<IEnumerable<Patient>> GetContumacialPatientsAsync()
        {
            try
            {
                return await _patientRepository.GetContumacialPatientsAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il recupero dei pazienti contumaciali");
                throw;
            }
        }

        public async Task<(bool Success, int PatientId, string ErrorMessage)> CreatePatientAsync(Patient patient)
        {
            try
            {
                _logger.Info($"Creazione nuovo paziente: {patient.Nome} {patient.Cognome}");

                // Validazione
                var validationResult = await ValidatePatientDataAsync(patient);
                if (!validationResult)
                {
                    return (false, 0, "Dati paziente non validi");
                }

                // Verifica codice fiscale univoco
                var existing = await _patientRepository.GetByCodiceFiscaleAsync(patient.CodiceFiscale);
                if (existing != null)
                {
                    _logger.Warn($"Codice fiscale già esistente: {patient.CodiceFiscale}");
                    return (false, 0, "Codice fiscale già presente nel sistema");
                }

                // Calcola BMI se possibile
                if (patient.PesoSecco.HasValue && patient.Altezza.HasValue && patient.Altezza.Value > 0)
                {
                    var altezzaMetri = patient.Altezza.Value / 100.0m;
                    patient.Bmi = patient.PesoSecco.Value / (altezzaMetri * altezzaMetri);
                }

                // Imposta valori di default
                patient.Stato = "attivo";
                patient.DataIngresso = DateTime.Now;
                patient.CreatedBy = _authService.GetCurrentUser()?.UserId;
                patient.UpdatedBy = _authService.GetCurrentUser()?.UserId;

                // Inserisci paziente
                var patientId = await _patientRepository.InsertAsync(patient);

                if (patientId > 0)
                {
                    _logger.Info($"Paziente creato con successo. ID: {patientId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "PATIENT_CREATED",
                        "patients",
                        patientId,
                        $"Creato paziente: {patient.Nome} {patient.Cognome}");

                    // Genera QR Code
                    await GenerateQRCodeAsync(patientId);

                    return (true, patientId, null);
                }

                return (false, 0, "Errore durante la creazione del paziente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la creazione del paziente");
                return (false, 0, $"Errore: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> UpdatePatientAsync(Patient patient)
        {
            try
            {
                _logger.Info($"Aggiornamento paziente ID: {patient.PatientId}");

                // Validazione
                var validationResult = await ValidatePatientDataAsync(patient);
                if (!validationResult)
                {
                    return (false, "Dati paziente non validi");
                }

                // Verifica esistenza
                var existing = await _patientRepository.GetByIdAsync(patient.PatientId);
                if (existing == null)
                {
                    return (false, "Paziente non trovato");
                }

                // Calcola BMI
                if (patient.PesoSecco.HasValue && patient.Altezza.HasValue && patient.Altezza.Value > 0)
                {
                    var altezzaMetri = patient.Altezza.Value / 100.0m;
                    patient.Bmi = patient.PesoSecco.Value / (altezzaMetri * altezzaMetri);
                }

                patient.UpdatedBy = _authService.GetCurrentUser()?.UserId;

                // Aggiorna
                var success = await _patientRepository.UpdateAsync(patient);

                if (success)
                {
                    _logger.Info($"Paziente aggiornato con successo. ID: {patient.PatientId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "PATIENT_UPDATED",
                        "patients",
                        patient.PatientId,
                        $"Aggiornato paziente: {patient.Nome} {patient.Cognome}");

                    return (true, null);
                }

                return (false, "Errore durante l'aggiornamento del paziente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante l'aggiornamento del paziente ID: {patient.PatientId}");
                return (false, $"Errore: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeletePatientAsync(int patientId)
        {
            try
            {
                _logger.Info($"Eliminazione paziente ID: {patientId}");

                // Verifica permessi
                if (!_authService.HasPermission("DELETE_PATIENTS"))
                {
                    return (false, "Permessi insufficienti");
                }

                var patient = await _patientRepository.GetByIdAsync(patientId);
                if (patient == null)
                {
                    return (false, "Paziente non trovato");
                }

                var success = await _patientRepository.DeleteAsync(patientId);

                if (success)
                {
                    _logger.Info($"Paziente eliminato con successo. ID: {patientId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "PATIENT_DELETED",
                        "patients",
                        patientId,
                        $"Eliminato paziente: {patient.Nome} {patient.Cognome}");

                    return (true, null);
                }

                return (false, "Errore durante l'eliminazione del paziente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante l'eliminazione del paziente ID: {patientId}");
                return (false, $"Errore: {ex.Message}");
            }
        }

        public async Task<bool> UpdatePesoSeccoAsync(int patientId, decimal pesoSecco)
        {
            try
            {
                _logger.Info($"Aggiornamento peso secco per paziente ID: {patientId}");

                if (pesoSecco <= 0 || pesoSecco > 300)
                {
                    _logger.Warn($"Peso secco non valido: {pesoSecco}");
                    return false;
                }

                var success = await _patientRepository.UpdatePesoSeccoAsync(patientId, pesoSecco);

                if (success)
                {
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "PESO_SECCO_UPDATED",
                        "patients",
                        patientId,
                        $"Peso secco aggiornato: {pesoSecco} kg");
                }

                return success;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante l'aggiornamento peso secco per paziente ID: {patientId}");
                return false;
            }
        }

        public async Task<string> GenerateQRCodeAsync(int patientId)
        {
            try
            {
                var patient = await _patientRepository.GetByIdAsync(patientId);
                if (patient == null)
                {
                    return null;
                }

                var qrData = $"PATIENT:{patientId}|CF:{patient.CodiceFiscale}|NAME:{patient.Nome} {patient.Cognome}";
                var qrPath = await _qrCodeGenerator.GenerateQRCodeAsync(qrData, patientId.ToString());

                return qrPath;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante la generazione QR code per paziente ID: {patientId}");
                return null;
            }
        }

        public async Task<bool> ValidatePatientDataAsync(Patient patient)
        {
            try
            {
                // Validazione campi obbligatori
                if (string.IsNullOrWhiteSpace(patient.CodiceFiscale) ||
                    string.IsNullOrWhiteSpace(patient.Nome) ||
                    string.IsNullOrWhiteSpace(patient.Cognome) ||
                    string.IsNullOrWhiteSpace(patient.Sesso))
                {
                    _logger.Warn("Campi obbligatori mancanti");
                    return false;
                }

                // Validazione codice fiscale
                if (!_validationHelper.ValidateCodiceFiscale(patient.CodiceFiscale))
                {
                    _logger.Warn($"Codice fiscale non valido: {patient.CodiceFiscale}");
                    return false;
                }

                // Validazione email
                if (!string.IsNullOrWhiteSpace(patient.Email) && !_validationHelper.ValidateEmail(patient.Email))
                {
                    _logger.Warn($"Email non valida: {patient.Email}");
                    return false;
                }

                // Validazione data nascita
                if (patient.DataNascita > DateTime.Now.AddYears(-18))
                {
                    _logger.Warn("Paziente minorenne");
                    return false;
                }

                if (patient.DataNascita < DateTime.Now.AddYears(-120))
                {
                    _logger.Warn("Data di nascita non valida");
                    return false;
                }

                // Validazione peso secco
                if (patient.PesoSecco.HasValue && (patient.PesoSecco.Value <= 0 || patient.PesoSecco.Value > 300))
                {
                    _logger.Warn($"Peso secco non valido: {patient.PesoSecco}");
                    return false;
                }

                // Validazione altezza
                if (patient.Altezza.HasValue && (patient.Altezza.Value < 50 || patient.Altezza.Value > 250))
                {
                    _logger.Warn($"Altezza non valida: {patient.Altezza}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la validazione dei dati paziente");
                return false;
            }
        }
    }
}
