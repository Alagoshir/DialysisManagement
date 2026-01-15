using DialysisManagement.Models;
using DialysisManagement.Services;
using DialysisManagement.Utilities;
using iText.StyledXmlParser.Jsoup.Nodes;
using NLog;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialysisManagement.Views
{
    public partial class PatientDetailForm : Form
    {
        private readonly IPatientService _patientService;
        private readonly IVascularAccessService _vascularAccessService;
        private readonly IValidationHelper _validationHelper;
        private readonly Logger _logger;
        private int? _patientId;
        private Patient _currentPatient;

        public PatientDetailForm(
            IPatientService patientService,
            IVascularAccessService vascularAccessService,
            IValidationHelper validationHelper)
        {
            InitializeComponent();

            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _vascularAccessService = vascularAccessService ?? throw new ArgumentNullException(nameof(vascularAccessService));
            _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
            _logger = LogManager.GetCurrentClassLogger();

            InitializeCustomControls();
        }

        public PatientDetailForm(int patientId,
            IPatientService patientService,
            IVascularAccessService vascularAccessService,
            IValidationHelper validationHelper) : this(patientService, vascularAccessService, validationHelper)
        {
            _patientId = patientId;
            LoadPatientDataAsync();
        }

        private void InitializeCustomControls()
        {
            // Imposta eventi
            btnSave.Click += async (s, e) => await SavePatientAsync();
            btnCancel.Click += (s, e) => this.Close();
            btnCalcolaEta.Click += CalcolaEta_Click;
            btnCalcolaBmi.Click += CalcolaBmi_Click;

            // Popola ComboBox
            cboSesso.Items.AddRange(new object[] { "M", "F" });
            cboGruppoSanguigno.Items.AddRange(new object[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "0+", "0-" });
            cboStato.Items.AddRange(new object[] { "attivo", "sospeso", "deceduto", "trapiantato" });
            cboStato.SelectedIndex = 0;

            // Validazioni in tempo reale
            txtCodiceFiscale.Leave += TxtCodiceFiscale_Leave;
            txtEmail.Leave += TxtEmail_Leave;
        }

        private async Task LoadPatientDataAsync()
        {
            try
            {
                if (_patientId.HasValue)
                {
                    _currentPatient = await _patientService.GetPatientByIdAsync(_patientId.Value);

                    if (_currentPatient != null)
                    {
                        PopulateFormData(_currentPatient);
                        this.Text = $"Modifica Paziente - {_currentPatient.Nome} {_currentPatient.Cognome}";
                        btnSave.Text = "💾 Aggiorna";
                    }
                }
                else
                {
                    this.Text = "Nuovo Paziente";
                    btnSave.Text = "💾 Salva";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore caricamento dati paziente");
                MessageBox.Show("Errore nel caricamento dei dati", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFormData(Patient patient)
        {
            txtCodiceFiscale.Text = patient.CodiceFiscale;
            txtNome.Text = patient.Nome;
            txtCognome.Text = patient.Cognome;
            dtpDataNascita.Value = patient.DataNascita;
            cboSesso.SelectedItem = patient.Sesso;
            txtLuogoNascita.Text = patient.LuogoNascita;

            txtIndirizzo.Text = patient.Indirizzo;
            txtCap.Text = patient.Cap;
            txtCitta.Text = patient.Citta;
            txtProvincia.Text = patient.Provincia;
            txtTelefono.Text = patient.Telefono;
            txtCellulare.Text = patient.Cellulare;
            txtEmail.Text = patient.Email;

            cboGruppoSanguigno.SelectedItem = patient.GruppoSanguigno;
            numPesoSecco.Value = patient.PesoSecco ?? 0;
            numAltezza.Value = patient.Altezza ?? 0;
            numBmi.Value = patient.Bmi ?? 0;

            chkHbsagPositivo.Checked = patient.HbsagPositivo;
            chkHcvPositivo.Checked = patient.HcvPositivo;
            chkHivPositivo.Checked = patient.HivPositivo;
            chkInListaTrapianto.Checked = patient.InListaTrapianto;

            cboStato.SelectedItem = patient.Stato;
            txtNote.Text = patient.Note;
        }

        private Patient GetPatientFromForm()
        {
            var patient = _currentPatient ?? new Patient();

            patient.CodiceFiscale = txtCodiceFiscale.Text.Trim().ToUpperInvariant();
            patient.Nome = txtNome.Text.Trim();
            patient.Cognome = txtCognome.Text.Trim();
            patient.DataNascita = dtpDataNascita.Value.Date;
            patient.Sesso = cboSesso.SelectedItem?.ToString();
            patient.LuogoNascita = txtLuogoNascita.Text.Trim();

            patient.Indirizzo = txtIndirizzo.Text.Trim();
            patient.Cap = txtCap.Text.Trim();
            patient.Citta = txtCitta.Text.Trim();
            patient.Provincia = txtProvincia.Text.Trim();
            patient.Telefono = txtTelefono.Text.Trim();
            patient.Cellulare = txtCellulare.Text.Trim();
            patient.Email = txtEmail.Text.Trim();

            patient.GruppoSanguigno = cboGruppoSanguigno.SelectedItem?.ToString();
            patient.PesoSecco = numPesoSecco.Value > 0 ? numPesoSecco.Value : (decimal?)null;
            patient.Altezza = numAltezza.Value > 0 ? (int)numAltezza.Value : (int?)null;
            patient.Bmi = numBmi.Value > 0 ? numBmi.Value : (decimal?)null;

            patient.HbsagPositivo = chkHbsagPositivo.Checked;
            patient.HcvPositivo = chkHcvPositivo.Checked;
            patient.HivPositivo = chkHivPositivo.Checked;
            patient.InListaTrapianto = chkInListaTrapianto.Checked;

            patient.Stato = cboStato.SelectedItem?.ToString() ?? "attivo";
            patient.Note = txtNote.Text.Trim();

            return patient;
        }

        private async Task SavePatientAsync()
        {
            try
            {
                var patient = GetPatientFromForm();

                // Validazione
                if (!await _patientService.ValidatePatientDataAsync(patient))
                {
                    MessageBox.Show("Verificare i dati inseriti", "Validazione",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnSave.Enabled = false;
                btnSave.Text = "Salvataggio...";

                if (_patientId.HasValue)
                {
                    // Aggiorna paziente esistente
                    patient.PatientId = _patientId.Value;
                    var (success, errorMessage) = await _patientService.UpdatePatientAsync(patient);

                    if (success)
                    {
                        MessageBox.Show("Paziente aggiornato con successo", "Successo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage ?? "Errore durante l'aggiornamento", "Errore",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Crea nuovo paziente
                    var (success, patientId, errorMessage) = await _patientService.CreatePatientAsync(patient);

                    if (success)
                    {
                        MessageBox.Show("Paziente creato con successo", "Successo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(errorMessage ?? "Errore durante la creazione", "Errore",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Errore salvataggio paziente");
                MessageBox.Show("Errore durante il salvataggio", "Errore",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = _patientId.HasValue ? "💾 Aggiorna" : "💾 Salva";
            }
        }

        private void TxtCodiceFiscale_Leave(object sender, EventArgs e)
        {
            var cf = txtCodiceFiscale.Text.Trim();
            if (!string.IsNullOrEmpty(cf))
            {
                if (!_validationHelper.ValidateCodiceFiscale(cf))
                {
                    lblCfError.Text = "⚠ Codice fiscale non valido";
                    lblCfError.ForeColor = Color.Red;
                    lblCfError.Visible = true;
                }
                else
                {
                    lblCfError.Text = "✓ Valido";
                    lblCfError.ForeColor = Color.Green;
                    lblCfError.Visible = true;
                }
            }
        }

        private void TxtEmail_Leave(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email))
            {
                if (!_validationHelper.ValidateEmail(email))
                {
                    lblEmailError.Text = "⚠ Email non valida";
                    lblEmailError.ForeColor = Color.Red;
                    lblEmailError.Visible = true;
                }
                else
                {
                    lblEmailError.Visible = false;
                }
            }
        }

        private void CalcolaEta_Click(object sender, EventArgs e)
        {
            var age = DateTime.Today.Year - dtpDataNascita.Value.Year;
            if (dtpDataNascita.Value.Date > DateTime.Today.AddYears(-age))
                age--;

            lblEta.Text = $"Età: {age} anni";
            lblEta.Visible = true;
        }

        private void CalcolaBmi_Click(object sender, EventArgs e)
        {
            if (numPesoSecco.Value > 0 && numAltezza.Value > 0)
            {
                var peso = numPesoSecco.Value;
                var altezzaM = numAltezza.Value / 100;
                var bmi = peso / (altezzaM * altezzaM);

                numBmi.Value = Math.Round(bmi, 2);

                string categoria = bmi switch
                {
                    < 18.5m => "Sottopeso",
                    < 25m => "Normopeso",
                    < 30m => "Sovrappeso",
                    _ => "Obesità"
                };

                lblBmiCategory.Text = $"Categoria: {categoria}";
                lblBmiCategory.Visible = true;
            }
        }
    }
}
