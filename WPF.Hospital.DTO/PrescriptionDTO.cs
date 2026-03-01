using System.ComponentModel.DataAnnotations.Schema;

namespace WPF.Hospital.Model
{
    [Table("Prescription")] 
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public int HistoryId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        public string Frequency { get; set; }
    }
}
