using System;
using System.Windows;
using WPF.Hospital.Model;
using WPF.Hospital.Service;
using WPF.Hospital.ViewModel;

namespace WPF.Hospital
{
    public partial class AddMedicine : Window
    {
        private readonly IMedicineService _medicineService;
        private Medicine _editingMedicine;

        public event Action MedicineSaved;

        public AddMedicine(IMedicineService medicineService)
        {
            InitializeComponent();
            _medicineService = medicineService ?? throw new ArgumentNullException(nameof(medicineService));
            DataContext = new MedicineViewModel();
        }

        public void LoadMedicine(Medicine medicine)
        {
            _editingMedicine = medicine;
            DataContext = new MedicineViewModel
            {
                Id = medicine.Id,
                Name = medicine.Name,
                Brand = medicine.Brand
            };
            Title = "Update Medicine";
            btnAddMedicine.Content = "Save Changes";
        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MedicineViewModel;
            if (vm == null) return;

            if (string.IsNullOrWhiteSpace(vm.Name) || string.IsNullOrWhiteSpace(vm.Brand))
            {
                MessageBox.Show("Please enter both Name and Brand.");
                return;
            }

            if (_editingMedicine != null)
            {
                _editingMedicine.Name = vm.Name;
                _editingMedicine.Brand = vm.Brand;

                var result = _medicineService.Update(_editingMedicine);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("Medicine updated successfully!");
            }
            else
            {
                var newMedicine = new Medicine
                {
                    Name = vm.Name,
                    Brand = vm.Brand
                };

                var result = _medicineService.Create(newMedicine);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("Medicine added successfully!");
            }

            MedicineSaved?.Invoke();
            Close();
        }
    }
}