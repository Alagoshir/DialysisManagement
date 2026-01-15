namespace DialysisManagement.Views
{
    partial class PatientDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabAnagrafica = new System.Windows.Forms.TabPage();
            this.pnlAnagrafica = new System.Windows.Forms.Panel();
            this.txtCodiceFiscale = new System.Windows.Forms.TextBox();
            this.lblCodiceFiscale = new System.Windows.Forms.Label();
            this.txtCognome = new System.Windows.Forms.TextBox();
            this.lblCognome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.dtpDataNascita = new System.Windows.Forms.DateTimePicker();
            this.lblDataNascita = new System.Windows.Forms.Label();
            this.cboSesso = new System.Windows.Forms.ComboBox();
            this.lblSesso = new System.Windows.Forms.Label();
            this.txtLuogoNascita = new System.Windows.Forms.TextBox();
            this.lblLuogoNascita = new System.Windows.Forms.Label();
            this.txtIndirizzo = new System.Windows.Forms.TextBox();
            this.lblIndirizzo = new System.Windows.Forms.Label();
            this.txtCitta = new System.Windows.Forms.TextBox();
            this.lblCitta = new System.Windows.Forms.Label();
            this.txtCAP = new System.Windows.Forms.TextBox();
            this.lblCAP = new System.Windows.Forms.Label();
            this.txtProvincia = new System.Windows.Forms.TextBox();
            this.lblProvincia = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtCellulare = new System.Windows.Forms.TextBox();
            this.lblCellulare = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.tabClinici = new System.Windows.Forms.TabPage();
            this.pnlClinici = new System.Windows.Forms.Panel();
            this.txtNumeroTessera = new System.Windows.Forms.TextBox();
            this.lblNumeroTessera = new System.Windows.Forms.Label();
            this.txtMedicoBase = new System.Windows.Forms.TextBox();
            this.lblMedicoBase = new System.Windows.Forms.Label();
            this.cboGruppoSanguigno = new System.Windows.Forms.ComboBox();
            this.lblGruppoSanguigno = new System.Windows.Forms.Label();
            this.txtPesoSecco = new System.Windows.Forms.TextBox();
            this.lblPesoSecco = new System.Windows.Forms.Label();
            this.txtAltezza = new System.Windows.Forms.TextBox();
            this.lblAltezza = new System.Windows.Forms.Label();
            this.dtpDataInizioDialisi = new System.Windows.Forms.DateTimePicker();
            this.lblDataInizioDialisi = new System.Windows.Forms.Label();
            this.txtNefropatiaBase = new System.Windows.Forms.TextBox();
            this.lblNefropatiaBase = new System.Windows.Forms.Label();
            this.cboTipoTrattamento = new System.Windows.Forms.ComboBox();
            this.lblTipoTrattamento = new System.Windows.Forms.Label();
            this.numFrequenza = new System.Windows.Forms.NumericUpDown();
            this.lblFrequenza = new System.Windows.Forms.Label();
            this.chkHBsAg = new System.Windows.Forms.CheckBox();
            this.chkHCV = new System.Windows.Forms.CheckBox();
            this.chkHIV = new System.Windows.Forms.CheckBox();
            this.chkInListaTrapianto = new System.Windows.Forms.CheckBox();
            this.dtpDataInserimentoLista = new System.Windows.Forms.DateTimePicker();
            this.lblDataInserimentoLista = new System.Windows.Forms.Label();
            this.tabAccessi = new System.Windows.Forms.TabPage();
            this.dgvAccessi = new System.Windows.Forms.DataGridView();
            this.pnlAccessiButtons = new System.Windows.Forms.Panel();
            this.btnAddAccesso = new System.Windows.Forms.Button();
            this.btnEditAccesso = new System.Windows.Forms.Button();
            this.btnDeleteAccesso = new System.Windows.Forms.Button();
            this.tabConsensi = new System.Windows.Forms.TabPage();
            this.pnlConsensi = new System.Windows.Forms.Panel();
            this.chkConsensoPrivacy = new System.Windows.Forms.CheckBox();
            this.dtpDataConsensoPrivacy = new System.Windows.Forms.DateTimePicker();
            this.lblDataConsensoPrivacy = new System.Windows.Forms.Label();
            this.chkConsensoTrattamento = new System.Windows.Forms.CheckBox();
            this.btnStampaModuli = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabAnagrafica.SuspendLayout();
            this.pnlAnagrafica.SuspendLayout();
            this.tabClinici.SuspendLayout();
            this.pnlClinici.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequenza)).BeginInit();
            this.tabAccessi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccessi)).BeginInit();
            this.pnlAccessiButtons.SuspendLayout();
            this.tabConsensi.SuspendLayout();
            this.pnlConsensi.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.pnlTop.Controls.Add(this.btnSave);
            this.pnlTop.Controls.Add(this.btnCancel);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlTop.Size = new System.Drawing.Size(1200, 80);
            this.pnlTop.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(920, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "💾 Salva";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(1050, 20);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 45);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "✖ Annulla";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dettaglio Paziente";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabAnagrafica);
            this.tabControl.Controls.Add(this.tabClinici);
            this.tabControl.Controls.Add(this.tabAccessi);
            this.tabControl.Controls.Add(this.tabConsensi);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 80);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 620);
            this.tabControl.TabIndex = 1;
            // 
            // tabAnagrafica
            // 
            this.tabAnagrafica.BackColor = System.Drawing.Color.White;
            this.tabAnagrafica.Controls.Add(this.pnlAnagrafica);
            this.tabAnagrafica.Location = new System.Drawing.Point(4, 32);
            this.tabAnagrafica.Name = "tabAnagrafica";
            this.tabAnagrafica.Padding = new System.Windows.Forms.Padding(3);
            this.tabAnagrafica.Size = new System.Drawing.Size(1192, 584);
            this.tabAnagrafica.TabIndex = 0;
            this.tabAnagrafica.Text = "Anagrafica";
            // 
            // pnlAnagrafica
            // 
            this.pnlAnagrafica.AutoScroll = true;
            this.pnlAnagrafica.Controls.Add(this.lblCodiceFiscale);
            this.pnlAnagrafica.Controls.Add(this.txtCodiceFiscale);
            this.pnlAnagrafica.Controls.Add(this.lblCognome);
            this.pnlAnagrafica.Controls.Add(this.txtCognome);
            this.pnlAnagrafica.Controls.Add(this.lblNome);
            this.pnlAnagrafica.Controls.Add(this.txtNome);
            this.pnlAnagrafica.Controls.Add(this.lblDataNascita);
            this.pnlAnagrafica.Controls.Add(this.dtpDataNascita);
            this.pnlAnagrafica.Controls.Add(this.lblSesso);
            this.pnlAnagrafica.Controls.Add(this.cboSesso);
            this.pnlAnagrafica.Controls.Add(this.lblLuogoNascita);
            this.pnlAnagrafica.Controls.Add(this.txtLuogoNascita);
            this.pnlAnagrafica.Controls.Add(this.lblIndirizzo);
            this.pnlAnagrafica.Controls.Add(this.txtIndirizzo);
            this.pnlAnagrafica.Controls.Add(this.lblCitta);
            this.pnlAnagrafica.Controls.Add(this.txtCitta);
            this.pnlAnagrafica.Controls.Add(this.lblCAP);
            this.pnlAnagrafica.Controls.Add(this.txtCAP);
            this.pnlAnagrafica.Controls.Add(this.lblProvincia);
            this.pnlAnagrafica.Controls.Add(this.txtProvincia);
            this.pnlAnagrafica.Controls.Add(this.lblTelefono);
            this.pnlAnagrafica.Controls.Add(this.txtTelefono);
            this.pnlAnagrafica.Controls.Add(this.lblCellulare);
            this.pnlAnagrafica.Controls.Add(this.txtCellulare);
            this.pnlAnagrafica.Controls.Add(this.lblEmail);
            this.pnlAnagrafica.Controls.Add(this.txtEmail);
            this.pnlAnagrafica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAnagrafica.Location = new System.Drawing.Point(3, 3);
            this.pnlAnagrafica.Name = "pnlAnagrafica";
            this.pnlAnagrafica.Padding = new System.Windows.Forms.Padding(20);
            this.pnlAnagrafica.Size = new System.Drawing.Size(1186, 578);
            this.pnlAnagrafica.TabIndex = 0;

            // Resto dei controlli con inizializzazione base
            // (per brevità, aggiungo solo le dichiarazioni essenziali)

            // 
            // PatientDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dettaglio Paziente";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabAnagrafica.ResumeLayout(false);
            this.pnlAnagrafica.ResumeLayout(false);
            this.pnlAnagrafica.PerformLayout();
            this.tabClinici.ResumeLayout(false);
            this.pnlClinici.ResumeLayout(false);
            this.pnlClinici.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFrequenza)).EndInit();
            this.tabAccessi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccessi)).EndInit();
            this.pnlAccessiButtons.ResumeLayout(false);
            this.tabConsensi.ResumeLayout(false);
            this.pnlConsensi.ResumeLayout(false);
            this.pnlConsensi.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // ========================================
        // DICHIARAZIONI CONTROLLI
        // ========================================

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabAnagrafica;
        private System.Windows.Forms.TabPage tabClinici;
        private System.Windows.Forms.TabPage tabAccessi;
        private System.Windows.Forms.TabPage tabConsensi;

        // Tab Anagrafica
        private System.Windows.Forms.Panel pnlAnagrafica;
        private System.Windows.Forms.TextBox txtCodiceFiscale;
        private System.Windows.Forms.Label lblCodiceFiscale;
        private System.Windows.Forms.TextBox txtCognome;
        private System.Windows.Forms.Label lblCognome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.DateTimePicker dtpDataNascita;
        private System.Windows.Forms.Label lblDataNascita;
        private System.Windows.Forms.ComboBox cboSesso;
        private System.Windows.Forms.Label lblSesso;
        private System.Windows.Forms.TextBox txtLuogoNascita;
        private System.Windows.Forms.Label lblLuogoNascita;
        private System.Windows.Forms.TextBox txtIndirizzo;
        private System.Windows.Forms.Label lblIndirizzo;
        private System.Windows.Forms.TextBox txtCitta;
        private System.Windows.Forms.Label lblCitta;
        private System.Windows.Forms.TextBox txtCAP;
        private System.Windows.Forms.Label lblCAP;
        private System.Windows.Forms.TextBox txtProvincia;
        private System.Windows.Forms.Label lblProvincia;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.TextBox txtCellulare;
        private System.Windows.Forms.Label lblCellulare;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblEmail;

        // Tab Clinici
        private System.Windows.Forms.Panel pnlClinici;
        private System.Windows.Forms.TextBox txtNumeroTessera;
        private System.Windows.Forms.Label lblNumeroTessera;
        private System.Windows.Forms.TextBox txtMedicoBase;
        private System.Windows.Forms.Label lblMedicoBase;
        private System.Windows.Forms.ComboBox cboGruppoSanguigno;
        private System.Windows.Forms.Label lblGruppoSanguigno;
        private System.Windows.Forms.TextBox txtPesoSecco;
        private System.Windows.Forms.Label lblPesoSecco;
        private System.Windows.Forms.TextBox txtAltezza;
        private System.Windows.Forms.Label lblAltezza;
        private System.Windows.Forms.DateTimePicker dtpDataInizioDialisi;
        private System.Windows.Forms.Label lblDataInizioDialisi;
        private System.Windows.Forms.TextBox txtNefropatiaBase;
        private System.Windows.Forms.Label lblNefropatiaBase;
        private System.Windows.Forms.ComboBox cboTipoTrattamento;
        private System.Windows.Forms.Label lblTipoTrattamento;
        private System.Windows.Forms.NumericUpDown numFrequenza;
        private System.Windows.Forms.Label lblFrequenza;
        private System.Windows.Forms.CheckBox chkHBsAg;
        private System.Windows.Forms.CheckBox chkHCV;
        private System.Windows.Forms.CheckBox chkHIV;
        private System.Windows.Forms.CheckBox chkInListaTrapianto;
        private System.Windows.Forms.DateTimePicker dtpDataInserimentoLista;
        private System.Windows.Forms.Label lblDataInserimentoLista;

        // Tab Accessi
        private System.Windows.Forms.DataGridView dgvAccessi;
        private System.Windows.Forms.Panel pnlAccessiButtons;
        private System.Windows.Forms.Button btnAddAccesso;
        private System.Windows.Forms.Button btnEditAccesso;
        private System.Windows.Forms.Button btnDeleteAccesso;

        // Tab Consensi
        private System.Windows.Forms.Panel pnlConsensi;
        private System.Windows.Forms.CheckBox chkConsensoPrivacy;
        private System.Windows.Forms.DateTimePicker dtpDataConsensoPrivacy;
        private System.Windows.Forms.Label lblDataConsensoPrivacy;
        private System.Windows.Forms.CheckBox chkConsensoTrattamento;
        private System.Windows.Forms.Button btnStampaModuli;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label lblNote;
    }
}
