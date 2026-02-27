using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitaDbContext _context;

        public PatientRepository(HospitaDbContext context)
        {
            _context = context;
        }


        public List<Patients> GetAll()
        {
            return _context.Patients.ToList();
        }


        public Patients? Get(int id)
        {
            return _context.Patients.Find(id);
        }


        public void Add(Patients entity)
        {
            _context.Patients.Add(entity);
            _context.SaveChanges(); 
        }


        public void Update(Patients entity)
        {
            _context.Patients.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }


        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}