using DialysisManagement.Models;
using DialysisManagement.Services;
using DialysisManagement.Utilities;
using NLog;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialysisManagement.Views
{
    public partial class InstrumentalExamsForm : Form
    {
        private readonly IInstrumentalExamService _examService;
        private readonly IPatientService _patientService;
        private readonly IFileHelper _fileHelper;
        private readonly Logger _logger;
        private int? _selectedPatientId;

        public InstrumentalExamsForm(
            IInstrumentalExamService examService,
            IPatientService patientService,
            IFileHelper fileHelper)
        {
            InitializeComponent();

            _examService = examService ?? throw new ArgumentNullException(nameof(examService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
            LoadPatientsAsync();
        }

        private void InitializeCustomControls()
        {
            // Eventi
            cboPatient.SelectedIndexChanged += async (s, e) =>
            {
                if (cboPatient.SelectedValue is int patientId)
                {
                    _selectedPatientId = patientId;
                    await LoadExamsAsync();
                }
            };

            btnAddExam.Click += BtnAddExam_Click;
            btnUploadFile.Click += BtnUploadFile_Click;
            btnRefresh.Click += async (s, e) => await LoadExamsAsync();

            // Tipo esame
            cboExamType.Items.AddRange(new object[]
            {
                "Tutti", "ECG", "Ecocardiogramma", "Ecografia",
                "RX Torace", "TAC", "RMN", "Altro"
            });
            cboExamType.SelectedIndex = 0;
            cboExamType.SelectedIndexChanged += async (s, e) => await LoadExamsAsync();

            // Configura DataGridView
            ConfigureDataGridView();
        }

        private async Task LoadPatientsAsync()
        {
            try
            {
                var patients = await _patientService.GetActivePatientsAsync();
                var patientsList = patients
                    .Select(p => new { p.PatientId, FullName = $"{p.Cognome} {p.Nome} - {p.CodiceFiscale}" })
                    .OrderBy(p => p.FullName)
                    .ToList();

                cboPatient.DisplayMember = "FullName";
                cboPatient.ValueMember = "PatientId";
                cboPatient.DataSource = patientsList;

                if (patientsList.Any())
                {
                    cboPatient.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento pazienti");
                MessageBox.Show("Errore nel caricamento dei pazienti", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExamId",
                DataPropertyName = "ExamId",
                Visible = false
            });

            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TipoEsame",
                HeaderText = "Tipo Esame",
                DataPropertyName = "TipoEsame",
                Width = 150
            });

            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataEsecuzione",
                HeaderText = "Data Esecuzione",
                DataPropertyName = "DataEsecuzione",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataReferto",
                HeaderText = "Data Referto",
                DataPropertyName = "DataReferto",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });

            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Medico",
                HeaderText = "Medico Richiedente",
                DataPropertyName = "Medico",
                Width = 180
            });

            dgvExams.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "EsameInterno",
                HeaderText = "Interno",
                DataPropertyName = "EsameInterno",
                Width = 80
            });

            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodiceNomenclatore",
                HeaderText = "Cod. SDO",
                DataPropertyName = "CodiceNomenclatore",
                Width = 100
            });

            dgvExams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Note",
                HeaderText = "Note",
                DataPropertyName = "Note",
                Width = 200
            });

            dgvExams.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "HasAttachments",
                HeaderText = "File",
                Width = 60
            });

            var btnViewColumn = new DataGridViewButtonColumn
            {
                Name = "btnView",
                HeaderText = "Azioni",
                Text = "Dettagli",
                UseColumnTextForButtonValue = true,
                Width = 90
            };
            dgvExams.Columns.Add(btnViewColumn);

            dgvExams.CellClick += DgvExams_CellClick;
        }

        private async Task LoadExamsAsync()
        {
            try
            {
                if (!_selectedPatientId.HasValue)
                    return;

                var exams = await _examService.GetExamsByPatientAsync(_selectedPatientId.Value);
                var examsList = exams.OrderByDescending(e => e.DataEsecuzione).ToList();

                // Filtro tipo esame
                var examTypeFilter = cboExamType.SelectedItem?.ToString();
                if (!string.IsNullOrEmpty(examTypeFilter) && examTypeFilter != "Tutti")
                {
                    examsList = examsList
                        .Where(e => e.TipoEsame?.Equals(examTypeFilter, StringComparison.OrdinalIgnoreCase) ?? false)
                        .ToList();
                }

                // Controlla allegati
                foreach (var exam in examsList)
                {
                    var attachments = await _examService.GetAttachmentsByExamAsync(exam.ExamId);
                    exam.HasAttachments = attachments.Any();
                }

                dgvExams.DataSource = null;
                dgvExams.DataSource = examsList;

                lblTotalExams.Text = $"Esami totali: {examsList.Count}";
                lblWithFiles.Text = $"Con allegati: {examsList.Count(e => e.HasAttachments)}";

                _logger.Info($"Caricati {examsList.Count} esami per paziente ID: {_selectedPatientId}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento esami");
                MessageBox.Show("Errore nel caricamento degli esami", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvExams_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var columnName = dgvExams.Columns[e.ColumnIndex].Name;

                if (columnName == "btnView")
                {
                    OpenExamDetail(e.RowIndex);
                }
            }
        }

        private void OpenExamDetail(int rowIndex)
        {
            try
            {
                var examId = Convert.ToInt32(dgvExams.Rows[rowIndex].Cells["ExamId"].Value);

                // TODO: Aprire form dettaglio esame con visualizzazione file
                MessageBox.Show($"Apertura dettaglio esame ID: {examId}", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore apertura dettaglio esame");
                MessageBox.Show("Errore nell'apertura del dettaglio", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddExam_Click(object sender, EventArgs e)
        {
            if (!_selectedPatientId.HasValue)
            {
                MessageBox.Show("Selezionare un paziente", "Attenzione",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // TODO: Aprire form nuovo esame
                MessageBox.Show("Funzionalità in sviluppo", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore creazione nuovo esame");
            }
        }

        private async void BtnUploadFile_Click(object sender, EventArgs e)
        {
            if (dgvExams.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selezionare un esame", "Attenzione",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "PDF Files|*.pdf|Image Files|*.jpg;*.jpeg;*.png|DICOM Files|*.dcm|All Files|*.*";
                    openFileDialog.Multiselect = true;
                    openFileDialog.Title = "Seleziona file da allegare";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var examId = Convert.ToInt32(dgvExams.SelectedRows[0].Cells["ExamId"].Value);

                        foreach (var filePath in openFileDialog.FileNames)
                        {
                            var fileData = await File.ReadAllBytesAsync(filePath);
                            var fileName = Path.GetFileName(filePath);

                            // TODO: Salvare allegato
                            _logger.Info($"Upload file: {fileName} per esame ID: {examId}");
                        }

                        MessageBox.Show("File caricati con successo", "Successo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await LoadExamsAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore upload file");
                MessageBox.Show("Errore durante l'upload del file", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // Estensione per allegati
    public partial class InstrumentalExam
    {
        public bool HasAttachments { get; set; }
    }
}
