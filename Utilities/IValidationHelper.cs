namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Interfaccia per validazione dati
    /// </summary>
    public interface IValidationHelper
    {
        /// <summary>
        /// Valida codice fiscale italiano
        /// </summary>
        bool IsValidCodiceFiscale(string codiceFiscale);

        /// <summary>
        /// Valida email
        /// </summary>
        bool IsValidEmail(string email);

        /// <summary>
        /// Valida numero telefono italiano
        /// </summary>
        bool IsValidPhoneNumber(string phoneNumber);

        /// <summary>
        /// Valida CAP italiano
        /// </summary>
        bool IsValidCAP(string cap);

        /// <summary>
        /// Valida password (requisiti minimi)
        /// </summary>
        bool IsValidPassword(string password, int minLength = 8);

        /// <summary>
        /// Valida data di nascita (età ragionevole)
        /// </summary>
        bool IsValidBirthDate(System.DateTime birthDate);

        /// <summary>
        /// Valida stringa non vuota
        /// </summary>
        bool IsNotEmpty(string value);

        /// <summary>
        /// Valida range numerico
        /// </summary>
        bool IsInRange(decimal value, decimal min, decimal max);

        /// <summary>
        /// Calcola check digit codice fiscale
        /// </summary>
        char CalculateCodiceFiscaleCheckDigit(string codiceFiscale);

        /// <summary>
        /// Normalizza codice fiscale (uppercase, trim)
        /// </summary>
        string NormalizeCodiceFiscale(string codiceFiscale);

        /// <summary>
        /// Ottiene messaggio errore validazione
        /// </summary>
        string GetValidationErrorMessage(string fieldName, string errorType);
    }
}
