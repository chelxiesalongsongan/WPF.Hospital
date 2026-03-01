using System.Collections.Generic;
using WPF.Hospital.Model; 

namespace WPF.Hospital.DTO
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public List<PrescriptionDTO> Prescriptions { get; set; } 
    }
}
