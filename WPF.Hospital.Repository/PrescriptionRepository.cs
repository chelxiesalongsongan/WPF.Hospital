using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly HospitaDbContext _context;

        public PrescriptionRepository(HospitaDbContext context)
        {
            _context = context;
        }

        public List<Prescription> GetAll()
        {
            return _context.Prescriptions.ToList();
        }


        public Prescription? Get(int id)
        {
            return _context.Prescriptions.Find(id);
        }


        public List<Prescription> GetByHistory(int historyId)
        {
            return _context.Prescriptions
                           .Where(p => p.HistoryId == historyId)
                           .ToList();
        }


        public void Add(Prescription entity)
        {
            _context.Prescriptions.Add(entity);
            _context.SaveChanges(); 
        }


        public void Update(Prescription entity)
        {
            _context.Prescriptions.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var prescription = _context.Prescriptions.Find(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                _context.SaveChanges();
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}