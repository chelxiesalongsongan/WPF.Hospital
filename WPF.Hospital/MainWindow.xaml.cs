using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPatientService _patientService;
        private readonly IMedicineService _medicineService;

        public MainWindow(IPatientService patientService, IMedicineService medicineService)
        {
            InitializeComponent();
            _patientService = patientService;
            _medicineService = medicineService; // IMPORTANT
            this.WindowState = WindowState.Maximized;
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatient addPatient = new AddPatient(_patientService);
            addPatient.ShowDialog();
        }

        private void btnAllPatients_Click(object sender, RoutedEventArgs e)
        {
            AllPatients allPatients = new AllPatients(_patientService);
            allPatients.ShowDialog();
        }

        private void btnDeletePatient_Click(Object sender, RoutedEventArgs e)
        {
            DeletePatient deletePatient = new DeletePatient(_patientService);
            deletePatient.ShowDialog();
        }

        private void btnUpdatePatient_Click(Object sender, RoutedEventArgs e)
        {
            UpdatePatients updatePatient = new UpdatePatients(_patientService);
            updatePatient.ShowDialog();
        }

        private void btnOpenAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            AddMedicine addMedWindow = new AddMedicine(_medicineService);
            addMedWindow.ShowDialog();
        }

        private void btnOpenAllMedicine_Click(object sender, RoutedEventArgs e)
        {
            AllMedicine allMedWindow = new AllMedicine(_medicineService);
            allMedWindow.ShowDialog();
        }
    }
}