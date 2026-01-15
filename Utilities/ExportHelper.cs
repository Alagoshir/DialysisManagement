using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace DialysisManagement.Utilities
{
    /// <summary>
    /// Helper per export dati in vari formati
    /// </summary>
    public interface IExportHelper
    {
        bool ExportToCsv<T>(IEnumerable<T> data, string filePath, char delimiter = ';');
        bool ExportToXml<T>(IEnumerable<T> data, string filePath, string rootElement = "Root");
        bool ExportDataTableToCsv(DataTable dataTable, string filePath, char delimiter = ';');
        string GenerateSDOFile(int patientId, DateTime startDate, DateTime endDate);
        string GenerateRegistroDialisiFile(DateTime startDate, DateTime endDate);
    }

    public class ExportHelper : IExportHelper
    {
        /// <summary>
        /// Esporta collezione in CSV
        /// </summary>
        public bool ExportToCsv<T>(IEnumerable<T> data, string filePath, char delimiter = ';')
        {
            try
            {
                var config = new CsvConfiguration(new CultureInfo("it-IT"))
                {
                    Delimiter = delimiter.ToString(),
                    Encoding = Encoding.UTF8,
                    HasHeaderRecord = true
                };

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(data);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }

        /// <summary>
        /// Esporta collezione in XML
        /// </summary>
        public bool ExportToXml<T>(IEnumerable<T> data, string filePath, string rootElement = "Root")
        {
            try
            {
                var xDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(rootElement)
                );

                var root = xDoc.Root;
                var itemName = typeof(T).Name;

                foreach (var item in data)
                {
                    var xItem = new XElement(itemName);

                    foreach (var prop in typeof(T).GetProperties())
                    {
                        var value = prop.GetValue(item);
                        if (value != null)
                        {
                            xItem.Add(new XElement(prop.Name, value.ToString()));
                        }
                    }

                    root.Add(xItem);
                }

                xDoc.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }

        /// <summary>
        /// Esporta DataTable in CSV
        /// </summary>
        public bool ExportDataTableToCsv(DataTable dataTable, string filePath, char delimiter = ';')
        {
            try
            {
                var sb = new StringBuilder();

                // Header
                var columnNames = dataTable.Columns.Cast<DataColumn>()
                    .Select(column => QuoteCsvValue(column.ColumnName));
                sb.AppendLine(string.Join(delimiter, columnNames));

                // Righe
                foreach (DataRow row in dataTable.Rows)
                {
                    var fields = row.ItemArray.Select(field => QuoteCsvValue(field?.ToString() ?? ""));
                    sb.AppendLine(string.Join(delimiter, fields));
                }

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                // Log error
                return false;
            }
        }

        /// <summary>
        /// Quote valore CSV se contiene caratteri speciali
        /// </summary>
        private string QuoteCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value.Contains(';') || value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                return $"\"{value.Replace("\"", "\"\"")}\"";
            }

            return value;
        }

        /// <summary>
        /// Genera file SDO (Scheda Dimissione Ospedaliera)
        /// </summary>
        public string GenerateSDOFile(int patientId, DateTime startDate, DateTime endDate)
        {
            try
            {
                // TODO: Implementare generazione file SDO secondo tracciato ministeriale
                // Formato: record tracciato con posizioni fisse

                var fileName = $"SDO_{patientId}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.txt";
                var filePath = Path.Combine(ConfigurationHelper.GetReportsPath(), fileName);

                // Placeholder - implementare tracciato reale
                var sb = new StringBuilder();
                sb.AppendLine("# File SDO - Scheda Dimissione Ospedaliera");
                sb.AppendLine($"# Paziente: {patientId}");
                sb.AppendLine($"# Periodo: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}");
                sb.AppendLine($"# Generato: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return filePath;
            }
            catch (Exception ex)
            {
                // Log error
                return null;
            }
        }

        /// <summary>
        /// Genera file Registro Dialisi Regionale (Campania)
        /// </summary>
        public string GenerateRegistroDialisiFile(DateTime startDate, DateTime endDate)
        {
            try
            {
                // TODO: Implementare secondo specifiche Regione Campania
                // Formato: CSV o XML secondo tracciato regionale

                var fileName = $"RegistroDialisi_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.csv";
                var filePath = Path.Combine(ConfigurationHelper.GetReportsPath(), fileName);

                // Placeholder - implementare tracciato reale
                var sb = new StringBuilder();
                sb.AppendLine("CentroCodice;DataSeduta;CodicePaziente;TipoTrattamento;DurataMinuti;UF;PesoSecco");

                File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
                return filePath;
            }
            catch (Exception ex)
            {
                // Log error
                return null;
            }
        }
    }
}
