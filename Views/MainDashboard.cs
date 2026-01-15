using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using DialysisManagement.Services;
using DialysisManagement.Models;
using NLog;

namespace DialysisManagement.Views
{
    public partial class MainDashboard : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IPatientService _patientService;
        private readonly IAlertService _alertService;
        private readonly IServiceProvider _serviceProvider;
        private readonly Logger _logger;
        private User _currentUser;
        private Form _activeForm;

        public MainDashboard(
            IAuthenticationService authService,
            IPatientService patientService,
            IAlertService alertService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeUI();
            LoadUserInfo();
            ShowDashboardHome();
        }

        private void InitializeUI()
        {
            // Eventi menu
            btnDashboard.Click += (s, e) => ShowDashboardHome();
            btnPatients.Click += (s, e) => OpenForm<PatientListForm>();
            btnSessions.Click += (s, e) => OpenForm<DialysisSessionForm>();
            btnLab.Click += (s, e) => OpenForm<LabTestsForm>();
            btnExams.Click += (s, e) => OpenForm<InstrumentalExamsForm>();
            btnReports.Click += (s, e) => OpenForm<ReportsForm>();
            btnSettings.Click += (s, e) => OpenForm<SettingsForm>();
            btnLogout.Click += BtnLogout_Click;

            // Configura icone menu (usando FontAwesome se disponibile)
            SetMenuIcons();
        }

        private void SetMenuIcons()
        {
            btnDashboard.Text = "  📊  Dashboard";
            btnPatients.Text = "  👥  Pazienti";
            btnSessions.Text = "  💉  Sedute Dialisi";
            btnLab.Text = "  🧪  Laboratorio";
            btnExams.Text = "  📋  Esami Strumentali";
            btnReports.Text = "  📈  Report";
            btnSettings.Text = "  ⚙️  Impostazioni";
            btnLogout.Text = "  🚪  Logout";
        }

        private void LoadUserInfo()
        {
            _currentUser = _authService.GetCurrentUser();
            if (_currentUser != null)
            {
                lblUserInfo.Text = $"{_currentUser.FullName} - {_currentUser.Role}";
                _logger.Info($"Dashboard caricata per utente: {_currentUser.Username}");
            }
        }

        private void ShowDashboardHome()
        {
            CloseActiveForm();
            lblPageTitle.Text = "Dashboard";

            // Crea pannello dashboard con statistiche
            var dashPanel = CreateDashboardPanel();
            dashPanel.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(dashPanel);

            LoadDashboardStatistics(dashPanel);
        }

        private Panel CreateDashboardPanel()
        {
            var panel = new Panel
            {
                BackColor = Color.FromArgb(236, 240, 241),
                Padding = new Padding(20)
            };

            // Card Pazienti Attivi
            var cardPatients = CreateStatCard("Pazienti Attivi", "0", Color.FromArgb(52, 152, 219));
            cardPatients.Location = new Point(20, 20);
            cardPatients.Tag = "patients";
            panel.Controls.Add(cardPatients);

            // Card Sedute Oggi
            var cardSessions = CreateStatCard("Sedute Oggi", "0", Color.FromArgb(46, 204, 113));
            cardSessions.Location = new Point(270, 20);
            cardSessions.Tag = "sessions";
            panel.Controls.Add(cardSessions);

            // Card Alert Laboratorio
            var cardAlerts = CreateStatCard("Alert Laboratorio", "0", Color.FromArgb(231, 76, 60));
            cardAlerts.Location = new Point(520, 20);
            cardAlerts.Tag = "alerts";
            panel.Controls.Add(cardAlerts);

            // Card Manutenzioni Scadenza
            var cardMaintenance = CreateStatCard("Manutenzioni", "0", Color.FromArgb(241, 196, 15));
            cardMaintenance.Location = new Point(770, 20);
            cardMaintenance.Tag = "maintenance";
            panel.Controls.Add(cardMaintenance);

            // Area alert
            var lblAlerts = new Label
            {
                Text = "Alert e Notifiche",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(20, 240),
                AutoSize = true
            };
            panel.Controls.Add(lblAlerts);

            var lstAlerts = new ListBox
            {
                Location = new Point(20, 270),
                Size = new Size(1000, 300),
                Font = new Font("Segoe UI", 10),
                BackColor = Color.White
            };
            lstAlerts.Tag = "alertsList";
            panel.Controls.Add(lstAlerts);

            return panel;
        }

        private Panel CreateStatCard(string title, string value, Color color)
        {
            var card = new Panel
            {
                Size = new Size(230, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Barra colorata superiore
            var topBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 5,
                BackColor = color
            };
            card.Controls.Add(topBar);

            // Titolo
            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(15, 20),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);

            // Valore
            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(15, 60),
                AutoSize = true
            };
            lblValue.Name = "lblValue";
            card.Controls.Add(lblValue);

            return card;
        }

        private async void LoadDashboardStatistics(Panel dashPanel)
        {
            try
            {
                // Carica statistiche
                var patients = await _patientService.GetActivePatientsAsync();
                var patientCount = patients.Count();

                var alerts = await _alertService.GetAllAlertsAsync();
                var alertCount = alerts.Values.Sum(list => list.Count);

                // Aggiorna card
                UpdateStatCard(dashPanel, "patients", patientCount.ToString());
                UpdateStatCard(dashPanel, "alerts", alertCount.ToString());

                // Carica lista alert
                var lstAlerts = dashPanel.Controls.Find("alertsList", true).FirstOrDefault() as ListBox;
                if (lstAlerts != null)
                {
                    lstAlerts.Items.Clear();
                    foreach (var category in alerts)
                    {
                        foreach (var alert in category.Value)
                        {
                            lstAlerts.Items.Add($"[{category.Key}] {alert}");
                        }
                    }

                    if (lstAlerts.Items.Count == 0)
                    {
                        lstAlerts.Items.Add("Nessun alert presente");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento statistiche dashboard");
                MessageBox.Show("Errore nel caricamento delle statistiche", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatCard(Panel parent, string tag, string value)
        {
            var card = parent.Controls.Find(tag, false).FirstOrDefault() as Panel;
            if (card != null)
            {
                var lblValue = card.Controls.Find("lblValue", true).FirstOrDefault() as Label;
                if (lblValue != null)
                {
                    lblValue.Text = value;
                }
            }
        }

        private void OpenForm<T>() where T : Form
        {
            try
            {
                CloseActiveForm();

                var form = _serviceProvider.GetService<T>();
                if (form != null)
                {
                    form.TopLevel = false;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Dock = DockStyle.Fill;

                    pnlContent.Controls.Clear();
                    pnlContent.Controls.Add(pnlTopBar);
                    pnlContent.Controls.Add(form);

                    form.Show();
                    _activeForm = form;

                    lblPageTitle.Text = form.Text;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Errore apertura form: {typeof(T).Name}");
                MessageBox.Show($"Errore nell'apertura della schermata", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CloseActiveForm()
        {
            if (_activeForm != null && !_activeForm.IsDisposed)
            {
                _activeForm.Close();
                _activeForm.Dispose();
                _activeForm = null;
            }

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(pnlTopBar);
        }

        private async void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Sei sicuro di voler uscire?",
                "Conferma Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await _authService.LogoutAsync(_currentUser?.UserId ?? 0);

                // Chiudi dashboard
                this.Hide();

                // Mostra login form
                var loginForm = _serviceProvider.GetService<LoginForm>();
                if (loginForm != null)
                {
                    loginForm.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_currentUser != null)
            {
                _authService.LogoutAsync(_currentUser.UserId).Wait();
            }
            base.OnFormClosing(e);
        }
    }
}
