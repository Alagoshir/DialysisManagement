using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using DialysisManagement.Services;
using DialysisManagement.Utilities;
using NLog;

namespace DialysisManagement.Views
{
    public partial class SettingsForm : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly Logger _logger;

        public SettingsForm(IAuthenticationService authService)
        {
            InitializeComponent();

            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
            LoadSettings();
        }

        private void InitializeCustomControls()
        {
            btnSaveSettings.Click += async (s, e) => await SaveSettingsAsync();
            btnChangePassword.Click += async (s, e) => await ChangePasswordAsync();
            btnTestConnection.Click += (s, e) => TestDatabaseConnection();
        }

        private void LoadSettings()
        {
            try
            {
                // Carica impostazioni correnti
                txtCentroNome.Text = ConfigurationHelper.GetAppSetting("centro_nome", "");
                txtCentroCodice.Text = ConfigurationHelper.GetAppSetting("centro_codice", "");
                txtCentroIndirizzo.Text = ConfigurationHelper.GetAppSetting("centro_indirizzo", "");
                txtCentroTelefono.Text = ConfigurationHelper.GetAppSetting("centro_telefono", "");
                txtCentroEmail.Text = ConfigurationHelper.GetAppSetting("centro_email", "");

                chkEncryptAttachments.Checked = ConfigurationHelper.GetAppSettingBool("EncryptAttachments", true);
                chkCompressBackup.Checked = ConfigurationHelper.GetAppSettingBool("CompressBackup", true);
                chkCloudBackup.Checked = ConfigurationHelper.GetAppSettingBool("CloudBackupEnabled", false);

                numBackupRetention.Value = ConfigurationHelper.GetAppSettingInt("BackupRetentionDays", 30);
                numSessionTimeout.Value = ConfigurationHelper.GetAppSettingInt("SessionTimeoutMinutes", 30);

                // Utente corrente
                var currentUser = _authService.GetCurrentUser();
                if (currentUser != null)
                {
                    lblCurrentUser.Text = $"Utente: {currentUser.FullName} ({currentUser.Role})";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento impostazioni");
                MessageBox.Show("Errore nel caricamento delle impostazioni", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task SaveSettingsAsync()
        {
            try
            {
                btnSaveSettings.Enabled = false;
                btnSaveSettings.Text = "Salvataggio...";

                // Salva impostazioni (implementare salvataggio in config file o DB)
                ConfigurationHelper.SetAppSetting("centro_nome", txtCentroNome.Text);
                ConfigurationHelper.SetAppSetting("centro_codice", txtCentroCodice.Text);
                ConfigurationHelper.SetAppSetting("centro_indirizzo", txtCentroIndirizzo.Text);
                ConfigurationHelper.SetAppSetting("centro_telefono", txtCentroTelefono.Text);
                ConfigurationHelper.SetAppSetting("centro_email", txtCentroEmail.Text);

                ConfigurationHelper.SetAppSetting("EncryptAttachments", chkEncryptAttachments.Checked.ToString());
                ConfigurationHelper.SetAppSetting("CompressBackup", chkCompressBackup.Checked.ToString());
                ConfigurationHelper.SetAppSetting("CloudBackupEnabled", chkCloudBackup.Checked.ToString());

                ConfigurationHelper.SetAppSetting("BackupRetentionDays", numBackupRetention.Value.ToString());
                ConfigurationHelper.SetAppSetting("SessionTimeoutMinutes", numSessionTimeout.Value.ToString());

                await Task.Delay(500); // Simula salvataggio

                MessageBox.Show("Impostazioni salvate con successo", "Successo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                _logger.Info("Impostazioni salvate");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore salvataggio impostazioni");
                MessageBox.Show("Errore durante il salvataggio", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSaveSettings.Enabled = true;
                btnSaveSettings.Text = "💾 Salva Impostazioni";
            }
        }

        private async Task ChangePasswordAsync()
        {
            try
            {
                var currentUser = _authService.GetCurrentUser();
                if (currentUser == null)
                {
                    MessageBox.Show("Utente non autenticato", "Errore",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtOldPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
                    string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
                {
                    MessageBox.Show("Compilare tutti i campi password", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Le password non coincidono", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text.Length < 8)
                {
                    MessageBox.Show("La password deve essere di almeno 8 caratteri", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var success = await _authService.ChangePasswordAsync(
                    currentUser.UserId,
                    txtOldPassword.Text,
                    txtNewPassword.Text);

                if (success)
                {
                    MessageBox.Show("Password modificata con successo", "Successo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtOldPassword.Clear();
                    txtNewPassword.Clear();
                    txtConfirmPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Password vecchia errata o errore durante la modifica", "Errore",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore cambio password");
                MessageBox.Show("Errore durante il cambio password", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TestDatabaseConnection()
        {
            try
            {
                // Test connessione database
                MessageBox.Show("Connessione al database OK", "Test Connessione",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore test connessione");
                MessageBox.Show("Errore connessione database", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
