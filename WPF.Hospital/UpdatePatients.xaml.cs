using System;
using System.Windows;
using WPF.Hospital.DTO;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    public partial class UpdatePatients : Window
    {
        private readonly IPatientService _patientService;
        private Patient _editingPatient;

        public UpdatePatients(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
        }

        public void LoadPatient(Patient patient)
        {
            _editingPatient = patient;
            DataContext = new PatientViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age.ToString(),
                Birthdate = patient.Birthdate
            };
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var vm = (PatientViewModel)DataContext;

            _editingPatient.FirstName = vm.FirstName;
            _editingPatient.LastName = vm.LastName;
            _editingPatient.Age = int.Parse(vm.Age);
            _editingPatient.Birthdate = vm.Birthdate;

            var result = _patientService.Update(_editingPatient);

            if (result.Ok)
            {
                MessageBox.Show("Patient updated successfully!");
                this.Close();
            }
            else
            {
                MessageBox.Show($"Error updating patient: {result.Message}");
            }
        }
    }
}