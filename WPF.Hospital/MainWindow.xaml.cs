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
        private readonly IDoctorService _doctorService;
        private readonly IHistoryService _historyService;
        private readonly IPrescriptionService _prescriptionService;

        public MainWindow(IPatientService patientService, IMedicineService medicineService, IDoctorService doctorService, IHistoryService historyService, IPrescriptionService prescriptionService)
        {
            InitializeComponent();
            _patientService = patientService;
            _medicineService = medicineService;
            _doctorService = doctorService;
            _historyService = historyService;
            _prescriptionService = prescriptionService;
            this.WindowState = WindowState.Maximized;
            _historyService = historyService;
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

        private void btnOpenDeleteMedicine_Click(object sender, RoutedEventArgs e)
        {
            DeleteMedicine deleteMedWindow = new DeleteMedicine(_medicineService);
            deleteMedWindow.ShowDialog();
        }

        private void btnOpenAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            AddDoctor addDoctorWindow = new AddDoctor(_doctorService);
            addDoctorWindow.ShowDialog();
        }

        private void btnOpenAllDoctor_Click(object sender, RoutedEventArgs e)
        {
            AllDoctor allDoctorWindow = new AllDoctor(_doctorService);
            allDoctorWindow.ShowDialog();
        }

        private void btnOpenDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            DeleteDoctor deleteDoctorWindow = new DeleteDoctor(_doctorService);
            deleteDoctorWindow.ShowDialog();
        }

        private void btnOpenAddHistory_Click(object sender, RoutedEventArgs e)
        {
            AddHistory addHistoryWindow = new AddHistory(_historyService, _patientService, _doctorService);
            addHistoryWindow.ShowDialog();
        }

        private void btnOpenAllHistory_Click(object sender, RoutedEventArgs e)
        {
            AllHistory allHistoryWindow = new AllHistory(_historyService, _patientService, _doctorService);
            allHistoryWindow.ShowDialog();
        }

        private void btnOpenDeleteHistory_Click(object sender, RoutedEventArgs e)
        {
            DeleteHistory deleteHistoryWindow = new DeleteHistory(_historyService);
            deleteHistoryWindow.ShowDialog();
        }



        private void btnAddPrescription_Click(object sender, RoutedEventArgs e)
        {
            AddPrescription addPrescriptionWindow = new AddPrescription(
                _prescriptionService,
                _historyService, 
                _medicineService
            );
            addPrescriptionWindow.ShowDialog();
        }

        private void btnAllPrescriptions_Click(object sender, RoutedEventArgs e)
        {
            AllPrescription allPrescriptionsWindow = new AllPrescription(
                _prescriptionService,
                _historyService,
                _medicineService
            );
            allPrescriptionsWindow.ShowDialog();
        }

        private void btnDeletePrescription_Click(object sender, RoutedEventArgs e)
        {
            DeletePrescription deletePrescriptionWindow = new DeletePrescription(_prescriptionService);
            deletePrescriptionWindow.ShowDialog();
        }

    }
}