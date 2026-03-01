using System.Collections.Generic;

namespace WPF.Hospital.Model
{
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;


        public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
