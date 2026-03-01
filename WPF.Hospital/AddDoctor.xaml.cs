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
using WPF.Hospital.ViewModel;
using WPF.Hospital.Model;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for AddDoctor.xaml
    /// </summary>
    public partial class AddDoctor : Window
    {
        private readonly IDoctorService _doctorservice;
        private Model.Doctor _editingDoctor;

        public event Action DoctorSaved;

        public AddDoctor(IDoctorService doctorService)
        {
            InitializeComponent();
            _doctorservice = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
            DataContext = new DoctorViewModel();
        }

        public void LoadDoctor(Model.Doctor doctor)
        {
            _editingDoctor = doctor;
            DataContext = new DoctorViewModel
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };
            Title = "Update Doctor";
            btnAddDoctor.Content = "Save Changes";
        }

        private void btnAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as DoctorViewModel;
            if (vm == null) return;

            if (string.IsNullOrWhiteSpace(vm.FirstName) || string.IsNullOrWhiteSpace(vm.LastName))
            {
                MessageBox.Show("Please enter both Firstname and Lastname.");
                return;
            }

            if (_editingDoctor != null)
            {
                _editingDoctor.FirstName = vm.FirstName;
                _editingDoctor.LastName = vm.LastName;

                var result = _doctorservice.Update(_editingDoctor);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("Doctor updated successfully!");
            }
            else
            {
                var newDoctor = new Model.Doctor
                {
                    FirstName = vm.FirstName,
                    LastName = vm.LastName
                };

                var result = _doctorservice.Create(newDoctor);
                if (!result.Ok)
                {
                    MessageBox.Show(result.Message);
                    return;
                }

                MessageBox.Show("Doctor added successfully!");
            }

            DoctorSaved?.Invoke();
            Close();
        }
    }
}
