using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DialysisManagement.Models;
using DialysisManagement.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DialysisManagement.Views
{
    /// <summary>
    /// Form lista pazienti
    /// </summary>
    public partial class PatientListForm : Form
    {
        private readonly IPatientService _patientService;
        private List<Patient> _patients;

        public PatientListForm(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));

            InitializeComponent(); // <-- Chiamata OK, ma NON definizione

            LoadPatients();
        }

        // ⚠️ NON DEVE ESSERCI QUESTO METODO QUI:
        // private void InitializeComponent() { ... }  <-- RIMUOVERE SE PRESENTE!

        private async void LoadPatients()
        {
            try
            {
                _patients = (await _patientService.GetAllPatientsAsync()).ToList();
                dgvPatients.DataSource = _patients;
                ConfigureGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGrid()
        {
            // Configurazione colonne DataGridView
            dgvPatients.AutoGenerateColumns = false;
            dgvPatients.Columns.Clear();

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PatientId",
                HeaderText = "ID",
                DataPropertyName = "PatientId",
                Width = 50,
                Visible = false
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CodiceFiscale",
                HeaderText = "Codice Fiscale",
                DataPropertyName = "CodiceFiscale",
                Width = 150
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cognome",
                HeaderText = "Cognome",
                DataPropertyName = "Cognome",
                Width = 150
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                DataPropertyName = "Nome",
                Width = 150
            });

            dgvPatients.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataNascita",
                HeaderText = "Data Nascita",
                DataPropertyName = "DataNascita",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                Width = 120
            });

            dgvPatients.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                HeaderText = "Attivo",
                DataPropertyName = "IsActive",
                Width = 70
            });
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = Program.ServiceProvider.GetRequiredService<PatientDetailForm>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPatients();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null)
            {
                MessageBox.Show("Selezionare un paziente", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var patient = dgvPatients.CurrentRow.DataBoundItem as Patient;
            if (patient != null)
            {
                var form = Program.ServiceProvider.GetRequiredService<PatientDetailForm>();
                // TODO: Passa patient ID al form
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPatients.CurrentRow == null)
            {
                MessageBox.Show("Selezionare un paziente", "Avviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var patient = dgvPatients.CurrentRow.DataBoundItem as Patient;
            if (patient != null)
            {
                var result = MessageBox.Show(
                    $"Disattivare il paziente {patient.Cognome} {patient.Nome}?",
                    "Conferma",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _patientService.DeletePatientAsync(patient.PatientId);
                        MessageBox.Show("Paziente disattivato", "Successo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatients();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Errore: {ex.Message}", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvPatients.DataSource = _patients;
                return;
            }

            var filtered = _patients.Where(p =>
                p.Cognome.ToLower().Contains(searchText) ||
                p.Nome.ToLower().Contains(searchText) ||
                p.CodiceFiscale.ToLower().Contains(searchText)
            ).ToList();

            dgvPatients.DataSource = filtered;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPatients();
        }
    }
}
