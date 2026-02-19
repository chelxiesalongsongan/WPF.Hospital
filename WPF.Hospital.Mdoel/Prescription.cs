using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Hospital.Model
{
    public class Prescription
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public History History { get; set; }
        public int MedicineI { get; set; }
        public Medicine Medicine { get; set; }
        public string Frequency { get; set; }
    }
}
