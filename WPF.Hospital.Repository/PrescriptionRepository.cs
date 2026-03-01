using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;  
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

        public List<Prescription> GetAll() =>
            _context.Prescriptions.AsNoTracking().ToList();

        public List<Prescription> GetByHistory(int historyId) =>
            _context.Prescriptions
                    .AsNoTracking()
                    .Where(p => p.HistoryId == historyId)
                    .ToList();

        public Prescription? Get(int id) =>
            _context.Prescriptions
                    .AsNoTracking()
                    .FirstOrDefault(p => p.Id == id);

        public void Add(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            _context.SaveChanges();
        }

        public void Update(Prescription prescription)
        {
  
            _context.Prescriptions.Update(prescription);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var prescription = _context.Prescriptions.FirstOrDefault(p => p.Id == id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                _context.SaveChanges();
            }
        }
    }
}
