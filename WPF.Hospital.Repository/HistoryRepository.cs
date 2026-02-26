using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Add(History entity)
        {
            _context.History.Add(entity);
        }

        public void Delete(int id)
        {
            var history = _context.History.Find(id);
            if (history != null)
            {
                _context.History.Remove(history);
            }
        }

        public History Get(int id)
        {
            return _context.History.Find(id);
        }

        public IEnumerable<History> GetAll()
        {
            return _context.History.ToList();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(History entity)
        {
            _context.History.Update(entity);
        }

        public IEnumerable<History> GetByPatientId(int patientId)
        {
            return _context.History.Where(h => h.PatientId == patientId).ToList();
        }

        List<History> IHistoryRepository.GetAll()
        {
            throw new NotImplementedException();
        }

        public List<History> GetByPatient(int patientId)
        {
            throw new NotImplementedException();
        }

        List<History> IHistoryRepository.GetByPatientId(int patientId)
        {
            throw new NotImplementedException();
        }
    }
}
