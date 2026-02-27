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

        public List<Medicine> GetAll()
        {
            return _context.Medicines.ToList();
        }

        public Medicine? Get(int id)
        {
            return _context.Medicines.Find(id);
        }

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
            var medicine = _context.Medicines.Find(id);
            if (medicine != null)
            {
                _context.Medicines.Remove(medicine);
                _context.SaveChanges();
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}