using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DialysisManagement.Repositories;
using NLog;

namespace DialysisManagement.Services
{
    public class AlertService : IAlertService
    {
        private readonly ILabTestRepository _labTestRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly Logger _logger;

        public AlertService(
            ILabTestRepository labTestRepository,
            IPatientRepository patientRepository)
        {
            _labTestRepository = labTestRepository ?? throw new ArgumentNullException(nameof(labTestRepository));
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<List<string>> CheckLabAlertsAsync()
        {
            var alerts = new List<string>();

            try
            {
                var testsWithAlerts = await _labTestRepository.GetTestsWithAlertsAsync();

                foreach (var test in testsWithAlerts)
                {
                    var patient = await _patientRepository.GetByIdAsync(test.PatientId);
                    if (patient != null)
                    {
                        alerts.Add($"Alert laboratorio per {patient.Nome} {patient.Cognome} - Data: {test.DataPrelievo:dd/MM/yyyy}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la verifica degli alert laboratorio");
            }

            return alerts;
        }

        public async Task<List<string>> CheckMaintenanceAlertsAsync()
        {
            var alerts = new List<string>();

            try
            {
                // TODO: Implementare verifica manutenzioni in scadenza
                // Query device_maintenance per date prossime
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la verifica degli alert manutenzioni");
            }

            return await Task.FromResult(alerts);
        }

        public async Task<List<string>> CheckVaccinationAlertsAsync()
        {
            var alerts = new List<string>();

            try
            {
                // TODO: Implementare verifica vaccinazioni in scadenza
                // Query vaccinations per richiami vicini
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la verifica degli alert vaccinazioni");
            }

            return await Task.FromResult(alerts);
        }

        public async Task<Dictionary<string, List<string>>> GetAllAlertsAsync()
        {
            var allAlerts = new Dictionary<string, List<string>>();

            try
            {
                allAlerts["Laboratorio"] = await CheckLabAlertsAsync();
                allAlerts["Manutenzioni"] = await CheckMaintenanceAlertsAsync();
                allAlerts["Vaccinazioni"] = await CheckVaccinationAlertsAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il recupero di tutti gli alert");
            }

            return allAlerts;
        }
    }
}
