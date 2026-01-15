using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using DialysisManagement.Services;
using NLog;

namespace DialysisManagement.Views
{
    public partial class ReportsForm : Form
    {
        private readonly IReportService _reportService;
        private readonly IPatientService _patientService;
        private readonly Logger _logger;

        public ReportsForm(
            IReportService reportService,
            IPatientService patientService)
        {
            InitializeComponent();

            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
        }

        private void InitializeCustomControls()
        {
            // Eventi generazione report
            btnGeneratePatientReport.Click += async (s, e) => await GeneratePatientReportAsync();
            btnGenerateSessionsReport.Click += async (s, e) => await GenerateSessionsReportAsync();
            btnGenerateMonthlyStats.Click += async (s, e) => await GenerateMonthlyStatsAsync();
            btnExportSDO.Click += async (s, e) => await ExportSDOAsync();
            btnExportRegistroDialisi.Click += async (s, e) => await ExportRegistroDialisiAsync();

            // Configura date
            dtpStartDate.Value = DateTime.Today.AddMonths(-1);
            dtpEndDate.Value = DateTime.Today;

            // Carica pazienti
            LoadPatientsAsync();
        }

        private async Task LoadPatientsAsync()
        {
            try
            {
                var patients = await _patientService.GetActivePatientsAsync();
                var patientsList = patients
                    .Select(p => new { p.PatientId, FullName = $"{p.Cognome} {p.Nome}" })
                    .OrderBy(p => p.FullName)
                    .ToList();

                cboPatient.DisplayMember = "FullName";
                cboPatient.ValueMember = "PatientId";
                cboPatient.DataSource = patientsList;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento pazienti");
            }
        }

        private async Task GeneratePatientReportAsync()
        {
            try
            {
                if (cboPatient.SelectedValue == null)
                {
                    MessageBox.Show("Selezionare un paziente", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var patientId = (int)cboPatient.SelectedValue;
                var startDate = dtpStartDate.Value;
                var endDate = dtpEndDate.Value;

                btnGeneratePatientReport.Enabled = false;
                btnGeneratePatientReport.Text = "Generazione...";

                var filePath = await _reportService.GeneratePatientReportAsync(patientId, startDate, endDate);

                if (!string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show($"Report generato:\n{filePath}", "Successo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Apri file
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Errore durante la generazione del report", "Errore",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore generazione report paziente");
                MessageBox.Show("Errore durante la generazione del report", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGeneratePatientReport.Enabled = true;
                btnGeneratePatientReport.Text = "📄 Genera Report Paziente";
            }
        }

        private async Task GenerateSessionsReportAsync()
        {
            try
            {
                var startDate = dtpStartDate.Value;
                var endDate = dtpEndDate.Value;

                btnGenerateSessionsReport.Enabled = false;
                btnGenerateSessionsReport.Text = "Generazione...";

                var filePath = await _reportService.GenerateSessionsReportAsync(startDate, endDate);

                if (!string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show($"Report generato:\n{filePath}", "Successo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore generazione report sedute");
                MessageBox.Show("Errore durante la generazione del report", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateSessionsReport.Enabled = true;
                btnGenerateSessionsReport.Text = "📊 Report Sedute";
            }
        }

        private async Task GenerateMonthlyStatsAsync()
        {
            try
            {
                var year = dtpMonthYear.Value.Year;
                var month = dtpMonthYear.Value.Month;

                btnGenerateMonthlyStats.Enabled = false;
                btnGenerateMonthlyStats.Text = "Generazione...";

                var filePath = await _reportService.GenerateMonthlyStatisticsAsync(year, month);

                if (!string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show($"Statistiche generate:\n{filePath}", "Successo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore generazione statistiche mensili");
                MessageBox.Show("Errore durante la generazione delle statistiche", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateMonthlyStats.Enabled = true;
                btnGenerateMonthlyStats.Text = "📈 Statistiche Mensili";
            }
        }

        private async Task ExportSDOAsync()
        {
            try
            {
                MessageBox.Show("Export SDO - Funzionalità in sviluppo\n\n" +
                    "Genererà file per Scheda Dimissione Ospedaliera\n" +
                    "con codici nomenclatore esami interni", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore export SDO");
            }
        }

        private async Task ExportRegistroDialisiAsync()
        {
            try
            {
                MessageBox.Show("Export Registro Dialisi Regionale Campania\n\n" +
                    "Genererà file CSV/XML secondo specifiche regionali\n" +
                    "per invio dati trimestrali", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore export registro dialisi");
            }
        }
    }
}
