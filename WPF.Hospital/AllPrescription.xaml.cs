using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF.Hospital.Model;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class AllPrescription : Window
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IHistoryService _historyService;
        private readonly IMedicineService _medicineService;
        private List<Prescription> _allPrescriptions;

        public AllPrescription(IPrescriptionService prescriptionService,
                               IHistoryService historyService,
                               IMedicineService medicineService)
        {
            InitializeComponent();
            _prescriptionService = prescriptionService;
            _historyService = historyService;
            _medicineService = medicineService;

            LoadPrescriptions();
        }

        private void LoadPrescriptions()
        {
            _allPrescriptions = _prescriptionService.GetAll();
            dgPrescriptions.ItemsSource = _allPrescriptions;
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                dgPrescriptions.ItemsSource = _allPrescriptions;
            }
            else
            {
                var filtered = _allPrescriptions.Where(p =>
                    p.Id.ToString().Contains(searchText) ||
                    p.HistoryId.ToString().Contains(searchText) ||
                    p.MedicineId.ToString().Contains(searchText) ||
                    p.Quantity.ToString().Contains(searchText) ||
                    (!string.IsNullOrEmpty(p.Frequency) && p.Frequency.ToLower().Contains(searchText))
                ).ToList();

                dgPrescriptions.ItemsSource = filtered;
            }
        }

        private void btnUpdatePrescription_Click(object sender, RoutedEventArgs e)
        {
            if (dgPrescriptions.SelectedItem is Prescription selectedPrescription)
            {
                var updateWindow = new AddPrescription(_prescriptionService, _historyService, _medicineService);
                updateWindow.LoadPrescription(selectedPrescription);
                updateWindow.PrescriptionSaved += LoadPrescriptions;
                updateWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a prescription to update.");
            }
        }
    }
}
