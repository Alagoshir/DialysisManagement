namespace DialysisManagement.Views
{
    partial class BackupRestoreForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnCleanOldBackups = new System.Windows.Forms.Button();
            this.btnOpenBackupFolder = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpBackup = new System.Windows.Forms.GroupBox();
            this.txtBackupNotes = new System.Windows.Forms.TextBox();
            this.lblBackupNotes = new System.Windows.Forms.Label();
            this.chkAutoUpload = new System.Windows.Forms.CheckBox();
            this.chkEncrypt = new System.Windows.Forms.CheckBox();
            this.chkCompress = new System.Windows.Forms.CheckBox();
            this.btnScheduleBackup = new System.Windows.Forms.Button();
            this.btnCreateBackup = new System.Windows.Forms.Button();
            this.grpRestore = new System.Windows.Forms.GroupBox();
            this.btnDeleteBackup = new System.Windows.Forms.Button();
            this.btnRestoreFromFile = new System.Windows.Forms.Button();
            this.btnRestoreSelected = new System.Windows.Forms.Button();
            this.grpCloud = new System.Windows.Forms.GroupBox();
            this.btnConfigureCloud = new System.Windows.Forms.Button();
            this.btnDownloadCloud = new System.Windows.Forms.Button();
            this.btnUploadCloud = new System.Windows.Forms.Button();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvBackups = new System.Windows.Forms.DataGridView();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.lblLastBackup = new System.Windows.Forms.Label();
            this.lblCloudBackups = new System.Windows.Forms.Label();
            this.lblTotalSize = new System.Windows.Forms.Label();
            this.lblTotalBackups = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpBackup.SuspendLayout();
            this.grpRestore.SuspendLayout();
            this.grpCloud.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).BeginInit();
            this.pnlStatistics.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.btnRefresh);
            this.pnlTop.Controls.Add(this.btnCleanOldBackups);
            this.pnlTop.Controls.Add(this.btnOpenBackupFolder);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlTop.Size = new System.Drawing.Size(1400, 70);
            this.pnlTop.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1330, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnCleanOldBackups
            // 
            this.btnCleanOldBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCleanOldBackups.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnCleanOldBackups.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCleanOldBackups.FlatAppearance.BorderSize = 0;
            this.btnCleanOldBackups.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCleanOldBackups.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCleanOldBackups.ForeColor = System.Drawing.Color.White;
            this.btnCleanOldBackups.Location = new System.Drawing.Point(1110, 15);
            this.btnCleanOldBackups.Name = "btnCleanOldBackups";
            this.btnCleanOldBackups.Size = new System.Drawing.Size(210, 40);
            this.btnCleanOldBackups.TabIndex = 2;
            this.btnCleanOldBackups.Text = "🗑️ Pulisci Backup Vecchi";
            this.btnCleanOldBackups.UseVisualStyleBackColor = false;
            // 
            // btnOpenBackupFolder
            // 
            this.btnOpenBackupFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenBackupFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnOpenBackupFolder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenBackupFolder.FlatAppearance.BorderSize = 0;
            this.btnOpenBackupFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenBackupFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOpenBackupFolder.ForeColor = System.Drawing.Color.White;
            this.btnOpenBackupFolder.Location = new System.Drawing.Point(920, 15);
            this.btnOpenBackupFolder.Name = "btnOpenBackupFolder";
            this.btnOpenBackupFolder.Size = new System.Drawing.Size(180, 40);
            this.btnOpenBackupFolder.TabIndex = 1;
            this.btnOpenBackupFolder.Text = "📁 Apri Cartella";
            this.btnOpenBackupFolder.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(319, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Backup e Ripristino DB";
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlLeft.Controls.Add(this.grpBackup);
            this.pnlLeft.Controls.Add(this.grpRestore);
            this.pnlLeft.Controls.Add(this.grpCloud);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 70);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(15);
            this.pnlLeft.Size = new System.Drawing.Size(380, 560);
            this.pnlLeft.TabIndex = 1;
            // 
            // grpBackup
            // 
            this.grpBackup.BackColor = System.Drawing.Color.White;
            this.grpBackup.Controls.Add(this.txtBackupNotes);
            this.grpBackup.Controls.Add(this.lblBackupNotes);
            this.grpBackup.Controls.Add(this.chkAutoUpload);
            this.grpBackup.Controls.Add(this.chkEncrypt);
            this.grpBackup.Controls.Add(this.chkCompress);
            this.grpBackup.Controls.Add(this.btnScheduleBackup);
            this.grpBackup.Controls.Add(this.btnCreateBackup);
            this.grpBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpBackup.Location = new System.Drawing.Point(15, 15);
            this.grpBackup.Name = "grpBackup";
            this.grpBackup.Padding = new System.Windows.Forms.Padding(15);
            this.grpBackup.Size = new System.Drawing.Size(350, 340);
            this.grpBackup.TabIndex = 0;
            this.grpBackup.TabStop = false;
            this.grpBackup.Text = "Crea Backup";
            // 
            // txtBackupNotes
            // 
            this.txtBackupNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBackupNotes.Location = new System.Drawing.Point(15, 175);
            this.txtBackupNotes.Multiline = true;
            this.txtBackupNotes.Name = "txtBackupNotes";
            this.txtBackupNotes.PlaceholderText = "Note opzionali...";
            this.txtBackupNotes.Size = new System.Drawing.Size(320, 60);
            this.txtBackupNotes.TabIndex = 6;
            // 
            // lblBackupNotes
            // 
            this.lblBackupNotes.AutoSize = true;
            this.lblBackupNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBackupNotes.Location = new System.Drawing.Point(15, 150);
            this.lblBackupNotes.Name = "lblBackupNotes";
            this.lblBackupNotes.Size = new System.Drawing.Size(42, 20);
            this.lblBackupNotes.TabIndex = 5;
            this.lblBackupNotes.Text = "Note";
            // 
            // chkAutoUpload
            // 
            this.chkAutoUpload.AutoSize = true;
            this.chkAutoUpload.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkAutoUpload.Location = new System.Drawing.Point(15, 115);
            this.chkAutoUpload.Name = "chkAutoUpload";
            this.chkAutoUpload.Size = new System.Drawing.Size(239, 27);
            this.chkAutoUpload.TabIndex = 4;
            this.chkAutoUpload.Text = "Upload Automatico su Cloud";
            this.chkAutoUpload.UseVisualStyleBackColor = true;
            // 
            // chkEncrypt
            // 
            this.chkEncrypt.AutoSize = true;
            this.chkEncrypt.Checked = true;
            this.chkEncrypt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEncrypt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEncrypt.Location = new System.Drawing.Point(15, 80);
            this.chkEncrypt.Name = "chkEncrypt";
            this.chkEncrypt.Size = new System.Drawing.Size(188, 27);
            this.chkEncrypt.TabIndex = 3;
            this.chkEncrypt.Text = "Cripta (AES-256)";
            this.chkEncrypt.UseVisualStyleBackColor = true;
            // 
            // chkCompress
            // 
            this.chkCompress.AutoSize = true;
            this.chkCompress.Checked = true;
            this.chkCompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkCompress.Location = new System.Drawing.Point(15, 45);
            this.chkCompress.Name = "chkCompress";
            this.chkCompress.Size = new System.Drawing.Size(164, 27);
            this.chkCompress.TabIndex = 2;
            this.chkCompress.Text = "Comprimi (ZIP)";
            this.chkCompress.UseVisualStyleBackColor = true;
            // 
            // btnScheduleBackup
            // 
            this.btnScheduleBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnScheduleBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnScheduleBackup.FlatAppearance.BorderSize = 0;
            this.btnScheduleBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnScheduleBackup.ForeColor = System.Drawing.Color.White;
            this.btnScheduleBackup.Location = new System.Drawing.Point(15, 285);
            this.btnScheduleBackup.Name = "btnScheduleBackup";
            this.btnScheduleBackup.Size = new System.Drawing.Size(320, 40);
            this.btnScheduleBackup.TabIndex = 1;
            this.btnScheduleBackup.Text = "📅 Programma Backup";
            this.btnScheduleBackup.UseVisualStyleBackColor = false;
            // 
            // btnCreateBackup
            // 
            this.btnCreateBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnCreateBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateBackup.FlatAppearance.BorderSize = 0;
            this.btnCreateBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateBackup.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCreateBackup.ForeColor = System.Drawing.Color.White;
            this.btnCreateBackup.Location = new System.Drawing.Point(15, 240);
            this.btnCreateBackup.Name = "btnCreateBackup";
            this.btnCreateBackup.Size = new System.Drawing.Size(320, 45);
            this.btnCreateBackup.TabIndex = 0;
            this.btnCreateBackup.Text = "💾 Crea Backup Ora";
            this.btnCreateBackup.UseVisualStyleBackColor = false;
            // 
            // grpRestore
            // 
            this.grpRestore.BackColor = System.Drawing.Color.White;
            this.grpRestore.Controls.Add(this.btnDeleteBackup);
            this.grpRestore.Controls.Add(this.btnRestoreFromFile);
            this.grpRestore.Controls.Add(this.btnRestoreSelected);
            this.grpRestore.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpRestore.Location = new System.Drawing.Point(15, 365);
            this.grpRestore.Name = "grpRestore";
            this.grpRestore.Padding = new System.Windows.Forms.Padding(15);
            this.grpRestore.Size = new System.Drawing.Size(350, 190);
            this.grpRestore.TabIndex = 1;
            this.grpRestore.TabStop = false;
            this.grpRestore.Text = "Ripristina / Gestisci";
            // 
            // btnDeleteBackup
            // 
            this.btnDeleteBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnDeleteBackup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteBackup.FlatAppearance.BorderSize = 0;
            this.btnDeleteBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteBackup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeleteBackup.ForeColor = System.Drawing.Color.White;
            this.btnDeleteBackup.Location = new System.Drawing.Point(15, 135);
            this.btnDeleteBackup.Name = "btnDeleteBackup";
            this.btnDeleteBackup.Size = new System.Drawing.Size(320, 40);
            this.btnDeleteBackup.TabIndex = 2;
            this.btnDeleteBackup.Text = "🗑️ Elimina Selezionato";
            this.btnDeleteBackup.UseVisualStyleBackColor = false;
            // 
            // btnRestoreFromFile
            // 
            this.btnRestoreFromFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRestoreFromFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestoreFromFile.FlatAppearance.BorderSize = 0;
            this.btnRestoreFromFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreFromFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestoreFromFile.ForeColor = System.Drawing.Color.White;
            this.btnRestoreFromFile.Location = new System.Drawing.Point(15, 85);
            this.btnRestoreFromFile.Name = "btnRestoreFromFile";
            this.btnRestoreFromFile.Size = new System.Drawing.Size(320, 40);
            this.btnRestoreFromFile.TabIndex = 1;
            this.btnRestoreFromFile.Text = "📂 Ripristina da File...";
            this.btnRestoreFromFile.UseVisualStyleBackColor = false;
            // 
            // btnRestoreSelected
            // 
            this.btnRestoreSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnRestoreSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestoreSelected.FlatAppearance.BorderSize = 0;
            this.btnRestoreSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreSelected.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRestoreSelected.ForeColor = System.Drawing.Color.White;
            this.btnRestoreSelected.Location = new System.Drawing.Point(15, 35);
            this.btnRestoreSelected.Name = "btnRestoreSelected";
            this.btnRestoreSelected.Size = new System.Drawing.Size(320, 40);
            this.btnRestoreSelected.TabIndex = 0;
            this.btnRestoreSelected.Text = "↩️ Ripristina Selezionato";
            this.btnRestoreSelected.UseVisualStyleBackColor = false;
            // 
            // grpCloud
            // 
            this.grpCloud.BackColor = System.Drawing.Color.White;
            this.grpCloud.Controls.Add(this.btnConfigureCloud);
            this.grpCloud.Controls.Add(this.btnDownloadCloud);
            this.grpCloud.Controls.Add(this.btnUploadCloud);
            this.grpCloud.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpCloud.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpCloud.Location = new System.Drawing.Point(15, 410);
            this.grpCloud.Name = "grpCloud";
            this.grpCloud.Padding = new System.Windows.Forms.Padding(15);
            this.grpCloud.Size = new System.Drawing.Size(350, 135);
            this.grpCloud.TabIndex = 2;
            this.grpCloud.TabStop = false;
            this.grpCloud.Text = "Cloud Storage";
            this.grpCloud.Visible = false;
            // 
            // btnConfigureCloud
            // 
            this.btnConfigureCloud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnConfigureCloud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigureCloud.FlatAppearance.BorderSize = 0;
            this.btnConfigureCloud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigureCloud.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConfigureCloud.ForeColor = System.Drawing.Color.White;
            this.btnConfigureCloud.Location = new System.Drawing.Point(240, 35);
            this.btnConfigureCloud.Name = "btnConfigureCloud";
            this.btnConfigureCloud.Size = new System.Drawing.Size(95, 80);
            this.btnConfigureCloud.TabIndex = 2;
            this.btnConfigureCloud.Text = "⚙️\r\nConfig";
            this.btnConfigureCloud.UseVisualStyleBackColor = false;
            // 
            // btnDownloadCloud
            // 
            this.btnDownloadCloud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnDownloadCloud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDownloadCloud.FlatAppearance.BorderSize = 0;
            this.btnDownloadCloud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadCloud.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDownloadCloud.ForeColor = System.Drawing.Color.White;
            this.btnDownloadCloud.Location = new System.Drawing.Point(125, 35);
            this.btnDownloadCloud.Name = "btnDownloadCloud";
            this.btnDownloadCloud.Size = new System.Drawing.Size(105, 80);
            this.btnDownloadCloud.TabIndex = 1;
            this.btnDownloadCloud.Text = "⬇️\r\nDownload";
            this.btnDownloadCloud.UseVisualStyleBackColor = false;
            // 
            // btnUploadCloud
            // 
            this.btnUploadCloud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnUploadCloud.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadCloud.FlatAppearance.BorderSize = 0;
            this.btnUploadCloud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadCloud.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnUploadCloud.ForeColor = System.Drawing.Color.White;
            this.btnUploadCloud.Location = new System.Drawing.Point(15, 35);
            this.btnUploadCloud.Name = "btnUploadCloud";
            this.btnUploadCloud.Size = new System.Drawing.Size(100, 80);
            this.btnUploadCloud.TabIndex = 0;
            this.btnUploadCloud.Text = "⬆️\r\nUpload";
            this.btnUploadCloud.UseVisualStyleBackColor = false;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlRight.Controls.Add(this.dgvBackups);
            this.pnlRight.Controls.Add(this.pnlStatistics);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(380, 70);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(1020, 560);
            this.pnlRight.TabIndex = 2;
            // 
            // dgvBackups
            // 
            this.dgvBackups.AllowUserToAddRows = false;
            this.dgvBackups.AllowUserToDeleteRows = false;
            this.dgvBackups.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBackups.BackgroundColor = System.Drawing.Color.White;
            this.dgvBackups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBackups.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBackups.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvBackups.ColumnHeadersHeight = 40;
            this.dgvBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBackups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBackups.EnableHeadersVisualStyles = false;
            this.dgvBackups.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvBackups.Location = new System.Drawing.Point(10, 70);
            this.dgvBackups.MultiSelect = false;
            this.dgvBackups.Name = "dgvBackups";
            this.dgvBackups.ReadOnly = true;
            this.dgvBackups.RowHeadersVisible = false;
            this.dgvBackups.RowHeadersWidth = 51;
            this.dgvBackups.RowTemplate.Height = 45;
            this.dgvBackups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackups.Size = new System.Drawing.Size(1000, 480);
            this.dgvBackups.TabIndex = 1;
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.BackColor = System.Drawing.Color.White;
            this.pnlStatistics.Controls.Add(this.lblLastBackup);
            this.pnlStatistics.Controls.Add(this.lblCloudBackups);
            this.pnlStatistics.Controls.Add(this.lblTotalSize);
            this.pnlStatistics.Controls.Add(this.lblTotalBackups);
            this.pnlStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatistics.Location = new System.Drawing.Point(10, 10);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlStatistics.Size = new System.Drawing.Size(1000, 60);
            this.pnlStatistics.TabIndex = 0;
            // 
            // lblLastBackup
            // 
            this.lblLastBackup.AutoSize = true;
            this.lblLastBackup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLastBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblLastBackup.Location = new System.Drawing.Point(15, 35);
            this.lblLastBackup.Name = "lblLastBackup";
            this.lblLastBackup.Size = new System.Drawing.Size(180, 20);
            this.lblLastBackup.TabIndex = 3;
            this.lblLastBackup.Text = "Ultimo Backup: Nessuno";
            // 
            // lblCloudBackups
            // 
            this.lblCloudBackups.AutoSize = true;
            this.lblCloudBackups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCloudBackups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblCloudBackups.Location = new System.Drawing.Point(600, 10);
            this.lblCloudBackups.Name = "lblCloudBackups";
            this.lblCloudBackups.Size = new System.Drawing.Size(93, 20);
            this.lblCloudBackups.TabIndex = 2;
            this.lblCloudBackups.Text = "Su Cloud: 0";
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.AutoSize = true;
            this.lblTotalSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblTotalSize.Location = new System.Drawing.Point(330, 10);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(189, 20);
            this.lblTotalSize.TabIndex = 1;
            this.lblTotalSize.Text = "Dimensione Totale: 0 MB";
            // 
            // lblTotalBackups
            // 
            this.lblTotalBackups.AutoSize = true;
            this.lblTotalBackups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalBackups.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotalBackups.Location = new System.Drawing.Point(15, 10);
            this.lblTotalBackups.Name = "lblTotalBackups";
            this.lblTotalBackups.Size = new System.Drawing.Size(129, 20);
            this.lblTotalBackups.TabIndex = 0;
            this.lblTotalBackups.Text = "Totale Backup: 0";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.progressBar);
            this.pnlBottom.Controls.Add(this.lblStatus);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 630);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlBottom.Size = new System.Drawing.Size(1400, 70);
            this.pnlBottom.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(20, 40);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1360, 20);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(63, 23);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Pronto";
            // 
            // BackupRestoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Name = "BackupRestoreForm";
            this.Text = "Backup e Ripristino Database";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.grpBackup.ResumeLayout(false);
            this.grpBackup.PerformLayout();
            this.grpRestore.ResumeLayout(false);
            this.grpCloud.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).EndInit();
            this.pnlStatistics.ResumeLayout(false);
            this.pnlStatistics.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOpenBackupFolder;
        private System.Windows.Forms.Button btnCleanOldBackups;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpBackup;
        private System.Windows.Forms.Button btnCreateBackup;
        private System.Windows.Forms.Button btnScheduleBackup;
        private System.Windows.Forms.CheckBox chkCompress;
        private System.Windows.Forms.CheckBox chkEncrypt;
        private System.Windows.Forms.CheckBox chkAutoUpload;
        private System.Windows.Forms.Label lblBackupNotes;
        private System.Windows.Forms.TextBox txtBackupNotes;
        private System.Windows.Forms.GroupBox grpRestore;
        private System.Windows.Forms.Button btnRestoreSelected;
        private System.Windows.Forms.Button btnRestoreFromFile;
        private System.Windows.Forms.Button btnDeleteBackup;
        private System.Windows.Forms.GroupBox grpCloud;
        private System.Windows.Forms.Button btnUploadCloud;
        private System.Windows.Forms.Button btnDownloadCloud;
        private System.Windows.Forms.Button btnConfigureCloud;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Label lblTotalBackups;
        private System.Windows.Forms.Label lblTotalSize;
        private System.Windows.Forms.Label lblCloudBackups;
        private System.Windows.Forms.Label lblLastBackup;
        private System.Windows.Forms.DataGridView dgvBackups;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
