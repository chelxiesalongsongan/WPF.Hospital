using System;
using System.Windows;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class DeletePatient : Window
    {
        private readonly IPatientService _patientService;

        public DeletePatient(IPatientService patientService)
        {
            InitializeComponent();
            _patientService = patientService;
        }

        private void Delete_Click(Object sender, RoutedEventArgs e)
        {
            _patientService.Delete(Convert.ToInt32(tbPatientId.Text));
            MessageBox.Show("Patient deleted successfully");
        }
    }
}