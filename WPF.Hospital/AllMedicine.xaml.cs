using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPF.Hospital.Model;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    public partial class AllMedicine : Window
    {
        private readonly IMedicineService _medicineService;
        private List<Medicine> _medicines;

        public AllMedicine(IMedicineService medicineService)
        {
            InitializeComponent();
            _medicineService = medicineService;
            LoadMedicines();
        }

        private void LoadMedicines()
        {
            _medicines = _medicineService.GetAll().ToList();
            dgMedicines.ItemsSource = _medicines;
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            var filtered = _medicines
                .Where(m =>
                    m.Name.ToLower().Contains(keyword) ||
                    m.Brand.ToLower().Contains(keyword))
                .ToList();

            dgMedicines.ItemsSource = filtered;
        }

        private void btnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            var selectedMedicine = dgMedicines.SelectedItem as Medicine;

            if (selectedMedicine == null)
            {
                MessageBox.Show("Please select a medicine first.");
                return;
            }

            AddMedicine updateWindow = new AddMedicine(_medicineService);
            updateWindow.LoadMedicine(selectedMedicine);
            updateWindow.MedicineSaved += LoadMedicines;
            updateWindow.ShowDialog();
        }
    }
}