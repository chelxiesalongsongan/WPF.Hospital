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
using WPF.Hospital.DTO;
using WPF.Hospital.Service;
using WPF.Hospital.Model;


namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for AllDoctor.xaml
    /// </summary>
    public partial class AllDoctor : Window
    {
        private readonly IDoctorService _doctorService;
        private List<Model.Doctor> _doctors;

        public AllDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorService = doctorService;
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            _doctors = _doctorService.GetAll().ToList();
            dgDoctors.ItemsSource = _doctors;
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.ToLower();

            var filtered = _doctors
                .Where(m =>
                    m.FirstName.ToLower().Contains(keyword) ||
                    m.LastName.ToLower().Contains(keyword))
                .ToList();

            dgDoctors.ItemsSource = filtered;
        }

        private void btnUpdateDoctor_Click(object sender, RoutedEventArgs e)
        {
            var selectedDoctors = dgDoctors.SelectedItem as Model.Doctor;

            if (selectedDoctors == null)
            {
                MessageBox.Show("Please select a doctor first.");
                return;
            }

            AddDoctor updateWindow = new AddDoctor(_doctorService);
            updateWindow.LoadDoctor(selectedDoctors);
            updateWindow.DoctorSaved += LoadDoctors;
            updateWindow.ShowDialog();
        }
    }
}
