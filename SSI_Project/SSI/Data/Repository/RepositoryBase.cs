using Microsoft.EntityFrameworkCore;
using SSI.Data.IRepository;
using SSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Data.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly SSIV2Context _context;
        protected DbSet<T> _dbset;

        public RepositoryBase(SSIV2Context context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
    }
}
