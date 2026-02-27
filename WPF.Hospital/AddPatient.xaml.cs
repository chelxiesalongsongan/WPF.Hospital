using System;
using System.Windows;
using WPF.Hospital.DTO;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    public partial class AddPatient : Window
    {
        private readonly IPatientService _patientService;
        private Patient _editingPatient;  // This will hold the patient being updated

        public AddPatient(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
        }

        // This method will be called to load data into the window if we're updating
        public void LoadPatient(Patient patient)
        {
            _editingPatient = patient;

            // Pre-fill the fields for the update
            DataContext = new PatientViewModel
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Age = patient.Age.ToString(),
                Birthdate = patient.Birthdate
            };

            // Update the window title and button content for update mode
            this.Title = "Update Patient";
            btnAddPatient.Content = "Save Changes";  // Change button text to Save Changes
        }

        // Handle the button click for both Add and Update
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            // Convert ViewModel to DTO
            var vm = (PatientViewModel)DataContext;

            // If we are editing a patient (Update)
            if (_editingPatient != null)
            {
                _editingPatient.FirstName = vm.FirstName;
                _editingPatient.LastName = vm.LastName;
                _editingPatient.Age = int.Parse(vm.Age);
                _editingPatient.Birthdate = vm.Birthdate;

                // Update the patient via the service
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
            else
            {
                // Handle Add patient logic
                var newPatient = new Patient
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Age = int.Parse(vm.Age),
                    Birthdate = vm.Birthdate
                };

                var result = _patientService.Create(newPatient);  // Assuming Create method exists
                if (result.Ok)
                {
                    MessageBox.Show("Patient added successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Error adding patient: {result.Message}");
                }
            }
        }
    }
}