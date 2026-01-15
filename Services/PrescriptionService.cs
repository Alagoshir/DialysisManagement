using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DialysisManagement.Models;
using DialysisManagement.Repositories;
using Microsoft.Extensions.Logging;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Servizio per gestione prescrizioni dialitiche e farmacologiche
    /// </summary>
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IMedicationRepository _medicationRepository;
        private readonly IAuditService _auditService;
        private readonly ILogger<PrescriptionService> _logger;

        public PrescriptionService(
            IPrescriptionRepository prescriptionRepository,
            IMedicationRepository medicationRepository,
            IAuditService auditService = null,
            ILogger<PrescriptionService> logger = null)
        {
            _prescriptionRepository = prescriptionRepository ?? throw new ArgumentNullException(nameof(prescriptionRepository));
            _medicationRepository = medicationRepository ?? throw new ArgumentNullException(nameof(medicationRepository));
            _auditService = auditService;
            _logger = logger;
        }

        /// <summary>
        /// Recupera prescrizione per ID
        /// </summary>
        public async Task<Prescription> GetPrescriptionByIdAsync(int prescriptionId)
        {
            try
            {
                _logger?.LogInformation($"Recupero prescrizione ID: {prescriptionId}");
                return await _prescriptionRepository.GetByIdAsync(prescriptionId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante il recupero della prescrizione {prescriptionId}");
                throw;
            }
        }

        /// <summary>
        /// Recupera tutte le prescrizioni di un paziente
        /// </summary>
        public async Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId)
        {
            try
            {
                _logger?.LogInformation($"Recupero prescrizioni per paziente ID: {patientId}");
                return await _prescriptionRepository.GetByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante il recupero delle prescrizioni del paziente {patientId}");
                throw;
            }
        }

        /// <summary>
        /// Recupera prescrizione dialitica attiva per paziente
        /// </summary>
        public async Task<Prescription> GetActivePrescriptionAsync(int patientId)
        {
            try
            {
                _logger?.LogInformation($"Recupero prescrizione attiva per paziente ID: {patientId}");
                var prescriptions = await _prescriptionRepository.GetActiveByPatientIdAsync(patientId);
                return prescriptions.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante il recupero della prescrizione attiva del paziente {patientId}");
                throw;
            }
        }

        /// <summary>
        /// Recupera storico prescrizioni paziente
        /// </summary>
        public async Task<IEnumerable<Prescription>> GetPrescriptionHistoryAsync(int patientId)
        {
            try
            {
                _logger?.LogInformation($"Recupero storico prescrizioni per paziente ID: {patientId}");
                return await _prescriptionRepository.GetByPatientIdAsync(patientId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante il recupero dello storico prescrizioni del paziente {patientId}");
                throw;
            }
        }

        /// <summary>
        /// Crea nuova prescrizione
        /// </summary>
        public async Task<int> CreatePrescriptionAsync(Prescription prescription)
        {
            try
            {
                if (prescription == null)
                    throw new ArgumentNullException(nameof(prescription));

                // Validazioni
                ValidatePrescription(prescription);

                _logger?.LogInformation($"Creazione nuova prescrizione per paziente ID: {prescription.PatientId}");

                // Inserisci prescrizione
                var prescriptionId = await _prescriptionRepository.InsertAsync(prescription);

                // Audit log
                await _auditService?.LogActionAsync(
                    "INSERT",
                    "Prescription",
                    prescriptionId,
                    null,
                    Newtonsoft.Json.JsonConvert.SerializeObject(prescription));

                _logger?.LogInformation($"Prescrizione creata con ID: {prescriptionId}");
                return prescriptionId;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Errore durante la creazione della prescrizione");
                throw;
            }
        }

        /// <summary>
        /// Aggiorna prescrizione esistente
        /// </summary>
        public async Task<bool> UpdatePrescriptionAsync(Prescription prescription)
        {
            try
            {
                if (prescription == null)
                    throw new ArgumentNullException(nameof(prescription));

                // Validazioni
                ValidatePrescription(prescription);

                _logger?.LogInformation($"Aggiornamento prescrizione ID: {prescription.PrescriptionId}");

                // Recupera prescrizione originale per audit
                var oldPrescription = await _prescriptionRepository.GetByIdAsync(prescription.PrescriptionId);

                // Aggiorna
                var result = await _prescriptionRepository.UpdateAsync(prescription);

                if (result)
                {
                    // Audit log
                    await _auditService?.LogActionAsync(
                        "UPDATE",
                        "Prescription",
                        prescription.PrescriptionId,
                        Newtonsoft.Json.JsonConvert.SerializeObject(oldPrescription),
                        Newtonsoft.Json.JsonConvert.SerializeObject(prescription));

                    _logger?.LogInformation($"Prescrizione {prescription.PrescriptionId} aggiornata");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante l'aggiornamento della prescrizione {prescription?.PrescriptionId}");
                throw;
            }
        }

        /// <summary>
        /// Elimina prescrizione
        /// </summary>
        public async Task<bool> DeletePrescriptionAsync(int prescriptionId)
        {
            try
            {
                _logger?.LogInformation($"Eliminazione prescrizione ID: {prescriptionId}");

                // Recupera per audit
                var prescription = await _prescriptionRepository.GetByIdAsync(prescriptionId);

                // Elimina
                var result = await _prescriptionRepository.DeleteAsync(prescriptionId);

                if (result)
                {
                    // Audit log
                    await _auditService?.LogActionAsync(
                        "DELETE",
                        "Prescription",
                        prescriptionId,
                        Newtonsoft.Json.JsonConvert.SerializeObject(prescription),
                        null);

                    _logger?.LogInformation($"Prescrizione {prescriptionId} eliminata");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante l'eliminazione della prescrizione {prescriptionId}");
                throw;
            }
        }

        /// <summary>
        /// Verifica validità prescrizione (date validità)
        /// </summary>
        public async Task<bool> IsPrescriptionValidAsync(int prescriptionId)
        {
            try
            {
                var prescription = await _prescriptionRepository.GetByIdAsync(prescriptionId);

                if (prescription == null)
                    return false;

                var today = DateTime.Today;

                return prescription.DataInizio <= today &&
                       (prescription.DataFine == null || prescription.DataFine >= today);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante la verifica validità prescrizione {prescriptionId}");
                return false;
            }
        }

        /// <summary>
        /// Recupera farmaci della prescrizione
        /// </summary>
        public async Task<IEnumerable<Medication>> GetPrescriptionMedicationsAsync(int prescriptionId)
        {
            try
            {
                _logger?.LogInformation($"Recupero farmaci per prescrizione ID: {prescriptionId}");
                return await _medicationRepository.GetByPrescriptionIdAsync(prescriptionId);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante il recupero dei farmaci della prescrizione {prescriptionId}");
                throw;
            }
        }

        /// <summary>
        /// Aggiunge farmaco alla prescrizione
        /// </summary>
        public async Task<int> AddMedicationToPrescriptionAsync(int prescriptionId, Medication medication)
        {
            try
            {
                if (medication == null)
                    throw new ArgumentNullException(nameof(medication));

                medication.PrescriptionId = prescriptionId;

                _logger?.LogInformation($"Aggiunta farmaco alla prescrizione ID: {prescriptionId}");

                var medicationId = await _medicationRepository.InsertAsync(medication);

                // Audit log
                await _auditService?.LogActionAsync(
                    "INSERT",
                    "Medication",
                    medicationId,
                    null,
                    Newtonsoft.Json.JsonConvert.SerializeObject(medication));

                return medicationId;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante l'aggiunta del farmaco alla prescrizione {prescriptionId}");
                throw;
            }
        }

        /// <summary>
        /// Rimuove farmaco dalla prescrizione
        /// </summary>
        public async Task<bool> RemoveMedicationFromPrescriptionAsync(int medicationId)
        {
            try
            {
                _logger?.LogInformation($"Rimozione farmaco ID: {medicationId}");

                // Recupera per audit
                var medication = await _medicationRepository.GetByIdAsync(medicationId);

                // Elimina
                var result = await _medicationRepository.DeleteAsync(medicationId);

                if (result)
                {
                    // Audit log
                    await _auditService?.LogActionAsync(
                        "DELETE",
                        "Medication",
                        medicationId,
                        Newtonsoft.Json.JsonConvert.SerializeObject(medication),
                        null);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"Errore durante la rimozione del farmaco {medicationId}");
                throw;
            }
        }

        #region Private Methods

        /// <summary>
        /// Valida dati prescrizione
        /// </summary>
        private void ValidatePrescription(Prescription prescription)
        {
            if (prescription.PatientId <= 0)
                throw new ArgumentException("ID paziente non valido");

            if (prescription.DataInizio == null)
                throw new ArgumentException("Data inizio prescrizione obbligatoria");

            if (prescription.DataFine.HasValue && prescription.DataFine < prescription.DataInizio)
                throw new ArgumentException("Data fine non può essere precedente a data inizio");

            // Validazioni specifiche parametri dialisi
            if (prescription.FlussoPompaSangue.HasValue &&
                (prescription.FlussoPompaSangue < 100 || prescription.FlussoPompaSangue > 500))
                throw new ArgumentException("Flusso pompa sangue deve essere tra 100 e 500 ml/min");

            if (prescription.DurataSeduta.HasValue &&
                (prescription.DurataSeduta < 120 || prescription.DurataSeduta > 360))
                throw new ArgumentException("Durata seduta deve essere tra 120 e 360 minuti");
        }

        #endregion
    }
}
