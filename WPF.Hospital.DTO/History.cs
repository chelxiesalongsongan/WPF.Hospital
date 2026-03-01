using System.Collections.Generic;
using WPF.Hospital.Model; // import Model namespace

namespace WPF.Hospital.DTO
{
    public class History
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Procedure { get; set; }

        public List<PrescriptionDTO> Prescription { get; set; } // Model.Prescription
    }
}
