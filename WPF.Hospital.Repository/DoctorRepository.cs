using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitaDbContext _context;

        public DoctorRepository(HospitaDbContext context)
        {
            _context = context;
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public Doctor? Get(int id)
        {
            return _context.Doctors.Find(id);
        }

        public void Add(Doctor entity)
        {
            _context.Doctors.Add(entity);
            _context.SaveChanges(); 
        }

        public void Update(Doctor entity)
        {
            _context.Doctors.Update(entity);
            _context.SaveChanges(); 
        }

        public void Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}