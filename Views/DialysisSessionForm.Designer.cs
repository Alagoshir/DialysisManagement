namespace DialysisManagement.Views
{
    partial class DialysisSessionForm
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
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblLoadingStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewSession = new System.Windows.Forms.Button();
            this.cboTurno = new System.Windows.Forms.ComboBox();
            this.lblTurno = new System.Windows.Forms.Label();
            this.btnNextDay = new System.Windows.Forms.Button();
            this.btnPrevDay = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.dtpSessionDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.lblStatKtvMedio = new System.Windows.Forms.Label();
            this.lblStatProgrammate = new System.Windows.Forms.Label();
            this.lblStatInCorso = new System.Windows.Forms.Label();
            this.lblStatCompletate = new System.Windows.Forms.Label();
            this.lblStatTotali = new System.Windows.Forms.Label();
            this.dgvSessions = new System.Windows.Forms.DataGridView();
            this.pnlToolbar.SuspendLayout();
            this.pnlStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.lblLoadingStatus);
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnNewSession);
            this.pnlToolbar.Controls.Add(this.cboTurno);
            this.pnlToolbar.Controls.Add(this.lblTurno);
            this.pnlToolbar.Controls.Add(this.btnNextDay);
            this.pnlToolbar.Controls.Add(this.btnPrevDay);
            this.pnlToolbar.Controls.Add(this.btnToday);
            this.pnlToolbar.Controls.Add(this.dtpSessionDate);
            this.pnlToolbar.Controls.Add(this.lblDate);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10);
            this.pnlToolbar.Size = new System.Drawing.Size(1400, 80);
            this.pnlToolbar.TabIndex = 0;
            // 
            // lblLoadingStatus
            // 
            this.lblLoadingStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLoadingStatus.AutoSize = true;
            this.lblLoadingStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblLoadingStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblLoadingStatus.Location = new System.Drawing.Point(1200, 45);
            this.lblLoadingStatus.Name = "lblLoadingStatus";
            this.lblLoadingStatus.Size = new System.Drawing.Size(180, 23);
            this.lblLoadingStatus.TabIndex = 9;
            this.lblLoadingStatus.Text = "Caricamento sedute...";
            this.lblLoadingStatus.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(1340, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 50);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnNewSession
            // 
            this.btnNewSession.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSession.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNewSession.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewSession.FlatAppearance.BorderSize = 0;
            this.btnNewSession.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSession.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNewSession.ForeColor = System.Drawing.Color.White;
            this.btnNewSession.Location = new System.Drawing.Point(1150, 15);
            this.btnNewSession.Name = "btnNewSession";
            this.btnNewSession.Size = new System.Drawing.Size(180, 50);
            this.btnNewSession.TabIndex = 7;
            this.btnNewSession.Text = "➕ Nuova Seduta";
            this.btnNewSession.UseVisualStyleBackColor = false;
            // 
            // cboTurno
            // 
            this.cboTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTurno.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboTurno.FormattingEnabled = true;
            this.cboTurno.Location = new System.Drawing.Point(740, 25);
            this.cboTurno.Name = "cboTurno";
            this.cboTurno.Size = new System.Drawing.Size(150, 33);
            this.cboTurno.TabIndex = 6;
            // 
            // lblTurno
            // 
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTurno.Location = new System.Drawing.Point(660, 28);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(60, 25);
            this.lblTurno.TabIndex = 5;
            this.lblTurno.Text = "Turno:";
            // 
            // btnNextDay
            // 
            this.btnNextDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnNextDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNextDay.FlatAppearance.BorderSize = 0;
            this.btnNextDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextDay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNextDay.ForeColor = System.Drawing.Color.White;
            this.btnNextDay.Location = new System.Drawing.Point(590, 20);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(50, 40);
            this.btnNextDay.TabIndex = 4;
            this.btnNextDay.Text = "▶";
            this.btnNextDay.UseVisualStyleBackColor = false;
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPrevDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevDay.FlatAppearance.BorderSize = 0;
            this.btnPrevDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevDay.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPrevDay.ForeColor = System.Drawing.Color.White;
            this.btnPrevDay.Location = new System.Drawing.Point(290, 20);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(50, 40);
            this.btnPrevDay.TabIndex = 3;
            this.btnPrevDay.Text = "◀";
            this.btnPrevDay.UseVisualStyleBackColor = false;
            // 
            // btnToday
            // 
            this.btnToday.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToday.FlatAppearance.BorderSize = 0;
            this.btnToday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToday.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnToday.ForeColor = System.Drawing.Color.White;
            this.btnToday.Location = new System.Drawing.Point(530, 20);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(50, 40);
            this.btnToday.TabIndex = 2;
            this.btnToday.Text = "Oggi";
            this.btnToday.UseVisualStyleBackColor = false;
            // 
            // dtpSessionDate
            // 
            this.dtpSessionDate.CalendarFont = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpSessionDate.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpSessionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSessionDate.Location = new System.Drawing.Point(350, 25);
            this.dtpSessionDate.Name = "dtpSessionDate";
            this.dtpSessionDate.Size = new System.Drawing.Size(230, 32);
            this.dtpSessionDate.TabIndex = 1;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDate.Location = new System.Drawing.Point(15, 28);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(54, 25);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Data:";
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.BackColor = System.Drawing.Color.White;
            this.pnlStatistics.Controls.Add(this.lblStatKtvMedio);
            this.pnlStatistics.Controls.Add(this.lblStatProgrammate);
            this.pnlStatistics.Controls.Add(this.lblStatInCorso);
            this.pnlStatistics.Controls.Add(this.lblStatCompletate);
            this.pnlStatistics.Controls.Add(this.lblStatTotali);
            this.pnlStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatistics.Location = new System.Drawing.Point(0, 80);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlStatistics.Size = new System.Drawing.Size(1400, 50);
            this.pnlStatistics.TabIndex = 1;
            // 
            // lblStatKtvMedio
            // 
            this.lblStatKtvMedio.AutoSize = true;
            this.lblStatKtvMedio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatKtvMedio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(68)))), ((int)(((byte)(173)))));
            this.lblStatKtvMedio.Location = new System.Drawing.Point(950, 13);
            this.lblStatKtvMedio.Name = "lblStatKtvMedio";
            this.lblStatKtvMedio.Size = new System.Drawing.Size(130, 23);
            this.lblStatKtvMedio.TabIndex = 4;
            this.lblStatKtvMedio.Text = "Kt/V Medio: 0.00";
            // 
            // lblStatProgrammate
            // 
            this.lblStatProgrammate.AutoSize = true;
            this.lblStatProgrammate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatProgrammate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.lblStatProgrammate.Location = new System.Drawing.Point(720, 13);
            this.lblStatProgrammate.Name = "lblStatProgrammate";
            this.lblStatProgrammate.Size = new System.Drawing.Size(147, 23);
            this.lblStatProgrammate.TabIndex = 3;
            this.lblStatProgrammate.Text = "Programmate: 0";
            // 
            // lblStatInCorso
            // 
            this.lblStatInCorso.AutoSize = true;
            this.lblStatInCorso.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatInCorso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblStatInCorso.Location = new System.Drawing.Point(530, 13);
            this.lblStatInCorso.Name = "lblStatInCorso";
            this.lblStatInCorso.Size = new System.Drawing.Size(103, 23);
            this.lblStatInCorso.TabIndex = 2;
            this.lblStatInCorso.Text = "In Corso: 0";
            // 
            // lblStatCompletate
            // 
            this.lblStatCompletate.AutoSize = true;
            this.lblStatCompletate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatCompletate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblStatCompletate.Location = new System.Drawing.Point(300, 13);
            this.lblStatCompletate.Name = "lblStatCompletate";
            this.lblStatCompletate.Size = new System.Drawing.Size(133, 23);
            this.lblStatCompletate.TabIndex = 1;
            this.lblStatCompletate.Text = "Completate: 0";
            // 
            // lblStatTotali
            // 
            this.lblStatTotali.AutoSize = true;
            this.lblStatTotali.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatTotali.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblStatTotali.Location = new System.Drawing.Point(15, 13);
            this.lblStatTotali.Name = "lblStatTotali";
            this.lblStatTotali.Size = new System.Drawing.Size(80, 23);
            this.lblStatTotali.TabIndex = 0;
            this.lblStatTotali.Text = "Totali: 0";
            // 
            // dgvSessions
            // 
            this.dgvSessions.AllowUserToAddRows = false;
            this.dgvSessions.AllowUserToDeleteRows = false;
            this.dgvSessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSessions.BackgroundColor = System.Drawing.Color.White;
            this.dgvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSessions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvSessions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvSessions.ColumnHeadersHeight = 40;
            this.dgvSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvSessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSessions.EnableHeadersVisualStyles = false;
            this.dgvSessions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvSessions.Location = new System.Drawing.Point(0, 130);
            this.dgvSessions.MultiSelect = false;
            this.dgvSessions.Name = "dgvSessions";
            this.dgvSessions.ReadOnly = true;
            this.dgvSessions.RowHeadersVisible = false;
            this.dgvSessions.RowHeadersWidth = 51;
            this.dgvSessions.RowTemplate.Height = 45;
            this.dgvSessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSessions.Size = new System.Drawing.Size(1400, 570);
            this.dgvSessions.TabIndex = 2;
            // 
            // DialysisSessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.dgvSessions);
            this.Controls.Add(this.pnlStatistics);
            this.Controls.Add(this.pnlToolbar);
            this.Name = "DialysisSessionForm";
            this.Text = "Sedute Dialitiche";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlStatistics.ResumeLayout(false);
            this.pnlStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSessions)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpSessionDate;
        private System.Windows.Forms.Button btnToday;
        private System.Windows.Forms.Button btnPrevDay;
        private System.Windows.Forms.Button btnNextDay;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.ComboBox cboTurno;
        private System.Windows.Forms.Button btnNewSession;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblLoadingStatus;
        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Label lblStatTotali;
        private System.Windows.Forms.Label lblStatCompletate;
        private System.Windows.Forms.Label lblStatInCorso;
        private System.Windows.Forms.Label lblStatProgrammate;
        private System.Windows.Forms.Label lblStatKtvMedio;
        private System.Windows.Forms.DataGridView dgvSessions;
    }
}
