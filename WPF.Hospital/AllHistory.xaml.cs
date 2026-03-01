using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    public partial class AllHistory : Window
    {
        private readonly IHistoryService _historyService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private List<HistoryViewModel> _histories;

        // Constructor with 3 arguments (tugma sa MainWindow)
        public AllHistory(IHistoryService historyService, IPatientService patientService, IDoctorService doctorService)
        {
            InitializeComponent();
            _historyService = historyService;
            _patientService = patientService;
            _doctorService = doctorService;

            LoadHistories();
        }

        private void LoadHistories()
        {
            // Gumamit ng DTO-based GetAll
            var historyList = _historyService.GetAll();
            _histories = historyList.Select(h =>
            {
                var patient = _patientService.Get(h.PatientId);
                var doctor = _doctorService.Get(h.DoctorId);

                return new HistoryViewModel
                {
                    Id = h.Id,
                    PatientName = patient != null ? $"{patient.FirstName} {patient.LastName}" : "Unknown",
                    DoctorName = doctor != null ? $"{doctor.FirstName} {doctor.LastName}" : "Unknown",
                    Procedure = h.Procedure
                };
            }).ToList();

            dgHistory.ItemsSource = _histories;
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            var filtered = _histories
                .Where(h =>
                    h.PatientName.ToLower().Contains(keyword) ||
                    h.DoctorName.ToLower().Contains(keyword) ||
                    h.Procedure.ToLower().Contains(keyword))
                .ToList();

            dgHistory.ItemsSource = filtered;
        }

        private void btnUpdateHistory_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgHistory.SelectedItem as HistoryViewModel;
            if (selected == null)
            {
                MessageBox.Show("Please select a history first.");
                return;
            }

            var historyDto = _historyService.Get(selected.Id);
            if (historyDto == null)
            {
                MessageBox.Show("History not found.");
                return;
            }

            var updateWindow = new AddHistory(_historyService, _patientService, _doctorService);

            updateWindow.LoadHistory(historyDto);
            updateWindow.HistorySaved += LoadHistories;
            updateWindow.ShowDialog();
        }
    }
}
