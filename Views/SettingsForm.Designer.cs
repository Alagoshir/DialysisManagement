namespace DialysisManagement.Views
{
    partial class SettingsForm
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.grpCentro = new System.Windows.Forms.GroupBox();
            this.txtCentroEmail = new System.Windows.Forms.TextBox();
            this.txtCentroTelefono = new System.Windows.Forms.TextBox();
            this.txtCentroIndirizzo = new System.Windows.Forms.TextBox();
            this.txtCentroCodice = new System.Windows.Forms.TextBox();
            this.txtCentroNome = new System.Windows.Forms.TextBox();
            this.lblCentroEmail = new System.Windows.Forms.Label();
            this.lblCentroTelefono = new System.Windows.Forms.Label();
            this.lblCentroIndirizzo = new System.Windows.Forms.Label();
            this.lblCentroCodice = new System.Windows.Forms.Label();
            this.lblCentroNome = new System.Windows.Forms.Label();
            this.tabSecurity = new System.Windows.Forms.TabPage();
            this.grpChangePassword = new System.Windows.Forms.GroupBox();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtOldPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.lblOldPassword = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.grpSecuritySettings = new System.Windows.Forms.GroupBox();
            this.numSessionTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblSessionTimeout = new System.Windows.Forms.Label();
            this.chkEncryptAttachments = new System.Windows.Forms.CheckBox();
            this.tabBackup = new System.Windows.Forms.TabPage();
            this.grpBackupSettings = new System.Windows.Forms.GroupBox();
            this.numBackupRetention = new System.Windows.Forms.NumericUpDown();
            this.lblBackupRetention = new System.Windows.Forms.Label();
            this.chkCloudBackup = new System.Windows.Forms.CheckBox();
            this.chkCompressBackup = new System.Windows.Forms.CheckBox();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            this.grpDatabaseSettings = new System.Windows.Forms.GroupBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.grpCentro.SuspendLayout();
            this.tabSecurity.SuspendLayout();
            this.grpChangePassword.SuspendLayout();
            this.grpSecuritySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSessionTimeout)).BeginInit();
            this.tabBackup.SuspendLayout();
            this.grpBackupSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackupRetention)).BeginInit();
            this.tabDatabase.SuspendLayout();
            this.grpDatabaseSettings.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGeneral);
            this.tabControl.Controls.Add(this.tabSecurity);
            this.tabControl.Controls.Add(this.tabBackup);
            this.tabControl.Controls.Add(this.tabDatabase);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1400, 630);
            this.tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabGeneral.Controls.Add(this.grpCentro);
            this.tabGeneral.Location = new System.Drawing.Point(4, 32);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(20);
            this.tabGeneral.Size = new System.Drawing.Size(1392, 594);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "⚙️ Generale";
            // 
            // grpCentro
            // 
            this.grpCentro.BackColor = System.Drawing.Color.White;
            this.grpCentro.Controls.Add(this.txtCentroEmail);
            this.grpCentro.Controls.Add(this.txtCentroTelefono);
            this.grpCentro.Controls.Add(this.txtCentroIndirizzo);
            this.grpCentro.Controls.Add(this.txtCentroCodice);
            this.grpCentro.Controls.Add(this.txtCentroNome);
            this.grpCentro.Controls.Add(this.lblCentroEmail);
            this.grpCentro.Controls.Add(this.lblCentroTelefono);
            this.grpCentro.Controls.Add(this.lblCentroIndirizzo);
            this.grpCentro.Controls.Add(this.lblCentroCodice);
            this.grpCentro.Controls.Add(this.lblCentroNome);
            this.grpCentro.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpCentro.Location = new System.Drawing.Point(20, 20);
            this.grpCentro.Name = "grpCentro";
            this.grpCentro.Padding = new System.Windows.Forms.Padding(15);
            this.grpCentro.Size = new System.Drawing.Size(700, 350);
            this.grpCentro.TabIndex = 0;
            this.grpCentro.TabStop = false;
            this.grpCentro.Text = "Dati Centro Dialisi";
            // 
            // txtCentroEmail
            // 
            this.txtCentroEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCentroEmail.Location = new System.Drawing.Point(220, 290);
            this.txtCentroEmail.Name = "txtCentroEmail";
            this.txtCentroEmail.Size = new System.Drawing.Size(460, 30);
            this.txtCentroEmail.TabIndex = 9;
            // 
            // txtCentroTelefono
            // 
            this.txtCentroTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCentroTelefono.Location = new System.Drawing.Point(220, 235);
            this.txtCentroTelefono.Name = "txtCentroTelefono";
            this.txtCentroTelefono.Size = new System.Drawing.Size(300, 30);
            this.txtCentroTelefono.TabIndex = 8;
            // 
            // txtCentroIndirizzo
            // 
            this.txtCentroIndirizzo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCentroIndirizzo.Location = new System.Drawing.Point(220, 180);
            this.txtCentroIndirizzo.Name = "txtCentroIndirizzo";
            this.txtCentroIndirizzo.Size = new System.Drawing.Size(460, 30);
            this.txtCentroIndirizzo.TabIndex = 7;
            // 
            // txtCentroCodice
            // 
            this.txtCentroCodice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCentroCodice.Location = new System.Drawing.Point(220, 125);
            this.txtCentroCodice.Name = "txtCentroCodice";
            this.txtCentroCodice.Size = new System.Drawing.Size(200, 30);
            this.txtCentroCodice.TabIndex = 6;
            // 
            // txtCentroNome
            // 
            this.txtCentroNome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCentroNome.Location = new System.Drawing.Point(220, 70);
            this.txtCentroNome.Name = "txtCentroNome";
            this.txtCentroNome.Size = new System.Drawing.Size(460, 30);
            this.txtCentroNome.TabIndex = 5;
            // 
            // lblCentroEmail
            // 
            this.lblCentroEmail.AutoSize = true;
            this.lblCentroEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCentroEmail.Location = new System.Drawing.Point(20, 293);
            this.lblCentroEmail.Name = "lblCentroEmail";
            this.lblCentroEmail.Size = new System.Drawing.Size(51, 23);
            this.lblCentroEmail.TabIndex = 4;
            this.lblCentroEmail.Text = "Email";
            // 
            // lblCentroTelefono
            // 
            this.lblCentroTelefono.AutoSize = true;
            this.lblCentroTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCentroTelefono.Location = new System.Drawing.Point(20, 238);
            this.lblCentroTelefono.Name = "lblCentroTelefono";
            this.lblCentroTelefono.Size = new System.Drawing.Size(74, 23);
            this.lblCentroTelefono.TabIndex = 3;
            this.lblCentroTelefono.Text = "Telefono";
            // 
            // lblCentroIndirizzo
            // 
            this.lblCentroIndirizzo.AutoSize = true;
            this.lblCentroIndirizzo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCentroIndirizzo.Location = new System.Drawing.Point(20, 183);
            this.lblCentroIndirizzo.Name = "lblCentroIndirizzo";
            this.lblCentroIndirizzo.Size = new System.Drawing.Size(77, 23);
            this.lblCentroIndirizzo.TabIndex = 2;
            this.lblCentroIndirizzo.Text = "Indirizzo";
            // 
            // lblCentroCodice
            // 
            this.lblCentroCodice.AutoSize = true;
            this.lblCentroCodice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCentroCodice.Location = new System.Drawing.Point(20, 128);
            this.lblCentroCodice.Name = "lblCentroCodice";
            this.lblCentroCodice.Size = new System.Drawing.Size(116, 23);
            this.lblCentroCodice.TabIndex = 1;
            this.lblCentroCodice.Text = "Codice Centro";
            // 
            // lblCentroNome
            // 
            this.lblCentroNome.AutoSize = true;
            this.lblCentroNome.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCentroNome.Location = new System.Drawing.Point(20, 73);
            this.lblCentroNome.Name = "lblCentroNome";
            this.lblCentroNome.Size = new System.Drawing.Size(113, 23);
            this.lblCentroNome.TabIndex = 0;
            this.lblCentroNome.Text = "Nome Centro";
            // 
            // tabSecurity
            // 
            this.tabSecurity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabSecurity.Controls.Add(this.grpChangePassword);
            this.tabSecurity.Controls.Add(this.grpSecuritySettings);
            this.tabSecurity.Location = new System.Drawing.Point(4, 32);
            this.tabSecurity.Name = "tabSecurity";
            this.tabSecurity.Padding = new System.Windows.Forms.Padding(20);
            this.tabSecurity.Size = new System.Drawing.Size(1392, 594);
            this.tabSecurity.TabIndex = 1;
            this.tabSecurity.Text = "🔒 Sicurezza";
            // 
            // grpChangePassword
            // 
            this.grpChangePassword.BackColor = System.Drawing.Color.White;
            this.grpChangePassword.Controls.Add(this.btnChangePassword);
            this.grpChangePassword.Controls.Add(this.txtConfirmPassword);
            this.grpChangePassword.Controls.Add(this.txtNewPassword);
            this.grpChangePassword.Controls.Add(this.txtOldPassword);
            this.grpChangePassword.Controls.Add(this.lblConfirmPassword);
            this.grpChangePassword.Controls.Add(this.lblNewPassword);
            this.grpChangePassword.Controls.Add(this.lblOldPassword);
            this.grpChangePassword.Controls.Add(this.lblCurrentUser);
            this.grpChangePassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpChangePassword.Location = new System.Drawing.Point(20, 20);
            this.grpChangePassword.Name = "grpChangePassword";
            this.grpChangePassword.Padding = new System.Windows.Forms.Padding(15);
            this.grpChangePassword.Size = new System.Drawing.Size(700, 350);
            this.grpChangePassword.TabIndex = 0;
            this.grpChangePassword.TabStop = false;
            this.grpChangePassword.Text = "Cambio Password";
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderSize = 0;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(220, 280);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(300, 45);
            this.btnChangePassword.TabIndex = 7;
            this.btnChangePassword.Text = "🔑 Cambia Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(220, 225);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '●';
            this.txtConfirmPassword.Size = new System.Drawing.Size(300, 30);
            this.txtConfirmPassword.TabIndex = 6;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPassword.Location = new System.Drawing.Point(220, 170);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '●';
            this.txtNewPassword.Size = new System.Drawing.Size(300, 30);
            this.txtNewPassword.TabIndex = 5;
            this.txtNewPassword.UseSystemPasswordChar = true;
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtOldPassword.Location = new System.Drawing.Point(220, 115);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PasswordChar = '●';
            this.txtOldPassword.Size = new System.Drawing.Size(300, 30);
            this.txtOldPassword.TabIndex = 4;
            this.txtOldPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConfirmPassword.Location = new System.Drawing.Point(20, 228);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(156, 23);
            this.lblConfirmPassword.TabIndex = 3;
            this.lblConfirmPassword.Text = "Conferma Password";
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNewPassword.Location = new System.Drawing.Point(20, 173);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(132, 23);
            this.lblNewPassword.TabIndex = 2;
            this.lblNewPassword.Text = "Nuova Password";
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.AutoSize = true;
            this.lblOldPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblOldPassword.Location = new System.Drawing.Point(20, 118);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.Size = new System.Drawing.Size(144, 23);
            this.lblOldPassword.TabIndex = 1;
            this.lblOldPassword.Text = "Vecchia Password";
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblCurrentUser.Location = new System.Drawing.Point(20, 60);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(65, 23);
            this.lblCurrentUser.TabIndex = 0;
            this.lblCurrentUser.Text = "Utente:";
            // 
            // grpSecuritySettings
            // 
            this.grpSecuritySettings.BackColor = System.Drawing.Color.White;
            this.grpSecuritySettings.Controls.Add(this.numSessionTimeout);
            this.grpSecuritySettings.Controls.Add(this.lblSessionTimeout);
            this.grpSecuritySettings.Controls.Add(this.chkEncryptAttachments);
            this.grpSecuritySettings.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpSecuritySettings.Location = new System.Drawing.Point(740, 20);
            this.grpSecuritySettings.Name = "grpSecuritySettings";
            this.grpSecuritySettings.Padding = new System.Windows.Forms.Padding(15);
            this.grpSecuritySettings.Size = new System.Drawing.Size(630, 350);
            this.grpSecuritySettings.TabIndex = 1;
            this.grpSecuritySettings.TabStop = false;
            this.grpSecuritySettings.Text = "Impostazioni Sicurezza";
            // 
            // numSessionTimeout
            // 
            this.numSessionTimeout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSessionTimeout.Location = new System.Drawing.Point(280, 120);
            this.numSessionTimeout.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numSessionTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSessionTimeout.Name = "numSessionTimeout";
            this.numSessionTimeout.Size = new System.Drawing.Size(100, 30);
            this.numSessionTimeout.TabIndex = 2;
            this.numSessionTimeout.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblSessionTimeout
            // 
            this.lblSessionTimeout.AutoSize = true;
            this.lblSessionTimeout.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSessionTimeout.Location = new System.Drawing.Point(20, 122);
            this.lblSessionTimeout.Name = "lblSessionTimeout";
            this.lblSessionTimeout.Size = new System.Drawing.Size(239, 23);
            this.lblSessionTimeout.TabIndex = 1;
            this.lblSessionTimeout.Text = "Timeout Sessione (minuti):";
            // 
            // chkEncryptAttachments
            // 
            this.chkEncryptAttachments.AutoSize = true;
            this.chkEncryptAttachments.Checked = true;
            this.chkEncryptAttachments.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEncryptAttachments.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEncryptAttachments.Location = new System.Drawing.Point(20, 60);
            this.chkEncryptAttachments.Name = "chkEncryptAttachments";
            this.chkEncryptAttachments.Size = new System.Drawing.Size(201, 27);
            this.chkEncryptAttachments.TabIndex = 0;
            this.chkEncryptAttachments.Text = "Cripta File Allegati";
            this.chkEncryptAttachments.UseVisualStyleBackColor = true;
            // 
            // tabBackup
            // 
            this.tabBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabBackup.Controls.Add(this.grpBackupSettings);
            this.tabBackup.Location = new System.Drawing.Point(4, 32);
            this.tabBackup.Name = "tabBackup";
            this.tabBackup.Padding = new System.Windows.Forms.Padding(20);
            this.tabBackup.Size = new System.Drawing.Size(1392, 594);
            this.tabBackup.TabIndex = 2;
            this.tabBackup.Text = "💾 Backup";
            // 
            // grpBackupSettings
            // 
            this.grpBackupSettings.BackColor = System.Drawing.Color.White;
            this.grpBackupSettings.Controls.Add(this.numBackupRetention);
            this.grpBackupSettings.Controls.Add(this.lblBackupRetention);
            this.grpBackupSettings.Controls.Add(this.chkCloudBackup);
            this.grpBackupSettings.Controls.Add(this.chkCompressBackup);
            this.grpBackupSettings.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpBackupSettings.Location = new System.Drawing.Point(20, 20);
            this.grpBackupSettings.Name = "grpBackupSettings";
            this.grpBackupSettings.Padding = new System.Windows.Forms.Padding(15);
            this.grpBackupSettings.Size = new System.Drawing.Size(700, 250);
            this.grpBackupSettings.TabIndex = 0;
            this.grpBackupSettings.TabStop = false;
            this.grpBackupSettings.Text = "Impostazioni Backup";
            // 
            // numBackupRetention
            // 
            this.numBackupRetention.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numBackupRetention.Location = new System.Drawing.Point(280, 165);
            this.numBackupRetention.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numBackupRetention.Minimum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numBackupRetention.Name = "numBackupRetention";
            this.numBackupRetention.Size = new System.Drawing.Size(100, 30);
            this.numBackupRetention.TabIndex = 3;
            this.numBackupRetention.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblBackupRetention
            // 
            this.lblBackupRetention.AutoSize = true;
            this.lblBackupRetention.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBackupRetention.Location = new System.Drawing.Point(20, 167);
            this.lblBackupRetention.Name = "lblBackupRetention";
            this.lblBackupRetention.Size = new System.Drawing.Size(243, 23);
            this.lblBackupRetention.TabIndex = 2;
            this.lblBackupRetention.Text = "Conserva Backup (giorni):";
            // 
            // chkCloudBackup
            // 
            this.chkCloudBackup.AutoSize = true;
            this.chkCloudBackup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkCloudBackup.Location = new System.Drawing.Point(20, 115);
            this.chkCloudBackup.Name = "chkCloudBackup";
            this.chkCloudBackup.Size = new System.Drawing.Size(289, 27);
            this.chkCloudBackup.TabIndex = 1;
            this.chkCloudBackup.Text = "Abilita Backup Cloud Automatico";
            this.chkCloudBackup.UseVisualStyleBackColor = true;
            // 
            // chkCompressBackup
            // 
            this.chkCompressBackup.AutoSize = true;
            this.chkCompressBackup.Checked = true;
            this.chkCompressBackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCompressBackup.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkCompressBackup.Location = new System.Drawing.Point(20, 60);
            this.chkCompressBackup.Name = "chkCompressBackup";
            this.chkCompressBackup.Size = new System.Drawing.Size(182, 27);
            this.chkCompressBackup.TabIndex = 0;
            this.chkCompressBackup.Text = "Comprimi Backup";
            this.chkCompressBackup.UseVisualStyleBackColor = true;
            // 
            // tabDatabase
            // 
            this.tabDatabase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.tabDatabase.Controls.Add(this.grpDatabaseSettings);
            this.tabDatabase.Location = new System.Drawing.Point(4, 32);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(20);
            this.tabDatabase.Size = new System.Drawing.Size(1392, 594);
            this.tabDatabase.TabIndex = 3;
            this.tabDatabase.Text = "🗄️ Database";
            // 
            // grpDatabaseSettings
            // 
            this.grpDatabaseSettings.BackColor = System.Drawing.Color.White;
            this.grpDatabaseSettings.Controls.Add(this.btnTestConnection);
            this.grpDatabaseSettings.Controls.Add(this.lblConnectionString);
            this.grpDatabaseSettings.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpDatabaseSettings.Location = new System.Drawing.Point(20, 20);
            this.grpDatabaseSettings.Name = "grpDatabaseSettings";
            this.grpDatabaseSettings.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatabaseSettings.Size = new System.Drawing.Size(700, 200);
            this.grpDatabaseSettings.TabIndex = 0;
            this.grpDatabaseSettings.TabStop = false;
            this.grpDatabaseSettings.Text = "Connessione Database";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnTestConnection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestConnection.FlatAppearance.BorderSize = 0;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTestConnection.ForeColor = System.Drawing.Color.White;
            this.btnTestConnection.Location = new System.Drawing.Point(20, 130);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(250, 45);
            this.btnTestConnection.TabIndex = 1;
            this.btnTestConnection.Text = "🔌 Test Connessione";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblConnectionString.Location = new System.Drawing.Point(20, 60);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(397, 23);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Text = "Database: MySQL 8.0+ Portable (./Database/)";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.White;
            this.pnlBottom.Controls.Add(this.btnSaveSettings);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 630);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBottom.Size = new System.Drawing.Size(1400, 70);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSaveSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveSettings.FlatAppearance.BorderSize = 0;
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this.btnSaveSettings.Location = new System.Drawing.Point(1180, 15);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(200, 45);
            this.btnSaveSettings.TabIndex = 0;
            this.btnSaveSettings.Text = "💾 Salva Impostazioni";
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlBottom);
            this.Name = "SettingsForm";
            this.Text = "Impostazioni";
            this.tabControl.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.grpCentro.ResumeLayout(false);
            this.grpCentro.PerformLayout();
            this.tabSecurity.ResumeLayout(false);
            this.grpChangePassword.ResumeLayout(false);
            this.grpChangePassword.PerformLayout();
            this.grpSecuritySettings.ResumeLayout(false);
            this.grpSecuritySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSessionTimeout)).EndInit();
            this.tabBackup.ResumeLayout(false);
            this.grpBackupSettings.ResumeLayout(false);
            this.grpBackupSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBackupRetention)).EndInit();
            this.tabDatabase.ResumeLayout(false);
            this.grpDatabaseSettings.ResumeLayout(false);
            this.grpDatabaseSettings.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox grpCentro;
        private System.Windows.Forms.Label lblCentroNome;
        private System.Windows.Forms.Label lblCentroCodice;
        private System.Windows.Forms.Label lblCentroIndirizzo;
        private System.Windows.Forms.Label lblCentroTelefono;
        private System.Windows.Forms.Label lblCentroEmail;
        private System.Windows.Forms.TextBox txtCentroNome;
        private System.Windows.Forms.TextBox txtCentroCodice;
        private System.Windows.Forms.TextBox txtCentroIndirizzo;
        private System.Windows.Forms.TextBox txtCentroTelefono;
        private System.Windows.Forms.TextBox txtCentroEmail;
        private System.Windows.Forms.TabPage tabSecurity;
        private System.Windows.Forms.GroupBox grpChangePassword;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label lblOldPassword;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtOldPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.GroupBox grpSecuritySettings;
        private System.Windows.Forms.CheckBox chkEncryptAttachments;
        private System.Windows.Forms.Label lblSessionTimeout;
        private System.Windows.Forms.NumericUpDown numSessionTimeout;
        private System.Windows.Forms.TabPage tabBackup;
        private System.Windows.Forms.GroupBox grpBackupSettings;
        private System.Windows.Forms.CheckBox chkCompressBackup;
        private System.Windows.Forms.CheckBox chkCloudBackup;
        private System.Windows.Forms.Label lblBackupRetention;
        private System.Windows.Forms.NumericUpDown numBackupRetention;
        private System.Windows.Forms.TabPage tabDatabase;
        private System.Windows.Forms.GroupBox grpDatabaseSettings;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnSaveSettings;
    }
}
