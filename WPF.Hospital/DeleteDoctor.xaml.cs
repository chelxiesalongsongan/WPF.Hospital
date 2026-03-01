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
    /// Interaction logic for DeleteDoctor.xaml
    /// </summary>
    public partial class DeleteDoctor : Window
    {
        private readonly IDoctorService _doctorService;

        public DeleteDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorService = doctorService;
        }

        private void DeleteDoctor_Click(Object sender, RoutedEventArgs e)
        {
            _doctorService.Delete(Convert.ToInt32(tbDoctorId.Text));
            MessageBox.Show("Doctor deleted successfully");
        }
    }
}
