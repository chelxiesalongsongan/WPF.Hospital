using System;
using System.Windows;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class DeletePrescription : Window
    {
        private readonly IPrescriptionService _prescriptionService;

        public DeletePrescription(IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _prescriptionService = prescriptionService;
        }

        private void DeletePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPrescriptionId.Text))
            {
                MessageBox.Show("Please enter a Prescription ID.");
                return;
            }

            if (!int.TryParse(tbPrescriptionId.Text, out int prescriptionId))
            {
                MessageBox.Show("Prescription ID must be a number.");
                return;
            }

            try
            {
                var result = _prescriptionService.Delete(prescriptionId);
                if (result.Ok)
                {
                    MessageBox.Show("Prescription deleted successfully!");
                    Close();
                }
                else
                {
                    MessageBox.Show(result.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting prescription: {ex.Message}");
            }
        }
    }
}
