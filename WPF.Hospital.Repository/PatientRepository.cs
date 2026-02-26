using System;
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

        public Patients Get(int id)
        {
            return _context.Patients.Find(id);
        }

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

        public int Save()
        {
            return _context.SaveChanges();
        }

        
    }
}