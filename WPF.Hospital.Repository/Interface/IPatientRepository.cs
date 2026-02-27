using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public interface IPatientRepository
    {
        List<Patients> GetAll();
        Patients? Get(int id);
        void Add(Patients entity);
        void Update(Patients entity);
        void Delete(int id);
        int Save();
    }
}
