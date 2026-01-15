using DialysisManagement.Services;
using DialysisManagement.Utilities;
using NLog;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DialysisManagement.Models;

namespace DialysisManagement.Views
{
    public partial class BackupRestoreForm : Form
    {
        private readonly IBackupService _backupService;
        private readonly Logger _logger;

        public BackupRestoreForm(IBackupService backupService)
        {
            InitializeComponent();

            _backupService = backupService ?? throw new ArgumentNullException(nameof(backupService));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
            LoadBackupHistoryAsync();
        }

        private void InitializeCustomControls()
        {
            // Eventi Backup
            btnCreateBackup.Click += async (s, e) => await CreateBackupAsync();
            btnScheduleBackup.Click += ScheduleBackup_Click;

            // Eventi Restore
            btnRestoreSelected.Click += async (s, e) => await RestoreSelectedBackupAsync();
            btnRestoreFromFile.Click += async (s, e) => await RestoreFromFileAsync();
            btnDeleteBackup.Click += async (s, e) => await DeleteSelectedBackupAsync();

            // Eventi Cloud
            btnUploadCloud.Click += async (s, e) => await UploadToCloudAsync();
            btnDownloadCloud.Click += async (s, e) => await DownloadFromCloudAsync();
            btnConfigureCloud.Click += ConfigureCloud_Click;

            // Eventi Utility
            btnRefresh.Click += async (s, e) => await LoadBackupHistoryAsync();
            btnOpenBackupFolder.Click += OpenBackupFolder_Click;
            btnCleanOldBackups.Click += async (s, e) => await CleanOldBackupsAsync();

            // Configura DataGridView
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView()
        {
            dgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BackupId",
                DataPropertyName = "BackupId",
                Visible = false
            });

            dgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileName",
                HeaderText = "Nome File",
                DataPropertyName = "FileName",
                Width = 300
            });

            dgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                HeaderText = "Data Creazione",
                DataPropertyName = "CreatedDate",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" }
            });

            dgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileSizeMB",
                HeaderText = "Dimensione (MB)",
                DataPropertyName = "FileSizeMB",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "F2" }
            });

            dgvBackups.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsCompressed",
                HeaderText = "Compresso",
                DataPropertyName = "IsCompressed",
                Width = 100
            });

            dgvBackups.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsEncrypted",
                HeaderText = "Crittografato",
                DataPropertyName = "IsEncrypted",
                Width = 120
            });

            dgvBackups.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "UploadedToCloud",
                HeaderText = "Cloud",
                DataPropertyName = "UploadedToCloud",
                Width = 80
            });

            dgvBackups.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Notes",
                HeaderText = "Note",
                DataPropertyName = "Notes",
                Width = 200
            });

            dgvBackups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBackups.MultiSelect = false;
        }

        private async Task LoadBackupHistoryAsync()
        {
            try
            {
                lblStatus.Text = "Caricamento backup in corso...";
                lblStatus.ForeColor = Color.FromArgb(52, 152, 219);
                progressBar.Style = ProgressBarStyle.Marquee;
                progressBar.Visible = true;

                var backups = await _backupService.GetBackupHistoryAsync();
                var backupsList = backups.OrderByDescending(b => b.CreatedDate).ToList();

                dgvBackups.DataSource = null;
                dgvBackups.DataSource = backupsList;

                // Statistiche
                UpdateStatistics(backupsList);

                lblStatus.Text = $"Caricati {backupsList.Count} backup";
                lblStatus.ForeColor = Color.FromArgb(39, 174, 96);

                _logger.Info($"Caricati {backupsList.Count} backup");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento storico backup");
                lblStatus.Text = "Errore caricamento backup";
                lblStatus.ForeColor = Color.FromArgb(192, 57, 43);
                MessageBox.Show("Errore nel caricamento dello storico backup", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        private void UpdateStatistics(System.Collections.Generic.List<BackupInfo> backups)
        {
            var totalCount = backups.Count;
            var totalSizeMB = backups.Sum(b => b.FileSizeMB);
            var cloudCount = backups.Count(b => b.UploadedToCloud);
            var lastBackup = backups.FirstOrDefault()?.CreatedDate;

            lblTotalBackups.Text = $"Totale Backup: {totalCount}";
            lblTotalSize.Text = $"Dimensione Totale: {totalSizeMB:F2} MB";
            lblCloudBackups.Text = $"Su Cloud: {cloudCount}";
            lblLastBackup.Text = lastBackup.HasValue
                ? $"Ultimo Backup: {lastBackup.Value:dd/MM/yyyy HH:mm}"
                : "Ultimo Backup: Nessuno";
        }

        private async Task CreateBackupAsync()
        {
            try
            {
                var result = MessageBox.Show(
                    "Creare un nuovo backup del database?\n\n" +
                    "Opzioni:\n" +
                    $"- Compressione: {(chkCompress.Checked ? "Attiva" : "Disattivata")}\n" +
                    $"- Crittografia: {(chkEncrypt.Checked ? "Attiva" : "Disattivata")}\n" +
                    $"- Upload Cloud: {(chkAutoUpload.Checked ? "Automatico" : "Manuale")}",
                    "Conferma Backup",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                btnCreateBackup.Enabled = false;
                btnCreateBackup.Text = "Backup in corso...";
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;

                lblStatus.Text = "Creazione backup in corso...";
                lblStatus.ForeColor = Color.FromArgb(52, 152, 219);

                var backupOptions = new BackupOptions
                {
                    Compress = chkCompress.Checked,
                    Encrypt = chkEncrypt.Checked,
                    UploadToCloud = chkAutoUpload.Checked,
                    Notes = txtBackupNotes.Text.Trim()
                };

                var (success, filePath, errorMessage) = await _backupService.CreateBackupAsync(backupOptions);

                if (success)
                {
                    lblStatus.Text = $"Backup creato con successo: {Path.GetFileName(filePath)}";
                    lblStatus.ForeColor = Color.FromArgb(39, 174, 96);

                    MessageBox.Show(
                        $"Backup creato con successo!\n\nFile: {Path.GetFileName(filePath)}\n" +
                        $"Percorso: {Path.GetDirectoryName(filePath)}",
                        "Backup Completato",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtBackupNotes.Clear();
                    await LoadBackupHistoryAsync();

                    _logger.Info($"Backup creato: {filePath}");
                }
                else
                {
                    lblStatus.Text = "Errore durante la creazione del backup";
                    lblStatus.ForeColor = Color.FromArgb(192, 57, 43);

                    MessageBox.Show(
                        $"Errore durante la creazione del backup:\n\n{errorMessage}",
                        "Errore",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    _logger.Error($"Errore creazione backup: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore creazione backup");
                lblStatus.Text = "Errore durante la creazione del backup";
                lblStatus.ForeColor = Color.FromArgb(192, 57, 43);
                MessageBox.Show("Errore durante la creazione del backup", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCreateBackup.Enabled = true;
                btnCreateBackup.Text = "💾 Crea Backup Ora";
                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        private async Task RestoreSelectedBackupAsync()
        {
            try
            {
                if (dgvBackups.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selezionare un backup da ripristinare", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedBackup = dgvBackups.SelectedRows[0].DataBoundItem as BackupInfo;
                if (selectedBackup == null)
                    return;

                var result = MessageBox.Show(
                    $"ATTENZIONE: Ripristinare il backup selezionato?\n\n" +
                    $"File: {selectedBackup.FileName}\n" +
                    $"Data: {selectedBackup.CreatedDate:dd/MM/yyyy HH:mm:ss}\n\n" +
                    "Tutti i dati attuali verranno sovrascritti!\n" +
                    "Si consiglia di creare un backup prima di procedere.",
                    "Conferma Ripristino",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                    return;

                btnRestoreSelected.Enabled = false;
                btnRestoreSelected.Text = "Ripristino in corso...";
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;

                lblStatus.Text = "Ripristino database in corso...";
                lblStatus.ForeColor = Color.FromArgb(230, 126, 34);

                var (success, errorMessage) = await _backupService.RestoreBackupAsync(selectedBackup.BackupId);

                if (success)
                {
                    lblStatus.Text = "Database ripristinato con successo";
                    lblStatus.ForeColor = Color.FromArgb(39, 174, 96);

                    MessageBox.Show(
                        "Database ripristinato con successo!\n\n" +
                        "L'applicazione verrà riavviata per applicare le modifiche.",
                        "Ripristino Completato",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _logger.Info($"Database ripristinato da backup: {selectedBackup.FileName}");

                    // TODO: Riavvio applicazione
                    Application.Restart();
                }
                else
                {
                    lblStatus.Text = "Errore durante il ripristino";
                    lblStatus.ForeColor = Color.FromArgb(192, 57, 43);

                    MessageBox.Show(
                        $"Errore durante il ripristino:\n\n{errorMessage}",
                        "Errore",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    _logger.Error($"Errore ripristino backup: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore ripristino backup");
                lblStatus.Text = "Errore durante il ripristino";
                lblStatus.ForeColor = Color.FromArgb(192, 57, 43);
                MessageBox.Show("Errore durante il ripristino del backup", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRestoreSelected.Enabled = true;
                btnRestoreSelected.Text = "↩️ Ripristina Selezionato";
                progressBar.Visible = false;
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }

        private async Task RestoreFromFileAsync()
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Backup Files|*.sql;*.zip;*.7z|SQL Files|*.sql|Compressed Files|*.zip;*.7z|All Files|*.*";
                    openFileDialog.Title = "Seleziona file backup da ripristinare";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var result = MessageBox.Show(
                            $"Ripristinare il backup dal file:\n\n{openFileDialog.FileName}\n\n" +
                            "ATTENZIONE: Tutti i dati attuali verranno sovrascritti!",
                            "Conferma Ripristino",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result != DialogResult.Yes)
                            return;

                        btnRestoreFromFile.Enabled = false;
                        progressBar.Visible = true;
                        progressBar.Style = ProgressBarStyle.Marquee;

                        lblStatus.Text = "Ripristino da file in corso...";
                        lblStatus.ForeColor = Color.FromArgb(230, 126, 34);

                        var (success, errorMessage) = await _backupService.RestoreFromFileAsync(openFileDialog.FileName);

                        if (success)
                        {
                            MessageBox.Show("Ripristino completato con successo!", "Successo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Application.Restart();
                        }
                        else
                        {
                            MessageBox.Show($"Errore durante il ripristino:\n\n{errorMessage}", "Errore",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore ripristino da file");
                MessageBox.Show("Errore durante il ripristino dal file", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRestoreFromFile.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private async Task DeleteSelectedBackupAsync()
        {
            try
            {
                if (dgvBackups.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selezionare un backup da eliminare", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedBackup = dgvBackups.SelectedRows[0].DataBoundItem as BackupInfo;
                if (selectedBackup == null)
                    return;

                var result = MessageBox.Show(
                    $"Eliminare il backup selezionato?\n\n{selectedBackup.FileName}\n\n" +
                    "Questa operazione non può essere annullata.",
                    "Conferma Eliminazione",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                var success = await _backupService.DeleteBackupAsync(selectedBackup.BackupId);

                if (success)
                {
                    MessageBox.Show("Backup eliminato con successo", "Successo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadBackupHistoryAsync();
                }
                else
                {
                    MessageBox.Show("Errore durante l'eliminazione del backup", "Errore",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore eliminazione backup");
                MessageBox.Show("Errore durante l'eliminazione del backup", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UploadToCloudAsync()
        {
            try
            {
                if (dgvBackups.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Selezionare un backup da caricare", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedBackup = dgvBackups.SelectedRows[0].DataBoundItem as BackupInfo;
                if (selectedBackup == null)
                    return;

                btnUploadCloud.Enabled = false;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;

                lblStatus.Text = "Upload cloud in corso...";
                lblStatus.ForeColor = Color.FromArgb(52, 152, 219);

                var success = await _backupService.UploadToCloudAsync(selectedBackup.BackupId);

                if (success)
                {
                    MessageBox.Show("Upload completato con successo", "Successo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadBackupHistoryAsync();
                }
                else
                {
                    MessageBox.Show("Errore durante l'upload", "Errore",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore upload cloud");
                MessageBox.Show("Errore durante l'upload su cloud", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnUploadCloud.Enabled = true;
                progressBar.Visible = false;
            }
        }

        private async Task DownloadFromCloudAsync()
        {
            try
            {
                MessageBox.Show("Funzionalità in sviluppo\n\nPermette di scaricare backup da cloud storage",
                    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore download cloud");
            }
        }

        private void ConfigureCloud_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Configurazione Cloud Storage\n\n" +
                "Supportati:\n" +
                "- Google Drive\n" +
                "- OneDrive\n" +
                "- Dropbox\n" +
                "- FTP/SFTP personalizzato",
                "Configurazione Cloud",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ScheduleBackup_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Programmazione Backup Automatici\n\n" +
                "Opzioni:\n" +
                "- Giornaliero (orario configurabile)\n" +
                "- Settimanale\n" +
                "- Mensile\n" +
                "- All'avvio applicazione",
                "Backup Programmati",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void OpenBackupFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var backupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
                if (Directory.Exists(backupFolder))
                {
                    System.Diagnostics.Process.Start("explorer.exe", backupFolder);
                }
                else
                {
                    MessageBox.Show("Cartella backup non trovata", "Attenzione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore apertura cartella backup");
            }
        }

        private async Task CleanOldBackupsAsync()
        {
            try
            {
                var result = MessageBox.Show(
                    "Eliminare i backup più vecchi di 30 giorni?\n\n" +
                    "I backup su cloud non verranno eliminati.",
                    "Pulizia Backup Vecchi",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                var deletedCount = await _backupService.CleanOldBackupsAsync(30);

                MessageBox.Show($"Eliminati {deletedCount} backup obsoleti", "Pulizia Completata",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadBackupHistoryAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore pulizia backup vecchi");
                MessageBox.Show("Errore durante la pulizia dei backup", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
