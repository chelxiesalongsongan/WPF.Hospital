using System.Collections.Generic;

namespace WPF.Hospital.Model
{
    public class Patients
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime Birthdate { get; set; }

  
        public ICollection<History> Histories { get; set; } = new List<History>();
    }
}
