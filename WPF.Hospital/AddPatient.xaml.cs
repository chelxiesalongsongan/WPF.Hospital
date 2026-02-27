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
        private Patient _editingPatient;


        public event Action PatientSaved;

        public AddPatient(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
            DataContext = new PatientViewModel();
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
            Title = "Update Patient";
            btnAddPatient.Content = "Save Changes";
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as PatientViewModel;
            if (vm == null) return;


            string firstName = vm.FirstName ?? "";
            string lastName = vm.LastName ?? "";
            int.TryParse(vm.Age, out int age);
            DateTime? birthdate = vm.Birthdate;

            if (_editingPatient != null)
            {
                
                _editingPatient.FirstName = firstName;
                _editingPatient.LastName = lastName;
                _editingPatient.Age = age;
                _editingPatient.Birthdate = birthdate ?? DateTime.MinValue;

                var result = _patientService.Update(_editingPatient);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("Patient updated successfully!");
            }
            else
            {
               
                var newPatient = new Patient
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Birthdate = birthdate ?? DateTime.MinValue
                };

                var result = _patientService.Create(newPatient);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("Patient added successfully!");
            }

            PatientSaved?.Invoke(); 
            Close();
        }
    }
}