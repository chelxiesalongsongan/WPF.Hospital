using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for DeleteMedicine.xaml
    /// </summary>
    public partial class DeleteMedicine : Window
    {
        private readonly IMedicineService _medicineService;

        public DeleteMedicine(IMedicineService medicineService)
        {
            InitializeComponent();
            _medicineService = medicineService;
        }

        private void DeleteMedicine_Click(Object sender, RoutedEventArgs e)
        {
            _medicineService.Delete(Convert.ToInt32(tbMedicineId.Text));
            MessageBox.Show("Medicine deleted successfully");
        }
    }
}
