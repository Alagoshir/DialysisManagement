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
    public partial class DialysisSessionForm : Form
    {
        private readonly IDialysisSessionService _sessionService;
        private readonly IPatientService _patientService;
        private readonly Logger _logger;
        private DateTime _selectedDate = DateTime.Today;

        public DialysisSessionForm(
            IDialysisSessionService sessionService,
            IPatientService patientService)
        {
            InitializeComponent();

            _sessionService = sessionService ?? throw new ArgumentNullException(nameof(sessionService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
            LoadSessionsAsync();
        }

        private void InitializeCustomControls()
        {
            // Eventi
            dtpSessionDate.ValueChanged += async (s, e) =>
            {
                _selectedDate = dtpSessionDate.Value.Date;
                await LoadSessionsAsync();
            };

            btnToday.Click += (s, e) =>
            {
                dtpSessionDate.Value = DateTime.Today;
            };

            btnPrevDay.Click += (s, e) =>
            {
                dtpSessionDate.Value = dtpSessionDate.Value.AddDays(-1);
            };

            btnNextDay.Click += (s, e) =>
            {
                dtpSessionDate.Value = dtpSessionDate.Value.AddDays(1);
            };

            btnNewSession.Click += BtnNewSession_Click;
            btnRefresh.Click += async (s, e) => await LoadSessionsAsync();

            // Filtro turno
            cboTurno.Items.AddRange(new object[] { "Tutti", "Mattina", "Pomeriggio", "Sera" });
            cboTurno.SelectedIndex = 0;
            cboTurno.SelectedIndexChanged += async (s, e) => await LoadSessionsAsync();

            // Configura DataGridView
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SessionId",
                DataPropertyName = "SessionId",
                Visible = false
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PatientName",
                HeaderText = "Paziente",
                Width = 180
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Turno",
                HeaderText = "Turno",
                DataPropertyName = "Turno",
                Width = 100
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "StationId",
                HeaderText = "Postazione",
                DataPropertyName = "StationId",
                Width = 90
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OraInizio",
                HeaderText = "Ora Inizio",
                DataPropertyName = "OraInizio",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm" }
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OraFine",
                HeaderText = "Ora Fine",
                DataPropertyName = "OraFine",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm" }
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DurataEffettiva",
                HeaderText = "Durata (min)",
                DataPropertyName = "DurataEffettiva",
                Width = 100
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PesoPre",
                HeaderText = "Peso Pre",
                DataPropertyName = "PesoPre",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PesoPost",
                HeaderText = "Peso Post",
                DataPropertyName = "PesoPost",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UfEffettuata",
                HeaderText = "UF (ml)",
                DataPropertyName = "UfEffettuata",
                Width = 90
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ktv",
                HeaderText = "Kt/V",
                DataPropertyName = "Ktv",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });

            dgvSessions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Stato",
                HeaderText = "Stato",
                DataPropertyName = "Stato",
                Width = 110
            });

            var btnViewColumn = new DataGridViewButtonColumn
            {
                Name = "btnView",
                HeaderText = "Azioni",
                Text = "Dettagli",
                UseColumnTextForButtonValue = true,
                Width = 90
            };
            dgvSessions.Columns.Add(btnViewColumn);

            dgvSessions.CellClick += DgvSessions_CellClick;
            dgvSessions.CellFormatting += DgvSessions_CellFormatting;
        }

        private async Task LoadSessionsAsync()
        {
            try
            {
                lblLoadingStatus.Text = "Caricamento sedute...";
                lblLoadingStatus.Visible = true;

                var sessions = await _sessionService.GetSessionsByDateAsync(_selectedDate);
                var sessionsList = sessions.ToList();

                // Filtro per turno
                var turnoFilter = cboTurno.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(turnoFilter) && turnoFilter != "Tutti")
                {
                    sessionsList = sessionsList
                        .Where(s => s.Turno?.Equals(turnoFilter, StringComparison.OrdinalIgnoreCase) ?? false)
                        .ToList();
                }

                // Carica nomi pazienti
                foreach (var session in sessionsList)
                {
                    var patient = await _patientService.GetPatientByIdAsync(session.PatientId);
                    if (patient != null)
                    {
                        session.PatientName = $"{patient.Cognome} {patient.Nome}";
                    }
                }

                dgvSessions.DataSource = null;
                dgvSessions.DataSource = sessionsList;

                // Statistiche
                UpdateStatistics(sessionsList);

                lblLoadingStatus.Visible = false;
                _logger.Info($"Caricate {sessionsList.Count} sedute per {_selectedDate:dd/MM/yyyy}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento sedute");
                MessageBox.Show("Errore nel caricamento delle sedute", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblLoadingStatus.Visible = false;
            }
        }

        private void UpdateStatistics(System.Collections.Generic.List<DialysisSession> sessions)
        {
            var totali = sessions.Count;
            var completate = sessions.Count(s => s.Stato == "completata");
            var inCorso = sessions.Count(s => s.Stato == "in_corso");
            var programmate = sessions.Count(s => s.Stato == "programmata");

            var ktvMedio = sessions.Where(s => s.Ktv.HasValue).Average(s => s.Ktv) ?? 0;

            lblStatTotali.Text = $"Totali: {totali}";
            lblStatCompletate.Text = $"Completate: {completate}";
            lblStatInCorso.Text = $"In Corso: {inCorso}";
            lblStatProgrammate.Text = $"Programmate: {programmate}";
            lblStatKtvMedio.Text = $"Kt/V Medio: {ktvMedio:F2}";
        }

        private void DgvSessions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSessions.Columns[e.ColumnIndex].Name == "Stato" && e.Value != null)
            {
                var stato = e.Value.ToString();
                switch (stato)
                {
                    case "completata":
                        e.CellStyle.BackColor = Color.FromArgb(230, 255, 230);
                        e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                        break;
                    case "in_corso":
                        e.CellStyle.BackColor = Color.FromArgb(230, 245, 255);
                        e.CellStyle.ForeColor = Color.FromArgb(52, 152, 219);
                        break;
                    case "programmata":
                        e.CellStyle.BackColor = Color.FromArgb(255, 250, 230);
                        e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15);
                        break;
                    case "annullata":
                        e.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
                        e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                        break;
                }
            }
        }

        private void DgvSessions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var columnName = dgvSessions.Columns[e.ColumnIndex].Name;

                if (columnName == "btnView")
                {
                    OpenSessionDetail(e.RowIndex);
                }
            }
        }

        private void OpenSessionDetail(int rowIndex)
        {
            try
            {
                var sessionId = Convert.ToInt32(dgvSessions.Rows[rowIndex].Cells["SessionId"].Value);

                // TODO: Aprire form dettaglio seduta
                MessageBox.Show($"Apertura dettaglio seduta ID: {sessionId}", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadSessionsAsync(); // Refresh
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore apertura dettaglio seduta");
                MessageBox.Show("Errore nell'apertura del dettaglio", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNewSession_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: Aprire form nuova seduta
                MessageBox.Show("Funzionalità in sviluppo", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore creazione nuova seduta");
            }
        }
    }

    // Estensione per nome paziente
    public partial class DialysisSession
    {
        public string PatientName { get; set; }
    }
}
