using Forum.Data;
using Forum.Models;
using Forum.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.RepositoriesImpl
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ForumDbContext _context;
        private DbSet<T> table = null;
        private bool disposed = false;

        public GenericRepository(DbContextOptions<ForumDbContext> dbContextOptions)
        {
            _context = new ForumDbContext(dbContextOptions);
            table = _context.Set<T>();
        }

        public void Delete(int Id)
        {
            T obj = table.Find(Id);
            table.Remove(obj);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public IEnumerable<Topic> QueriedTopics(string queryString)
        {
            return _context.Topic.Where(p => p.Title.Contains(queryString) || p.Description.Contains(queryString)).ToList();
        }

        public T GetById(int Id)
        {
            return table.Find(Id);
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await table.FindAsync(Id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
