using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using NLog;

namespace DialysisManagement.Services
{
    public class LabTestService : ILabTestService
    {
        private readonly ILabTestRepository _labTestRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IAuditService _auditService;
        private readonly IAuthenticationService _authService;
        private readonly Logger _logger;

        // Valori di riferimento (da DB o configurazione)
        private readonly Dictionary<string, (decimal Min, decimal Max)> _referenceValues = new()
        {
            { "hb", (10.0m, 12.0m) },
            { "hct", (30.0m, 36.0m) },
            { "potassio", (3.5m, 5.5m) },
            { "calcio", (8.4m, 10.2m) },
            { "fosforo", (2.5m, 4.5m) },
            { "pth", (150.0m, 300.0m) },
            { "ferritina", (200.0m, 500.0m) },
            { "albumina", (3.5m, 5.0m) }
        };

        public LabTestService(
            ILabTestRepository labTestRepository,
            IPatientRepository patientRepository,
            IAuditService auditService,
            IAuthenticationService authService)
        {
            _labTestRepository = labTestRepository ?? throw new ArgumentNullException(nameof(labTestRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<IEnumerable<LabTest>> GetTestsByPatientAsync(int patientId)
        {
            try
            {
                return await _labTestRepository.GetByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero degli esami per paziente ID: {patientId}");
                throw;
            }
        }

        public async Task<LabTest> GetTestByIdAsync(int testId)
        {
            try
            {
                return await _labTestRepository.GetByIdAsync(testId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero dell'esame ID: {testId}");
                throw;
            }
        }

        public async Task<LabTest> GetLastTestByPatientAsync(int patientId)
        {
            try
            {
                return await _labTestRepository.GetLastTestByPatientAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante il recupero dell'ultimo esame per paziente ID: {patientId}");
                throw;
            }
        }

        public async Task<(bool Success, int TestId, string ErrorMessage)> CreateLabTestAsync(LabTest labTest)
        {
            try
            {
                _logger.Info($"Creazione nuovo esame laboratorio per paziente ID: {labTest.PatientId}");

                // Validazione
                if (labTest.PatientId <= 0)
                {
                    return (false, 0, "Paziente non valido");
                }

                // Verifica paziente esiste
                var patient = await _patientRepository.GetByIdAsync(labTest.PatientId);
                if (patient == null)
                {
                    return (false, 0, "Paziente non trovato");
                }

                labTest.CreatedBy = _authService.GetCurrentUser()?.UserId;

                // Verifica alert
                await CheckAlertsAsync(labTest);

                // Inserisci esame
                var testId = await _labTestRepository.InsertAsync(labTest);

                if (testId > 0)
                {
                    _logger.Info($"Esame laboratorio creato con successo. ID: {testId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "LAB_TEST_CREATED",
                        "lab_tests",
                        testId,
                        $"Creato esame per paziente ID: {labTest.PatientId}");

                    return (true, testId, null);
                }

                return (false, 0, "Errore durante la creazione dell'esame");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la creazione dell'esame laboratorio");
                return (false, 0, $"Errore: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateLabTestAsync(LabTest labTest)
        {
            try
            {
                _logger.Info($"Aggiornamento esame laboratorio ID: {labTest.TestId}");

                // Verifica esistenza
                var existing = await _labTestRepository.GetByIdAsync(labTest.TestId);
                if (existing == null)
                {
                    return (false, "Esame non trovato");
                }

                // Verifica alert
                await CheckAlertsAsync(labTest);

                // Aggiorna
                var success = await _labTestRepository.UpdateAsync(labTest);

                if (success)
                {
                    _logger.Info($"Esame laboratorio aggiornato con successo. ID: {labTest.TestId}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "LAB_TEST_UPDATED",
                        "lab_tests",
                        labTest.TestId,
                        $"Aggiornato esame per paziente ID: {labTest.PatientId}");

                    return (true, null);
                }

                return (false, "Errore durante l'aggiornamento dell'esame");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante l'aggiornamento dell'esame ID: {labTest.TestId}");
                return (false, $"Errore: {ex.Message}");
            }
        }

        public async Task<bool> CheckAlertsAsync(LabTest labTest)
        {
            try
            {
                bool hasAlert = false;
                var alertDetails = new List<string>();

                // Verifica Emoglobina
                if (labTest.Hb.HasValue)
                {
                    if (labTest.Hb.Value < _referenceValues["hb"].Min)
                    {
                        alertDetails.Add($"Hb bassa: {labTest.Hb} g/dL");
                        hasAlert = true;
                    }
                    else if (labTest.Hb.Value > _referenceValues["hb"].Max)
                    {
                        alertDetails.Add($"Hb alta: {labTest.Hb} g/dL");
                        hasAlert = true;
                    }
                }

                // Verifica Potassio
                if (labTest.Potassio.HasValue)
                {
                    if (labTest.Potassio.Value < _referenceValues["potassio"].Min)
                    {
                        alertDetails.Add($"K+ basso: {labTest.Potassio} mEq/L");
                        hasAlert = true;
                    }
                    else if (labTest.Potassio.Value > _referenceValues["potassio"].Max)
                    {
                        alertDetails.Add($"K+ alto: {labTest.Potassio} mEq/L (URGENTE)");
                        hasAlert = true;
                    }
                }

                // Verifica Calcio
                if (labTest.Calcio.HasValue)
                {
                    if (labTest.Calcio.Value < _referenceValues["calcio"].Min ||
                        labTest.Calcio.Value > _referenceValues["calcio"].Max)
                    {
                        alertDetails.Add($"Ca alterato: {labTest.Calcio} mg/dL");
                        hasAlert = true;
                    }
                }

                // Verifica Fosforo
                if (labTest.Fosforo.HasValue)
                {
                    if (labTest.Fosforo.Value > _referenceValues["fosforo"].Max)
                    {
                        alertDetails.Add($"P alto: {labTest.Fosforo} mg/dL");
                        hasAlert = true;
                    }
                }

                // Verifica PTH
                if (labTest.Pth.HasValue)
                {
                    if (labTest.Pth.Value < _referenceValues["pth"].Min ||
                        labTest.Pth.Value > _referenceValues["pth"].Max)
                    {
                        alertDetails.Add($"PTH fuori range: {labTest.Pth} pg/mL");
                        hasAlert = true;
                    }
                }

                // Verifica Ferritina
                if (labTest.Ferritina.HasValue)
                {
                    if (labTest.Ferritina.Value < _referenceValues["ferritina"].Min)
                    {
                        alertDetails.Add($"Ferritina bassa: {labTest.Ferritina} ng/mL");
                        hasAlert = true;
                    }
                }

                // Verifica Albumina
                if (labTest.Albumina.HasValue)
                {
                    if (labTest.Albumina.Value < _referenceValues["albumina"].Min)
                    {
                        alertDetails.Add($"Albumina bassa: {labTest.Albumina} g/dL");
                        hasAlert = true;
                    }
                }

                labTest.AlertGenerato = hasAlert;

                if (hasAlert)
                {
                    _logger.Warn($"Alert generato per esame paziente ID {labTest.PatientId}: {string.Join(", ", alertDetails)}");
                    await _auditService.LogAsync(
                        _authService.GetCurrentUser()?.UserId,
                        "LAB_ALERT_GENERATED",
                        "lab_tests",
                        labTest.TestId,
                        string.Join("; ", alertDetails));
                }

                return hasAlert;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la verifica degli alert");
                return false;
            }
        }

        public async Task<Dictionary<string, (decimal? Value, bool OutOfRange)>> GetParametersStatusAsync(LabTest labTest)
        {
            var status = new Dictionary<string, (decimal? Value, bool OutOfRange)>();

            try
            {
                status["Hb"] = (labTest.Hb, IsOutOfRange(labTest.Hb, "hb"));
                status["Potassio"] = (labTest.Potassio, IsOutOfRange(labTest.Potassio, "potassio"));
                status["Calcio"] = (labTest.Calcio, IsOutOfRange(labTest.Calcio, "calcio"));
                status["Fosforo"] = (labTest.Fosforo, IsOutOfRange(labTest.Fosforo, "fosforo"));
                status["PTH"] = (labTest.Pth, IsOutOfRange(labTest.Pth, "pth"));
                status["Ferritina"] = (labTest.Ferritina, IsOutOfRange(labTest.Ferritina, "ferritina"));
                status["Albumina"] = (labTest.Albumina, IsOutOfRange(labTest.Albumina, "albumina"));

                return await Task.FromResult(status);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il calcolo dello stato dei parametri");
                return status;
            }
        }

        private bool IsOutOfRange(decimal? value, string parameterKey)
        {
            if (!value.HasValue || !_referenceValues.ContainsKey(parameterKey))
            {
                return false;
            }

            var (min, max) = _referenceValues[parameterKey];
            return value.Value < min || value.Value > max;
        }
    }
}
