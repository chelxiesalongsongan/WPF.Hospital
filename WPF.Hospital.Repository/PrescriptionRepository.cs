using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public Prescription? Get(int id) => _context.Prescriptions.Find(id);
        public List<Prescription> GetAll() => _context.Prescriptions.ToList();
        public List<Prescription> GetByHistory(int historyId) => _context.Prescriptions.Where(p => p.HistoryId == historyId).ToList();
        public void Add(Prescription entity)
        {
            _context.Prescriptions.Add(entity);
        }
        public void Update(Prescription entity)
        {
            _context.Prescriptions.Update(entity);
        }
        public void Delete(int id)
        {
            var prescription = _context.Prescriptions.Find(id);
           if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
            }
        }
        public int Save() => _context.SaveChanges();
    }
}
