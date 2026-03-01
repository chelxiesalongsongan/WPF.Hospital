using System;
using System.Windows;
using WPF.Hospital.DTO;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class AddHistory : Window
    {
        private readonly IHistoryService _historyService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;

        private History _editingHistory;

        public event Action HistorySaved;

        public AddHistory(IHistoryService historyService, IPatientService patientService, IDoctorService doctorService)
        {
            InitializeComponent();

            _historyService = historyService ?? throw new ArgumentNullException(nameof(historyService));
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));

            LoadPatients();
            LoadDoctors();
        }

        private void LoadPatients()
        {
            cbPatient.ItemsSource = _patientService.GetAll()
                .Select(p => new { p.Id, Name = $"{p.FirstName} {p.LastName}" })
                .ToList();
            cbPatient.DisplayMemberPath = "Name";
            cbPatient.SelectedValuePath = "Id";
        }

        private void LoadDoctors()
        {
            cbDoctor.ItemsSource = _doctorService.GetAll()
                .Select(d => new { d.Id, Name = $"{d.FirstName} {d.LastName}" })
                .ToList();
            cbDoctor.DisplayMemberPath = "Name";
            cbDoctor.SelectedValuePath = "Id";
        }

        public void LoadHistory(History history)
        {
            if (history == null) return;

            _editingHistory = history;
            cbPatient.SelectedValue = history.PatientId;
            cbDoctor.SelectedValue = history.DoctorId;
            tbProcedure.Text = history.Procedure;

            Title = "Update History";
            btnSaveHistory.Content = "Save Changes";
        }

        private void btnSaveHistory_Click(object sender, RoutedEventArgs e)
        {
            if (cbPatient.SelectedValue == null || cbDoctor.SelectedValue == null || string.IsNullOrWhiteSpace(tbProcedure.Text))
            {
                MessageBox.Show("Please select patient, doctor, and enter procedure.");
                return;
            }

            int patientId = (int)cbPatient.SelectedValue;
            int doctorId = (int)cbDoctor.SelectedValue;
            string procedure = tbProcedure.Text.Trim();

            if (_editingHistory != null)
            {
                // Map Model to DTO
                var dtoHistory = new DTO.History
                {
                    Id = _editingHistory.Id,
                    PatientId = patientId,
                    DoctorId = doctorId,
                    Procedure = procedure
                };

                var result = _historyService.Update(dtoHistory);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("History updated successfully!");
            }
            else
            {
                var newHistory = new DTO.History
                {
                    PatientId = patientId,
                    DoctorId = doctorId,
                    Procedure = procedure
                };

                var result = _historyService.Create(newHistory);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("History added successfully!");
            }

            HistorySaved?.Invoke();
            Close();
        }
    }
}