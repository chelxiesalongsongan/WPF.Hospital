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

    
            if (!string.IsNullOrEmpty(searchTerm))
            {
                patients = patients.Where(p =>
                    p.FirstName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
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

        // TextChanged event handler for Search functionality
        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();  // Get the search term
            LoadPatients(searchTerm);  // Re-load patients with filtered list based on search term
        }

        // Update button click event
        private void btnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            if (dgPatients.SelectedItem is PatientViewModel selected)
            {
                // Convert selected ViewModel to DTO (Patient DTO)
                var patientDto = new Patient
                {
                    Id = selected.Id,
                    FirstName = selected.FirstName,
                    LastName = selected.LastName,
                    Age = int.Parse(selected.Age),
                    Birthdate = selected.Birthdate
                };

                // Open AddPatient window for editing the selected patient
                var updateWindow = new AddPatient(_patientService);

                // Pre-fill patient data in the AddPatient window for updating
                updateWindow.LoadPatient(patientDto); // This method will set the button to Save Changes

                // Show the update window
                updateWindow.ShowDialog();

                // Refresh the DataGrid after update
                LoadPatients(txtSearch.Text);  // Retain the search filter after update
            }
            else
            {
                MessageBox.Show("Please select a patient to update.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}