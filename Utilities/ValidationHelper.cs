using System;
using System.Text.RegularExpressions;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per validazione dati
    /// </summary>
    public class ValidationHelper : IValidationHelper
    {
        #region Codice Fiscale

        /// <summary>
        /// Valida codice fiscale italiano
        /// </summary>
        public bool IsValidCodiceFiscale(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
                return false;

            // Normalizza
            codiceFiscale = NormalizeCodiceFiscale(codiceFiscale);

            // Lunghezza esatta 16 caratteri
            if (codiceFiscale.Length != 16)
                return false;

            // Pattern: 6 lettere + 2 cifre + 1 lettera + 2 cifre + 1 lettera + 3 cifre + 1 lettera
            var pattern = @"^[A-Z]{6}[0-9]{2}[A-Z][0-9]{2}[A-Z][0-9]{3}[A-Z]$";
            if (!Regex.IsMatch(codiceFiscale, pattern))
                return false;

            // Verifica check digit (ultimo carattere)
            var calculatedCheckDigit = CalculateCodiceFiscaleCheckDigit(codiceFiscale.Substring(0, 15));
            return codiceFiscale[15] == calculatedCheckDigit;
        }

        /// <summary>
        /// Calcola check digit codice fiscale
        /// </summary>
        public char CalculateCodiceFiscaleCheckDigit(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale) || codiceFiscale.Length != 15)
                return '?';

            // Valori per caratteri in posizione dispari (1, 3, 5, ...)
            var oddValues = new System.Collections.Generic.Dictionary<char, int>
            {
                {'0', 1}, {'1', 0}, {'2', 5}, {'3', 7}, {'4', 9}, {'5', 13}, {'6', 15}, {'7', 17}, {'8', 19}, {'9', 21},
                {'A', 1}, {'B', 0}, {'C', 5}, {'D', 7}, {'E', 9}, {'F', 13}, {'G', 15}, {'H', 17}, {'I', 19}, {'J', 21},
                {'K', 2}, {'L', 4}, {'M', 18}, {'N', 20}, {'O', 11}, {'P', 3}, {'Q', 6}, {'R', 8}, {'S', 12}, {'T', 14},
                {'U', 16}, {'V', 10}, {'W', 22}, {'X', 25}, {'Y', 24}, {'Z', 23}
            };

            // Valori per caratteri in posizione pari (2, 4, 6, ...)
            var evenValues = new System.Collections.Generic.Dictionary<char, int>
            {
                {'0', 0}, {'1', 1}, {'2', 2}, {'3', 3}, {'4', 4}, {'5', 5}, {'6', 6}, {'7', 7}, {'8', 8}, {'9', 9},
                {'A', 0}, {'B', 1}, {'C', 2}, {'D', 3}, {'E', 4}, {'F', 5}, {'G', 6}, {'H', 7}, {'I', 8}, {'J', 9},
                {'K', 10}, {'L', 11}, {'M', 12}, {'N', 13}, {'O', 14}, {'P', 15}, {'Q', 16}, {'R', 17}, {'S', 18}, {'T', 19},
                {'U', 20}, {'V', 21}, {'W', 22}, {'X', 23}, {'Y', 24}, {'Z', 25}
            };

            // Caratteri per check digit
            var checkChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int sum = 0;
            for (int i = 0; i < 15; i++)
            {
                char c = char.ToUpper(codiceFiscale[i]);
                if (i % 2 == 0) // Posizione dispari (indice pari, si parte da 0)
                {
                    sum += oddValues.ContainsKey(c) ? oddValues[c] : 0;
                }
                else // Posizione pari
                {
                    sum += evenValues.ContainsKey(c) ? evenValues[c] : 0;
                }
            }

            int remainder = sum % 26;
            return checkChars[remainder];
        }

        /// <summary>
        /// Normalizza codice fiscale
        /// </summary>
        public string NormalizeCodiceFiscale(string codiceFiscale)
        {
            if (string.IsNullOrWhiteSpace(codiceFiscale))
                return string.Empty;

            return codiceFiscale.Trim().ToUpperInvariant();
        }

        #endregion

        #region Email

        /// <summary>
        /// Valida email
        /// </summary>
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Pattern RFC 5322 semplificato
                var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email.Trim(), pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Telefono

        /// <summary>
        /// Valida numero telefono italiano
        /// </summary>
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;

            // Rimuove spazi, trattini, parentesi
            var cleaned = Regex.Replace(phoneNumber, @"[\s\-\(\)\/\.]", "");

            // Pattern telefono italiano:
            // - Fisso: 06/081/etc + 6-8 cifre (totale 9-11 cifre)
            // - Mobile: 3xx (10 cifre totali)
            // - Con prefisso internazionale: +39 o 0039
            var patterns = new[]
            {
                @"^[0-9]{9,11}$",                    // Numero locale
                @"^\+39[0-9]{9,10}$",                // Con +39
                @"^0039[0-9]{9,10}$"                 // Con 0039
            };

            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(cleaned, pattern))
                    return true;
            }

            return false;
        }

        #endregion

        #region CAP

        /// <summary>
        /// Valida CAP italiano
        /// </summary>
        public bool IsValidCAP(string cap)
        {
            if (string.IsNullOrWhiteSpace(cap))
                return false;

            // CAP italiano: esattamente 5 cifre
            return Regex.IsMatch(cap.Trim(), @"^[0-9]{5}$");
        }

        #endregion

        #region Password

        /// <summary>
        /// Valida password con requisiti minimi
        /// </summary>
        public bool IsValidPassword(string password, int minLength = 8)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < minLength)
                return false;

            // Opzionale: requisiti aggiuntivi
            // - Almeno una maiuscola
            // - Almeno una minuscola
            // - Almeno un numero
            // - Almeno un carattere speciale

            bool hasUpper = Regex.IsMatch(password, @"[A-Z]");
            bool hasLower = Regex.IsMatch(password, @"[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"[0-9]");
            // bool hasSpecial = Regex.IsMatch(password, @"[!@#$%^&*(),.?""':{}|<>]");

            return hasUpper && hasLower && hasDigit;
        }

        #endregion

        #region Data di Nascita

        /// <summary>
        /// Valida data di nascita (età ragionevole)
        /// </summary>
        public bool IsValidBirthDate(DateTime birthDate)
        {
            // Età minima: 0 anni (neonati)
            // Età massima: 120 anni
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate > today.AddYears(-age))
                age--;

            return age >= 0 && age <= 120 && birthDate <= today;
        }

        #endregion

        #region Validazioni Generiche

        /// <summary>
        /// Valida stringa non vuota
        /// </summary>
        public bool IsNotEmpty(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Valida range numerico
        /// </summary>
        public bool IsInRange(decimal value, decimal min, decimal max)
        {
            return value >= min && value <= max;
        }

        #endregion

        #region Messaggi Errore

        /// <summary>
        /// Ottiene messaggio errore validazione
        /// </summary>
        public string GetValidationErrorMessage(string fieldName, string errorType)
        {
            var messages = new System.Collections.Generic.Dictionary<string, string>
            {
                { "required", $"Il campo '{fieldName}' è obbligatorio." },
                { "invalid_format", $"Il formato del campo '{fieldName}' non è valido." },
                { "invalid_email", $"L'indirizzo email '{fieldName}' non è valido." },
                { "invalid_phone", $"Il numero di telefono '{fieldName}' non è valido." },
                { "invalid_cf", $"Il codice fiscale '{fieldName}' non è valido." },
                { "invalid_cap", $"Il CAP '{fieldName}' non è valido." },
                { "invalid_password", $"La password non soddisfa i requisiti minimi." },
                { "invalid_date", $"La data '{fieldName}' non è valida." },
                { "out_of_range", $"Il valore di '{fieldName}' è fuori dal range consentito." },
                { "too_long", $"Il campo '{fieldName}' è troppo lungo." },
                { "too_short", $"Il campo '{fieldName}' è troppo corto." }
            };

            return messages.ContainsKey(errorType)
                ? messages[errorType]
                : $"Errore di validazione nel campo '{fieldName}'.";
        }

        #endregion
    }
}
