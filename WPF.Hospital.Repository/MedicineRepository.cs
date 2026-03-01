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

        public List<Medicine> GetAll() => _context.Medicine.ToList();
        public Medicine? Get(int id) => _context.Medicine.Find(id);

        public void Add(Medicine entity)
        {
            _context.Medicine.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Medicine entity)
        {
            _context.Medicine.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Medicine.Find(id);
            if (entity != null)
            {
                _context.Medicine.Remove(entity);
                _context.SaveChanges();
            }
        }

        public int Save()
        {
            throw new NotImplementedException();
        }
    }
}