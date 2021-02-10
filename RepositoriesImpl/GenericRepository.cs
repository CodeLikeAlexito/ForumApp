using Forum.Data;
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

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(int Id)
        {
            return table.Find(Id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
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
