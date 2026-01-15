namespace DialysisManagement.Views
{
    partial class ReportsForm
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
            this.grpPatientReport = new System.Windows.Forms.GroupBox();
            this.btnGeneratePatientReport = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.cboPatient = new System.Windows.Forms.ComboBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.grpSessionsReport = new System.Windows.Forms.GroupBox();
            this.btnGenerateSessionsReport = new System.Windows.Forms.Button();
            this.dtpSessionsEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpSessionsStart = new System.Windows.Forms.DateTimePicker();
            this.lblSessionsEnd = new System.Windows.Forms.Label();
            this.lblSessionsStart = new System.Windows.Forms.Label();
            this.grpMonthlyStats = new System.Windows.Forms.GroupBox();
            this.btnGenerateMonthlyStats = new System.Windows.Forms.Button();
            this.dtpMonthYear = new System.Windows.Forms.DateTimePicker();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.grpExports = new System.Windows.Forms.GroupBox();
            this.btnExportRegistroDialisi = new System.Windows.Forms.Button();
            this.btnExportSDO = new System.Windows.Forms.Button();
            this.grpPatientReport.SuspendLayout();
            this.grpSessionsReport.SuspendLayout();
            this.grpMonthlyStats.SuspendLayout();
            this.grpExports.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpPatientReport
            // 
            this.grpPatientReport.BackColor = System.Drawing.Color.White;
            this.grpPatientReport.Controls.Add(this.btnGeneratePatientReport);
            this.grpPatientReport.Controls.Add(this.dtpEndDate);
            this.grpPatientReport.Controls.Add(this.dtpStartDate);
            this.grpPatientReport.Controls.Add(this.cboPatient);
            this.grpPatientReport.Controls.Add(this.lblEndDate);
            this.grpPatientReport.Controls.Add(this.lblStartDate);
            this.grpPatientReport.Controls.Add(this.lblPatient);
            this.grpPatientReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpPatientReport.Location = new System.Drawing.Point(20, 20);
            this.grpPatientReport.Name = "grpPatientReport";
            this.grpPatientReport.Padding = new System.Windows.Forms.Padding(15);
            this.grpPatientReport.Size = new System.Drawing.Size(680, 250);
            this.grpPatientReport.TabIndex = 0;
            this.grpPatientReport.TabStop = false;
            this.grpPatientReport.Text = "Report Paziente";
            // 
            // btnGeneratePatientReport
            // 
            this.btnGeneratePatientReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGeneratePatientReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGeneratePatientReport.FlatAppearance.BorderSize = 0;
            this.btnGeneratePatientReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeneratePatientReport.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGeneratePatientReport.ForeColor = System.Drawing.Color.White;
            this.btnGeneratePatientReport.Location = new System.Drawing.Point(20, 190);
            this.btnGeneratePatientReport.Name = "btnGeneratePatientReport";
            this.btnGeneratePatientReport.Size = new System.Drawing.Size(640, 45);
            this.btnGeneratePatientReport.TabIndex = 6;
            this.btnGeneratePatientReport.Text = "📄 Genera Report Paziente";
            this.btnGeneratePatientReport.UseVisualStyleBackColor = false;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(400, 140);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(260, 32);
            this.dtpEndDate.TabIndex = 5;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(20, 140);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(260, 32);
            this.dtpStartDate.TabIndex = 4;
            // 
            // cboPatient
            // 
            this.cboPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPatient.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboPatient.FormattingEnabled = true;
            this.cboPatient.Location = new System.Drawing.Point(20, 70);
            this.cboPatient.Name = "cboPatient";
            this.cboPatient.Size = new System.Drawing.Size(640, 33);
            this.cboPatient.TabIndex = 3;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEndDate.Location = new System.Drawing.Point(400, 110);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(79, 23);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "Data Fine";
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStartDate.Location = new System.Drawing.Point(20, 110);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(88, 23);
            this.lblStartDate.TabIndex = 1;
            this.lblStartDate.Text = "Data Inizio";
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatient.Location = new System.Drawing.Point(20, 40);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(68, 23);
            this.lblPatient.TabIndex = 0;
            this.lblPatient.Text = "Paziente";
            // 
            // grpSessionsReport
            // 
            this.grpSessionsReport.BackColor = System.Drawing.Color.White;
            this.grpSessionsReport.Controls.Add(this.btnGenerateSessionsReport);
            this.grpSessionsReport.Controls.Add(this.dtpSessionsEnd);
            this.grpSessionsReport.Controls.Add(this.dtpSessionsStart);
            this.grpSessionsReport.Controls.Add(this.lblSessionsEnd);
            this.grpSessionsReport.Controls.Add(this.lblSessionsStart);
            this.grpSessionsReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpSessionsReport.Location = new System.Drawing.Point(720, 20);
            this.grpSessionsReport.Name = "grpSessionsReport";
            this.grpSessionsReport.Padding = new System.Windows.Forms.Padding(15);
            this.grpSessionsReport.Size = new System.Drawing.Size(660, 250);
            this.grpSessionsReport.TabIndex = 1;
            this.grpSessionsReport.TabStop = false;
            this.grpSessionsReport.Text = "Report Sedute";
            // 
            // btnGenerateSessionsReport
            // 
            this.btnGenerateSessionsReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGenerateSessionsReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateSessionsReport.FlatAppearance.BorderSize = 0;
            this.btnGenerateSessionsReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateSessionsReport.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGenerateSessionsReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateSessionsReport.Location = new System.Drawing.Point(20, 190);
            this.btnGenerateSessionsReport.Name = "btnGenerateSessionsReport";
            this.btnGenerateSessionsReport.Size = new System.Drawing.Size(620, 45);
            this.btnGenerateSessionsReport.TabIndex = 4;
            this.btnGenerateSessionsReport.Text = "📊 Report Sedute";
            this.btnGenerateSessionsReport.UseVisualStyleBackColor = false;
            // 
            // dtpSessionsEnd
            // 
            this.dtpSessionsEnd.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpSessionsEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSessionsEnd.Location = new System.Drawing.Point(340, 125);
            this.dtpSessionsEnd.Name = "dtpSessionsEnd";
            this.dtpSessionsEnd.Size = new System.Drawing.Size(300, 32);
            this.dtpSessionsEnd.TabIndex = 3;
            // 
            // dtpSessionsStart
            // 
            this.dtpSessionsStart.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpSessionsStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSessionsStart.Location = new System.Drawing.Point(20, 125);
            this.dtpSessionsStart.Name = "dtpSessionsStart";
            this.dtpSessionsStart.Size = new System.Drawing.Size(300, 32);
            this.dtpSessionsStart.TabIndex = 2;
            // 
            // lblSessionsEnd
            // 
            this.lblSessionsEnd.AutoSize = true;
            this.lblSessionsEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSessionsEnd.Location = new System.Drawing.Point(340, 95);
            this.lblSessionsEnd.Name = "lblSessionsEnd";
            this.lblSessionsEnd.Size = new System.Drawing.Size(79, 23);
            this.lblSessionsEnd.TabIndex = 1;
            this.lblSessionsEnd.Text = "Data Fine";
            // 
            // lblSessionsStart
            // 
            this.lblSessionsStart.AutoSize = true;
            this.lblSessionsStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSessionsStart.Location = new System.Drawing.Point(20, 95);
            this.lblSessionsStart.Name = "lblSessionsStart";
            this.lblSessionsStart.Size = new System.Drawing.Size(88, 23);
            this.lblSessionsStart.TabIndex = 0;
            this.lblSessionsStart.Text = "Data Inizio";
            // 
            // grpMonthlyStats
            // 
            this.grpMonthlyStats.BackColor = System.Drawing.Color.White;
            this.grpMonthlyStats.Controls.Add(this.btnGenerateMonthlyStats);
            this.grpMonthlyStats.Controls.Add(this.dtpMonthYear);
            this.grpMonthlyStats.Controls.Add(this.lblMonthYear);
            this.grpMonthlyStats.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpMonthlyStats.Location = new System.Drawing.Point(20, 290);
            this.grpMonthlyStats.Name = "grpMonthlyStats";
            this.grpMonthlyStats.Padding = new System.Windows.Forms.Padding(15);
            this.grpMonthlyStats.Size = new System.Drawing.Size(680, 180);
            this.grpMonthlyStats.TabIndex = 2;
            this.grpMonthlyStats.TabStop = false;
            this.grpMonthlyStats.Text = "Statistiche Mensili";
            // 
            // btnGenerateMonthlyStats
            // 
            this.btnGenerateMonthlyStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnGenerateMonthlyStats.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateMonthlyStats.FlatAppearance.BorderSize = 0;
            this.btnGenerateMonthlyStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateMonthlyStats.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGenerateMonthlyStats.ForeColor = System.Drawing.Color.White;
            this.btnGenerateMonthlyStats.Location = new System.Drawing.Point(20, 120);
            this.btnGenerateMonthlyStats.Name = "btnGenerateMonthlyStats";
            this.btnGenerateMonthlyStats.Size = new System.Drawing.Size(640, 45);
            this.btnGenerateMonthlyStats.TabIndex = 2;
            this.btnGenerateMonthlyStats.Text = "📈 Statistiche Mensili";
            this.btnGenerateMonthlyStats.UseVisualStyleBackColor = false;
            // 
            // dtpMonthYear
            // 
            this.dtpMonthYear.CustomFormat = "MMMM yyyy";
            this.dtpMonthYear.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpMonthYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonthYear.Location = new System.Drawing.Point(20, 70);
            this.dtpMonthYear.Name = "dtpMonthYear";
            this.dtpMonthYear.Size = new System.Drawing.Size(300, 32);
            this.dtpMonthYear.TabIndex = 1;
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.AutoSize = true;
            this.lblMonthYear.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMonthYear.Location = new System.Drawing.Point(20, 40);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(103, 23);
            this.lblMonthYear.TabIndex = 0;
            this.lblMonthYear.Text = "Mese / Anno";
            // 
            // grpExports
            // 
            this.grpExports.BackColor = System.Drawing.Color.White;
            this.grpExports.Controls.Add(this.btnExportRegistroDialisi);
            this.grpExports.Controls.Add(this.btnExportSDO);
            this.grpExports.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpExports.Location = new System.Drawing.Point(720, 290);
            this.grpExports.Name = "grpExports";
            this.grpExports.Padding = new System.Windows.Forms.Padding(15);
            this.grpExports.Size = new System.Drawing.Size(660, 180);
            this.grpExports.TabIndex = 3;
            this.grpExports.TabStop = false;
            this.grpExports.Text = "Export Flussi";
            // 
            // btnExportRegistroDialisi
            // 
            this.btnExportRegistroDialisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnExportRegistroDialisi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportRegistroDialisi.FlatAppearance.BorderSize = 0;
            this.btnExportRegistroDialisi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportRegistroDialisi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExportRegistroDialisi.ForeColor = System.Drawing.Color.White;
            this.btnExportRegistroDialisi.Location = new System.Drawing.Point(340, 50);
            this.btnExportRegistroDialisi.Name = "btnExportRegistroDialisi";
            this.btnExportRegistroDialisi.Size = new System.Drawing.Size(300, 110);
            this.btnExportRegistroDialisi.TabIndex = 1;
            this.btnExportRegistroDialisi.Text = "📋 Export\r\nRegistro Dialisi\r\nRegione Campania";
            this.btnExportRegistroDialisi.UseVisualStyleBackColor = false;
            // 
            // btnExportSDO
            // 
            this.btnExportSDO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnExportSDO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportSDO.FlatAppearance.BorderSize = 0;
            this.btnExportSDO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportSDO.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnExportSDO.ForeColor = System.Drawing.Color.White;
            this.btnExportSDO.Location = new System.Drawing.Point(20, 50);
            this.btnExportSDO.Name = "btnExportSDO";
            this.btnExportSDO.Size = new System.Drawing.Size(300, 110);
            this.btnExportSDO.TabIndex = 0;
            this.btnExportSDO.Text = "📤 Export SDO\r\n(Scheda Dimissione\r\nOspedaliera)";
            this.btnExportSDO.UseVisualStyleBackColor = false;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.grpExports);
            this.Controls.Add(this.grpMonthlyStats);
            this.Controls.Add(this.grpSessionsReport);
            this.Controls.Add(this.grpPatientReport);
            this.Name = "ReportsForm";
            this.Text = "Report e Statistiche";
            this.grpPatientReport.ResumeLayout(false);
            this.grpPatientReport.PerformLayout();
            this.grpSessionsReport.ResumeLayout(false);
            this.grpSessionsReport.PerformLayout();
            this.grpMonthlyStats.ResumeLayout(false);
            this.grpMonthlyStats.PerformLayout();
            this.grpExports.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpPatientReport;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.ComboBox cboPatient;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnGeneratePatientReport;
        private System.Windows.Forms.GroupBox grpSessionsReport;
        private System.Windows.Forms.Label lblSessionsStart;
        private System.Windows.Forms.Label lblSessionsEnd;
        private System.Windows.Forms.DateTimePicker dtpSessionsStart;
        private System.Windows.Forms.DateTimePicker dtpSessionsEnd;
        private System.Windows.Forms.Button btnGenerateSessionsReport;
        private System.Windows.Forms.GroupBox grpMonthlyStats;
        private System.Windows.Forms.Label lblMonthYear;
        private System.Windows.Forms.DateTimePicker dtpMonthYear;
        private System.Windows.Forms.Button btnGenerateMonthlyStats;
        private System.Windows.Forms.GroupBox grpExports;
        private System.Windows.Forms.Button btnExportSDO;
        private System.Windows.Forms.Button btnExportRegistroDialisi;
    }
}
