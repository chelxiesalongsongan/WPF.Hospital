using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly HospitaDbContext _context;

        public HistoryRepository(HospitaDbContext context)
        {
            _context = context;
        }

        public List<History> GetAll()
        {
            return _context.History.ToList();
        }

        public History? Get(int id)
        {
            return _context.History.Find(id);
        }

        
        public List<History> GetByPatientId(int patientId)
        {
            return _context.History
                           .Where(h => h.PatientId == patientId)
                           .ToList();
        }

    
        public void Add(History entity)
        {
            _context.History.Add(entity);
            _context.SaveChanges();     
        }

        public void Update(History entity)
        {
            _context.History.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var history = _context.History.Find(id);
            if (history != null)
            {
                _context.History.Remove(history);
                _context.SaveChanges();
            }
        }
        public int Save()
        {
            return _context.SaveChanges();
        }

        IEnumerable<History> IRepository<History>.GetAll()
        {
            return GetAll();
        }
    }
}