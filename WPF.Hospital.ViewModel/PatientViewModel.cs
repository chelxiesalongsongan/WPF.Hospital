using System;

namespace WPF.Hospital.ViewModel
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public DateTime? Birthdate { get; set; }

        // String property for DataGrid display
        public string BirthdateString => Birthdate.HasValue ? Birthdate.Value.ToShortDateString() : "";
    }
}