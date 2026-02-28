using System.Collections.Generic;
using System.Linq;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly HospitaDbContext _context;

        public MedicineRepository(HospitaDbContext context)
        {
            _context = context;
        }

        public List<Medicine> GetAll() => _context.Medicines.ToList();
        public Medicine? Get(int id) => _context.Medicines.Find(id);
        public void Add(Medicine entity)
        {
            _context.Medicines.Add(entity);
            _context.SaveChanges();
        }
        public void Update(Medicine entity)
        {
            _context.Medicines.Update(entity);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var entity = _context.Medicines.Find(id);
            if (entity != null)
            {
                _context.Medicines.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}