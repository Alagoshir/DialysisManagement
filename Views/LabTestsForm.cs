using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using DialysisManagement.Services;
using DialysisManagement.Models;
using NLog;

namespace DialysisManagement.Views
{
    public partial class LabTestsForm : Form
    {
        private readonly ILabTestService _labTestService;
        private readonly IPatientService _patientService;
        private readonly Logger _logger;
        private int? _selectedPatientId;

        public LabTestsForm(
            ILabTestService labTestService,
            IPatientService patientService)
        {
            InitializeComponent();

            _labTestService = labTestService ?? throw new ArgumentNullException(nameof(labTestService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
            LoadPatientsAsync();
        }

        private void InitializeCustomControls()
        {
            // Eventi
            cboPatient.SelectedIndexChanged += async (s, e) =>
            {
                if (cboPatient.SelectedValue is int patientId)
                {
                    _selectedPatientId = patientId;
                    await LoadTestsAsync();
                }
            };

            btnAddTest.Click += BtnAddTest_Click;
            btnRefresh.Click += async (s, e) => await LoadTestsAsync();
            chkOnlyAlerts.CheckedChanged += async (s, e) => await LoadTestsAsync();

            // Configura DataGridView
            ConfigureDataGridView();
        }

        private async Task LoadPatientsAsync()
        {
            try
            {
                var patients = await _patientService.GetActivePatientsAsync();
                var patientsList = patients
                    .Select(p => new { p.PatientId, FullName = $"{p.Cognome} {p.Nome} - {p.CodiceFiscale}" })
                    .OrderBy(p => p.FullName)
                    .ToList();

                cboPatient.DisplayMember = "FullName";
                cboPatient.ValueMember = "PatientId";
                cboPatient.DataSource = patientsList;

                if (patientsList.Any())
                {
                    cboPatient.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento pazienti");
                MessageBox.Show("Errore nel caricamento dei pazienti", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TestId",
                DataPropertyName = "TestId",
                Visible = false
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataPrelievo",
                HeaderText = "Data Prelievo",
                DataPropertyName = "DataPrelievo",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Hb",
                HeaderText = "Hb (g/dL)",
                DataPropertyName = "Hb",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F1" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Potassio",
                HeaderText = "K+ (mEq/L)",
                DataPropertyName = "Potassio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F1" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Calcio",
                HeaderText = "Ca (mg/dL)",
                DataPropertyName = "Calcio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F1" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Fosforo",
                HeaderText = "P (mg/dL)",
                DataPropertyName = "Fosforo",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F1" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Pth",
                HeaderText = "PTH (pg/mL)",
                DataPropertyName = "Pth",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F0" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ferritina",
                HeaderText = "Ferritina (ng/mL)",
                DataPropertyName = "Ferritina",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F0" }
            });

            dgvTests.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Albumina",
                HeaderText = "Albumina (g/dL)",
                DataPropertyName = "Albumina",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F1" }
            });

            dgvTests.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "AlertGenerato",
                HeaderText = "Alert",
                DataPropertyName = "AlertGenerato",
                Width = 70
            });

            var btnViewColumn = new DataGridViewButtonColumn
            {
                Name = "btnView",
                HeaderText = "Azioni",
                Text = "Dettagli",
                UseColumnTextForButtonValue = true,
                Width = 90
            };
            dgvTests.Columns.Add(btnViewColumn);

            dgvTests.CellClick += DgvTests_CellClick;
            dgvTests.CellFormatting += DgvTests_CellFormatting;
        }

        private async Task LoadTestsAsync()
        {
            try
            {
                if (!_selectedPatientId.HasValue)
                    return;

                var tests = await _labTestService.GetTestsByPatientAsync(_selectedPatientId.Value);
                var testsList = tests.OrderByDescending(t => t.DataPrelievo).ToList();

                // Filtro solo alert
                if (chkOnlyAlerts.Checked)
                {
                    testsList = testsList.Where(t => t.AlertGenerato).ToList();
                }

                dgvTests.DataSource = null;
                dgvTests.DataSource = testsList;

                lblTotalTests.Text = $"Esami totali: {testsList.Count}";
                lblAlertCount.Text = $"Alert: {testsList.Count(t => t.AlertGenerato)}";

                _logger.Info($"Caricati {testsList.Count} esami per paziente ID: {_selectedPatientId}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento esami");
                MessageBox.Show("Errore nel caricamento degli esami", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvTests_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvTests.Rows[e.RowIndex];
            var alertGenerato = row.Cells["AlertGenerato"].Value as bool?;

            if (alertGenerato == true)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
            }

            // Evidenzia valori fuori range
            var columnName = dgvTests.Columns[e.ColumnIndex].Name;
            if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal value))
            {
                bool outOfRange = false;

                switch (columnName)
                {
                    case "Hb":
                        outOfRange = value < 10.0m || value > 12.0m;
                        break;
                    case "Potassio":
                        outOfRange = value < 3.5m || value > 5.5m;
                        break;
                    case "Calcio":
                        outOfRange = value < 8.4m || value > 10.2m;
                        break;
                    case "Fosforo":
                        outOfRange = value > 4.5m;
                        break;
                    case "Pth":
                        outOfRange = value < 150m || value > 300m;
                        break;
                    case "Ferritina":
                        outOfRange = value < 200m;
                        break;
                    case "Albumina":
                        outOfRange = value < 3.5m;
                        break;
                }

                if (outOfRange)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
        }

        private void DgvTests_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var columnName = dgvTests.Columns[e.ColumnIndex].Name;

                if (columnName == "btnView")
                {
                    OpenTestDetail(e.RowIndex);
                }
            }
        }

        private void OpenTestDetail(int rowIndex)
        {
            try
            {
                var testId = Convert.ToInt32(dgvTests.Rows[rowIndex].Cells["TestId"].Value);

                // TODO: Aprire form dettaglio esame
                MessageBox.Show($"Apertura dettaglio esame ID: {testId}", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore apertura dettaglio esame");
                MessageBox.Show("Errore nell'apertura del dettaglio", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddTest_Click(object sender, EventArgs e)
        {
            if (!_selectedPatientId.HasValue)
            {
                MessageBox.Show("Selezionare un paziente", "Attenzione",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // TODO: Aprire form nuovo esame
                MessageBox.Show("Funzionalità in sviluppo", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore creazione nuovo esame");
            }
        }
    }
}
