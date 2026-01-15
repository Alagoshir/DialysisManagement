using System;
using System.Globalization;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per operazioni su date
    /// </summary>
    public static class DateTimeHelper
    {
        private static readonly CultureInfo ItalianCulture = new CultureInfo("it-IT");

        /// <summary>
        /// Calcola età in anni
        /// </summary>
        public static int CalculateAge(DateTime birthDate, DateTime? referenceDate = null)
        {
            var reference = referenceDate ?? DateTime.Today;
            var age = reference.Year - birthDate.Year;

            if (birthDate.Date > reference.AddYears(-age))
                age--;

            return age;
        }

        /// <summary>
        /// Restituisce inizio settimana (lunedì)
        /// </summary>
        public static DateTime GetStartOfWeek(DateTime date)
        {
            var diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
            return date.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Restituisce fine settimana (domenica)
        /// </summary>
        public static DateTime GetEndOfWeek(DateTime date)
        {
            return GetStartOfWeek(date).AddDays(6);
        }

        /// <summary>
        /// Restituisce inizio mese
        /// </summary>
        public static DateTime GetStartOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Restituisce fine mese
        /// </summary>
        public static DateTime GetEndOfMonth(DateTime date)
        {
            return GetStartOfMonth(date).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Formatta data in italiano
        /// </summary>
        public static string FormatItalian(DateTime date, string format = "dd/MM/yyyy")
        {
            return date.ToString(format, ItalianCulture);
        }

        /// <summary>
        /// Formatta data e ora in italiano
        /// </summary>
        public static string FormatItalianDateTime(DateTime dateTime, string format = "dd/MM/yyyy HH:mm")
        {
            return dateTime.ToString(format, ItalianCulture);
        }

        /// <summary>
        /// Verifica se una data è in un range
        /// </summary>
        public static bool IsInRange(DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date <= endDate;
        }

        /// <summary>
        /// Calcola differenza in giorni lavorativi
        /// </summary>
        public static int GetWorkingDays(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return 0;

            int days = 0;
            var current = startDate.Date;

            while (current <= endDate.Date)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday &&
                    current.DayOfWeek != DayOfWeek.Sunday)
                {
                    days++;
                }
                current = current.AddDays(1);
            }

            return days;
        }

        /// <summary>
        /// Restituisce descrizione relativa ("oggi", "ieri", "domani")
        /// </summary>
        public static string GetRelativeDescription(DateTime date)
        {
            var today = DateTime.Today;
            var diff = (date.Date - today).Days;

            return diff switch
            {
                0 => "Oggi",
                -1 => "Ieri",
                1 => "Domani",
                _ when diff < -1 && diff >= -7 => $"{Math.Abs(diff)} giorni fa",
                _ when diff > 1 && diff <= 7 => $"Tra {diff} giorni",
                _ => FormatItalian(date)
            };
        }

        /// <summary>
        /// Verifica se data è festività italiana
        /// </summary>
        public static bool IsItalianHoliday(DateTime date)
        {
            // Festività fisse
            if ((date.Month == 1 && date.Day == 1) ||   // Capodanno
                (date.Month == 1 && date.Day == 6) ||   // Epifania
                (date.Month == 4 && date.Day == 25) ||  // Liberazione
                (date.Month == 5 && date.Day == 1) ||   // Festa Lavoratori
                (date.Month == 6 && date.Day == 2) ||   // Festa Repubblica
                (date.Month == 8 && date.Day == 15) ||  // Ferragosto
                (date.Month == 11 && date.Day == 1) ||  // Ognissanti
                (date.Month == 12 && date.Day == 8) ||  // Immacolata
                (date.Month == 12 && date.Day == 25) || // Natale
                (date.Month == 12 && date.Day == 26))   // Santo Stefano
            {
                return true;
            }

            // Pasqua e Lunedì dell'Angelo (calcolo complesso, semplificato)
            return false;
        }

        /// <summary>
        /// Converte TimeSpan in formato ore:minuti
        /// </summary>
        public static string FormatDuration(TimeSpan duration)
        {
            return $"{(int)duration.TotalHours:00}:{duration.Minutes:00}";
        }

        /// <summary>
        /// Parse data italiana
        /// </summary>
        public static DateTime? ParseItalianDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            if (DateTime.TryParse(dateString, ItalianCulture, DateTimeStyles.None, out DateTime result))
                return result;

            return null;
        }
    }
}
