namespace DialysisManagement.Views
{
    partial class InstrumentalExamsForm
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
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.cboExamType = new System.Windows.Forms.ComboBox();
            this.lblExamType = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddExam = new System.Windows.Forms.Button();
            this.cboPatient = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.pnlStatistics = new System.Windows.Forms.Panel();
            this.lblWithFiles = new System.Windows.Forms.Label();
            this.lblTotalExams = new System.Windows.Forms.Label();
            this.dgvExams = new System.Windows.Forms.DataGridView();
            this.pnlToolbar.SuspendLayout();
            this.pnlStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.btnUploadFile);
            this.pnlToolbar.Controls.Add(this.cboExamType);
            this.pnlToolbar.Controls.Add(this.lblExamType);
            this.pnlToolbar.Controls.Add(this.btnRefresh);
            this.pnlToolbar.Controls.Add(this.btnAddExam);
            this.pnlToolbar.Controls.Add(this.cboPatient);
            this.pnlToolbar.Controls.Add(this.lblPatient);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10);
            this.pnlToolbar.Size = new System.Drawing.Size(1400, 70);
            this.pnlToolbar.TabIndex = 0;
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUploadFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUploadFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUploadFile.FlatAppearance.BorderSize = 0;
            this.btnUploadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUploadFile.ForeColor = System.Drawing.Color.White;
            this.btnUploadFile.Location = new System.Drawing.Point(980, 15);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(150, 40);
            this.btnUploadFile.TabIndex = 6;
            this.btnUploadFile.Text = "📎 Allega File";
            this.btnUploadFile.UseVisualStyleBackColor = false;
            // 
            // cboExamType
            // 
            this.cboExamType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExamType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboExamType.FormattingEnabled = true;
            this.cboExamType.Location = new System.Drawing.Point(820, 20);
            this.cboExamType.Name = "cboExamType";
            this.cboExamType.Size = new System.Drawing.Size(150, 31);
            this.cboExamType.TabIndex = 5;
            // 
            // lblExamType
            // 
            this.lblExamType.AutoSize = true;
            this.lblExamType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblExamType.Location = new System.Drawing.Point(720, 23);
            this.lblExamType.Name = "lblExamType";
            this.lblExamType.Size = new System.Drawing.Size(96, 23);
            this.lblExamType.TabIndex = 4;
            this.lblExamType.Text = "Tipo Esame:";
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
            this.btnRefresh.Size = new System.Drawing.Size(50, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnAddExam
            // 
            this.btnAddExam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddExam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddExam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddExam.FlatAppearance.BorderSize = 0;
            this.btnAddExam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddExam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddExam.ForeColor = System.Drawing.Color.White;
            this.btnAddExam.Location = new System.Drawing.Point(1140, 15);
            this.btnAddExam.Name = "btnAddExam";
            this.btnAddExam.Size = new System.Drawing.Size(190, 40);
            this.btnAddExam.TabIndex = 2;
            this.btnAddExam.Text = "➕ Nuovo Esame";
            this.btnAddExam.UseVisualStyleBackColor = false;
            // 
            // cboPatient
            // 
            this.cboPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPatient.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboPatient.FormattingEnabled = true;
            this.cboPatient.Location = new System.Drawing.Point(110, 19);
            this.cboPatient.Name = "cboPatient";
            this.cboPatient.Size = new System.Drawing.Size(590, 33);
            this.cboPatient.TabIndex = 1;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPatient.Location = new System.Drawing.Point(15, 22);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(84, 25);
            this.lblPatient.TabIndex = 0;
            this.lblPatient.Text = "Paziente:";
            // 
            // pnlStatistics
            // 
            this.pnlStatistics.BackColor = System.Drawing.Color.White;
            this.pnlStatistics.Controls.Add(this.lblWithFiles);
            this.pnlStatistics.Controls.Add(this.lblTotalExams);
            this.pnlStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatistics.Location = new System.Drawing.Point(0, 70);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlStatistics.Size = new System.Drawing.Size(1400, 50);
            this.pnlStatistics.TabIndex = 1;
            // 
            // lblWithFiles
            // 
            this.lblWithFiles.AutoSize = true;
            this.lblWithFiles.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblWithFiles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblWithFiles.Location = new System.Drawing.Point(250, 13);
            this.lblWithFiles.Name = "lblWithFiles";
            this.lblWithFiles.Size = new System.Drawing.Size(132, 23);
            this.lblWithFiles.TabIndex = 1;
            this.lblWithFiles.Text = "Con allegati: 0";
            // 
            // lblTotalExams
            // 
            this.lblTotalExams.AutoSize = true;
            this.lblTotalExams.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalExams.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTotalExams.Location = new System.Drawing.Point(15, 13);
            this.lblTotalExams.Name = "lblTotalExams";
            this.lblTotalExams.Size = new System.Drawing.Size(135, 23);
            this.lblTotalExams.TabIndex = 0;
            this.lblTotalExams.Text = "Esami totali: 0";
            // 
            // dgvExams
            // 
            this.dgvExams.AllowUserToAddRows = false;
            this.dgvExams.AllowUserToDeleteRows = false;
            this.dgvExams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExams.BackgroundColor = System.Drawing.Color.White;
            this.dgvExams.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExams.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvExams.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvExams.ColumnHeadersHeight = 40;
            this.dgvExams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvExams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExams.EnableHeadersVisualStyles = false;
            this.dgvExams.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.dgvExams.Location = new System.Drawing.Point(0, 120);
            this.dgvExams.MultiSelect = false;
            this.dgvExams.Name = "dgvExams";
            this.dgvExams.ReadOnly = true;
            this.dgvExams.RowHeadersVisible = false;
            this.dgvExams.RowHeadersWidth = 51;
            this.dgvExams.RowTemplate.Height = 45;
            this.dgvExams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvExams.Size = new System.Drawing.Size(1400, 580);
            this.dgvExams.TabIndex = 2;
            // 
            // InstrumentalExamsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.dgvExams);
            this.Controls.Add(this.pnlStatistics);
            this.Controls.Add(this.pnlToolbar);
            this.Name = "InstrumentalExamsForm";
            this.Text = "Esami Strumentali";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlStatistics.ResumeLayout(false);
            this.pnlStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExams)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.ComboBox cboPatient;
        private System.Windows.Forms.Button btnAddExam;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblExamType;
        private System.Windows.Forms.ComboBox cboExamType;
        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.Panel pnlStatistics;
        private System.Windows.Forms.Label lblTotalExams;
        private System.Windows.Forms.Label lblWithFiles;
        private System.Windows.Forms.DataGridView dgvExams;
    }
}
