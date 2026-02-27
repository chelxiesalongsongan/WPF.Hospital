using System.Linq;
using System.Windows;
using WPF.Hospital.DTO;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    public partial class AllPatients : Window
    {
        private readonly IPatientService _patientService;

        public AllPatients(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
            LoadPatients();
        }

        private void LoadPatients(string searchTerm = "")
        {
            var patients = _patientService.GetAll();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                patients = patients
                    .Where(p => p.FirstName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var patientViewModels = patients.Select(p => new PatientViewModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age.ToString(),
                Birthdate = p.Birthdate
            }).ToList();

            dgPatients.ItemsSource = patientViewModels;
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LoadPatients(txtSearch.Text.Trim());
        }

        private void btnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientViewModel selected)
            {
                var patientDto = new Patient
                {
                    Id = selected.Id,
                    FirstName = selected.FirstName,
                    LastName = selected.LastName,
                    Age = int.TryParse(selected.Age, out int age) ? age : 0,
                    Birthdate = selected.Birthdate
                };

                var updateWindow = new AddPatient(_patientService);
                updateWindow.LoadPatient(patientDto);
                updateWindow.PatientSaved += () => LoadPatients(txtSearch.Text); // auto-refresh
                updateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a patient to update.",
                    "No Selection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }
    }
}