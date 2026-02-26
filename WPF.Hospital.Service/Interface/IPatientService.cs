using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.DTO;

namespace WPF.Hospital.Service
{
    public interface IPatientService
    {
        List<Patient> GetAll();
        Patient? Get(int id);
        (bool Ok, string Message) Create(Patient patient);
        (bool Ok, string Message) Update(Patient patient);
        (bool Ok, string Message) Delete(int id);
        void Add(Patient patient);
    }
}
