using System;
using System.Windows;
using WPF.Hospital.Model;   // <-- siguraduhin na ito lang ang gamit
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class AddPrescription : Window
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IHistoryService _historyService;
        private readonly IMedicineService _medicineService;
        private Prescription _editingPrescription; 

        public event Action PrescriptionSaved;

        public AddPrescription(IPrescriptionService prescriptionService,
                               IHistoryService historyService,
                               IMedicineService medicineService)
        {
            InitializeComponent();
            _prescriptionService = prescriptionService;
            _historyService = historyService;
            _medicineService = medicineService;

            LoadHistories();
            LoadMedicines();
        }

        private void LoadHistories()
        {
            cbHistory.ItemsSource = _historyService.GetAll();
            cbHistory.DisplayMemberPath = "Procedure";
            cbHistory.SelectedValuePath = "Id";
        }

        private void LoadMedicines()
        {
            cbMedicine.ItemsSource = _medicineService.GetAll();
            cbMedicine.DisplayMemberPath = "Brand";
            cbMedicine.SelectedValuePath = "Id";
        }

        public void LoadPrescription(Prescription prescription)
        {
            _editingPrescription = prescription;

            cbHistory.SelectedValue = prescription.HistoryId;
            cbMedicine.SelectedValue = prescription.MedicineId;
            tbQuantity.Text = prescription.Quantity.ToString();
            tbFrequency.Text = prescription.Frequency;

            Title = "Update Prescription";
            btnSavePrescription.Content = "Save Changes";
        }

        private void btnSavePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (cbHistory.SelectedValue == null || cbMedicine.SelectedValue == null ||
                string.IsNullOrWhiteSpace(tbQuantity.Text) || string.IsNullOrWhiteSpace(tbFrequency.Text))
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (!int.TryParse(tbQuantity.Text, out int quantity))
            {
                MessageBox.Show("Quantity must be a number.");
                return;
            }

            int historyId = (int)cbHistory.SelectedValue;
            int medicineId = (int)cbMedicine.SelectedValue;
            string frequency = tbFrequency.Text.Trim();

            try
            {
                if (_editingPrescription != null)
                {
                    var modelPrescription = new Prescription
                    {
                        Id = _editingPrescription.Id,
                        HistoryId = historyId,
                        MedicineId = medicineId,
                        Quantity = quantity,
                        Frequency = frequency
                    };

                    var result = _prescriptionService.Update(modelPrescription);
                    if (!result.Ok)
                    {
                        MessageBox.Show(result.Message);
                        return;
                    }

                    MessageBox.Show("Prescription updated successfully!");
                }
                else
                {
                    var modelPrescription = new Prescription
                    {
                        HistoryId = historyId,
                        MedicineId = medicineId,
                        Quantity = quantity,
                        Frequency = frequency
                    };

                    var result = _prescriptionService.Create(modelPrescription);
                    if (!result.Ok)
                    {
                        MessageBox.Show(result.Message);
                        return;
                    }

                    MessageBox.Show("Prescription added successfully!");
                }

                PrescriptionSaved?.Invoke();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving prescription: {ex.Message}");
            }
        }
    }
}
