using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DialysisManagement.Repositories;
using DialysisManagement.Utilities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using NLog;

namespace DialysisManagement.Services
{
    public class ReportService : IReportService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IDialysisSessionRepository _sessionRepository;
        private readonly ILabTestRepository _labTestRepository;
        private readonly IAuditService _auditService;
        private readonly IAuthenticationService _authService;
        private readonly Logger _logger;
        private readonly string _reportsPath;

        public ReportService(
            IPatientRepository patientRepository,
            IDialysisSessionRepository sessionRepository,
            ILabTestRepository labTestRepository,
            IAuditService auditService,
            IAuthenticationService authService)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _sessionRepository = sessionRepository ?? throw new ArgumentNullException(nameof(sessionRepository));
            _labTestRepository = labTestRepository ?? throw new ArgumentNullException(nameof(labTestRepository));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = LogManager.GetCurrentClassLogger();

            _reportsPath = ConfigurationHelper.GetAppSetting("ReportsPath", ".\\Reports\\");

            if (!Directory.Exists(_reportsPath))
            {
                Directory.CreateDirectory(_reportsPath);
            }
        }

        public async Task<string> GeneratePatientReportAsync(int patientId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                _logger.Info($"Generazione report paziente ID: {patientId}");

                var patient = await _patientRepository.GetByIdAsync(patientId);
                if (patient == null)
                {
                    return null;
                }

                var fileName = $"Report_Paziente_{patient.Cognome}_{patient.Nome}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                var filePath = Path.Combine(_reportsPath, fileName);

                // Recupera dati
                var sessions = await _sessionRepository.GetByPatientAndDateRangeAsync(
                    patientId,
                    startDate ?? DateTime.Now.AddMonths(-3),
                    endDate ?? DateTime.Now);

                var labTests = await _labTestRepository.GetByPatientIdAsync(patientId);

                // Genera PDF
                using (var writer = new PdfWriter(filePath))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    // Intestazione
                    document.Add(new Paragraph("REPORT PAZIENTE")
                        .SetFontSize(20)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph($"Centro Dialisi - {ConfigurationHelper.GetAppSetting("centro_nome", "ASL Napoli 1")}")
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph($"Data: {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .SetFontSize(10)
                        .SetTextAlignment(TextAlignment.RIGHT));

                    document.Add(new Paragraph("\n"));

                    // Dati paziente
                    document.Add(new Paragraph("DATI ANAGRAFICI")
                        .SetFontSize(14)
                        .SetBold());

                    document.Add(new Paragraph($"Nome: {patient.Nome} {patient.Cognome}"));
                    document.Add(new Paragraph($"Codice Fiscale: {patient.CodiceFiscale}"));
                    document.Add(new Paragraph($"Data di Nascita: {patient.DataNascita:dd/MM/yyyy} (Età: {patient.Eta} anni)"));
                    document.Add(new Paragraph($"Gruppo Sanguigno: {patient.GruppoSanguigno ?? "N/D"}"));
                    document.Add(new Paragraph($"Peso Secco: {patient.PesoSecco?.ToString("F2") ?? "N/D"} kg"));

                    document.Add(new Paragraph("\n"));

                    // Riepilogo sedute
                    document.Add(new Paragraph("RIEPILOGO SEDUTE DIALITICHE")
                        .SetFontSize(14)
                        .SetBold());

                    var sessionsList = sessions.ToList();
                    document.Add(new Paragraph($"Numero sedute: {sessionsList.Count}"));

                    if (sessionsList.Any())
                    {
                        var avgKtv = sessionsList.Where(s => s.Ktv.HasValue).Average(s => s.Ktv);
                        var avgUf = sessionsList.Where(s => s.UfEffettuata.HasValue).Average(s => s.UfEffettuata);

                        document.Add(new Paragraph($"Kt/V medio: {avgKtv?.ToString("F2") ?? "N/D"}"));
                        document.Add(new Paragraph($"UF media: {avgUf?.ToString("F0") ?? "N/D"} ml"));
                    }

                    document.Add(new Paragraph("\n"));

                    // Ultimi esami
                    document.Add(new Paragraph("ULTIMI ESAMI DI LABORATORIO")
                        .SetFontSize(14)
                        .SetBold());

                    var lastTest = labTests.OrderByDescending(t => t.DataPrelievo).FirstOrDefault();
                    if (lastTest != null)
                    {
                        document.Add(new Paragraph($"Data prelievo: {lastTest.DataPrelievo:dd/MM/yyyy}"));
                        document.Add(new Paragraph($"Emoglobina: {lastTest.Hb?.ToString("F1") ?? "N/D"} g/dL"));
                        document.Add(new Paragraph($"Potassio: {lastTest.Potassio?.ToString("F1") ?? "N/D"} mEq/L"));
                        document.Add(new Paragraph($"Calcio: {lastTest.Calcio?.ToString("F1") ?? "N/D"} mg/dL"));
                        document.Add(new Paragraph($"Fosforo: {lastTest.Fosforo?.ToString("F1") ?? "N/D"} mg/dL"));
                        document.Add(new Paragraph($"PTH: {lastTest.Pth?.ToString("F0") ?? "N/D"} pg/mL"));
                    }
                }

                _logger.Info($"Report generato: {filePath}");

                await _auditService.LogAsync(
                    _authService.GetCurrentUser()?.UserId,
                    "REPORT_GENERATED",
                    "patients",
                    patientId,
                    $"Report paziente generato: {fileName}");

                return filePath;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore durante la generazione del report paziente ID: {patientId}");
                return null;
            }
        }

        public async Task<string> GenerateSessionsReportAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                _logger.Info($"Generazione report sedute dal {startDate:yyyy-MM-dd} al {endDate:yyyy-MM-dd}");

                var fileName = $"Report_Sedute_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.pdf";
                var filePath = Path.Combine(_reportsPath, fileName);

                var sessions = await _sessionRepository.GetByDateRangeAsync(startDate, endDate);
                var sessionsList = sessions.ToList();

                using (var writer = new PdfWriter(filePath))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    document.Add(new Paragraph("REPORT SEDUTE DIALITICHE")
                        .SetFontSize(20)
                        .SetBold()
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph($"Periodo: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}")
                        .SetFontSize(12)
                        .SetTextAlignment(TextAlignment.CENTER));

                    document.Add(new Paragraph("\n"));

                    // Statistiche
                    document.Add(new Paragraph("STATISTICHE GENERALI")
                        .SetFontSize(14)
                        .SetBold());

                    document.Add(new Paragraph($"Numero totale sedute: {sessionsList.Count}"));
                    document.Add(new Paragraph($"Sedute completate: {sessionsList.Count(s => s.Stato == "completata")}"));
                    document.Add(new Paragraph($"Sedute annullate: {sessionsList.Count(s => s.Stato == "annullata")}"));

                    if (sessionsList.Any(s => s.Ktv.HasValue))
                    {
                        var avgKtv = sessionsList.Where(s => s.Ktv.HasValue).Average(s => s.Ktv);
                        document.Add(new Paragraph($"Kt/V medio: {avgKtv:F2}"));
                    }
                }

                await _auditService.LogAsync(
                    _authService.GetCurrentUser()?.UserId,
                    "REPORT_GENERATED",
                    "dialysis_sessions",
                    null,
                    $"Report sedute generato: {fileName}");

                return filePath;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante la generazione del report sedute");
                return null;
            }
        }

        public async Task<string> GenerateLabTestsReportAsync(int patientId)
        {
            // Implementazione simile agli altri report
            return await Task.FromResult<string>(null);
        }

        public async Task<string> GenerateMonthlyStatisticsAsync(int year, int month)
        {
            // Implementazione report statistiche mensili
            return await Task.FromResult<string>(null);
        }

        public async Task<string> ExportToExcelAsync(string reportType, Dictionary<string, object> parameters)
        {
            // TODO: Implementare export Excel con ClosedXML
            return await Task.FromResult<string>(null);
        }

        public async Task<byte[]> GeneratePdfReportAsync(string reportType, Dictionary<string, object> parameters)
        {
            // TODO: Implementare generazione PDF generico
            return await Task.FromResult<byte[]>(null);
        }
    }
}
