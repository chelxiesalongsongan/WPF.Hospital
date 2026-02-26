using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.Hospital.Model;
using WPF.Hospital.Repository;

namespace WPF.Hospital.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitaDbContext _context;
        public PatientRepository(HospitaDbContext context)
        {
            _context = context;
        }
        
        public Patients Get(int id) => _context.Patients.Find(id);
        public IEnumerable<Patients> GetAll() => _context.Patients.ToList();

        public void Add(Patients entity)
        {
            _context.Patients.Add(entity);
        }

        public void Update(Patients entity)
        {
            _context.Patients.Update(entity);
        }

        public void Delete(int id)
        {
            var patient = _context.Patients.Find(id);
           if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
        }

        public int Save() => _context.SaveChanges();

        List<Patients> IPatientRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
