using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAW_Project.Context;
using DAW_Project.Interfaces;

namespace DAW_Project.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            
            _context.Set<T>().Remove(entity);
        }
    }
}
