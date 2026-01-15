using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DialysisManagement.Models;

namespace DialysisManagement.Services
{
    /// <summary>
    /// Interfaccia per servizio gestione prescrizioni dialitiche e farmacologiche
    /// </summary>
    public interface IPrescriptionService
    {
        /// <summary>
        /// Recupera prescrizione per ID
        /// </summary>
        Task<Prescription> GetPrescriptionByIdAsync(int prescriptionId);

        /// <summary>
        /// Recupera tutte le prescrizioni di un paziente
        /// </summary>
        Task<IEnumerable<Prescription>> GetPrescriptionsByPatientIdAsync(int patientId);

        /// <summary>
        /// Recupera prescrizione dialitica attiva per paziente
        /// </summary>
        Task<Prescription> GetActivePrescriptionAsync(int patientId);

        /// <summary>
        /// Recupera storico prescrizioni paziente
        /// </summary>
        Task<IEnumerable<Prescription>> GetPrescriptionHistoryAsync(int patientId);

        /// <summary>
        /// Crea nuova prescrizione
        /// </summary>
        Task<int> CreatePrescriptionAsync(Prescription prescription);

        /// <summary>
        /// Aggiorna prescrizione esistente
        /// </summary>
        Task<bool> UpdatePrescriptionAsync(Prescription prescription);

        /// <summary>
        /// Elimina prescrizione
        /// </summary>
        Task<bool> DeletePrescriptionAsync(int prescriptionId);

        /// <summary>
        /// Verifica validità prescrizione
        /// </summary>
        Task<bool> IsPrescriptionValidAsync(int prescriptionId);

        /// <summary>
        /// Recupera farmaci della prescrizione
        /// </summary>
        Task<IEnumerable<Medication>> GetPrescriptionMedicationsAsync(int prescriptionId);

        /// <summary>
        /// Aggiunge farmaco alla prescrizione
        /// </summary>
        Task<int> AddMedicationToPrescriptionAsync(int prescriptionId, Medication medication);

        /// <summary>
        /// Rimuove farmaco dalla prescrizione
        /// </summary>
        Task<bool> RemoveMedicationFromPrescriptionAsync(int medicationId);
    }
}
