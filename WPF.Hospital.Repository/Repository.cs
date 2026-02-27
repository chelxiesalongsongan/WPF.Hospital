using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WPF.Hospital.Model;

namespace WPF.Hospital.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly HospitaDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(HospitaDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); 
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges(); 
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}