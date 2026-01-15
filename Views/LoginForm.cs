using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using DialysisManagement.Services;
using NLog;

namespace DialysisManagement.Views
{
    public partial class LoginForm : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IAuditService _auditService;
        private readonly IServiceProvider _serviceProvider;
        private readonly Logger _logger;

        public LoginForm(
            IAuthenticationService authService,
            IAuditService auditService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _auditService = auditService ?? throw new ArgumentNullException(nameof(auditService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeUI();
        }

        private void InitializeUI()
        {
            // Rounded corners per i panel
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            pnlLogin.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, pnlLogin.Width, pnlLogin.Height, 10, 10));

            // Eventi
            btnLogin.Click += BtnLogin_Click;
            btnClose.Click += (s, e) => Application.Exit();
            txtPassword.KeyPress += TxtPassword_KeyPress;

            // Focus iniziale
            txtUsername.Focus();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                BtnLogin_Click(sender, EventArgs.Empty);
            }
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            await PerformLoginAsync();
        }

        private async Task PerformLoginAsync()
        {
            try
            {
                // Validazione input
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Inserire username", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Inserire password", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                // Disabilita controlli durante il login
                SetControlsEnabled(false);
                btnLogin.Text = "Autenticazione...";

                // Effettua login
                var (success, user, errorMessage) = await _authService.LoginAsync(
                    txtUsername.Text.Trim(),
                    txtPassword.Text);

                if (success)
                {
                    _logger.Info($"Login effettuato con successo: {user.Username}");

                    // Nascondi form login
                    this.Hide();

                    // Apri dashboard principale
                    var mainDashboard = _serviceProvider.GetService(typeof(MainDashboard)) as MainDashboard;
                    if (mainDashboard != null)
                    {
                        mainDashboard.FormClosed += (s, e) => Application.Exit();
                        mainDashboard.Show();
                    }
                }
                else
                {
                    _logger.Warn($"Tentativo login fallito per: {txtUsername.Text}");

                    MessageBox.Show(errorMessage ?? "Login fallito", "Errore Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtPassword.Clear();
                    txtPassword.Focus();

                    SetControlsEnabled(true);
                    btnLogin.Text = "ACCEDI";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore durante il login");

                MessageBox.Show("Errore durante il login. Riprovare.", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                SetControlsEnabled(true);
                btnLogin.Text = "ACCEDI";
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            btnLogin.Enabled = enabled;
        }

        // Mouse drag per spostare form senza bordo
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;

        private void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = this.Location;
        }

        private void PnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(diff));
            }
        }

        private void PnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
    }
}
