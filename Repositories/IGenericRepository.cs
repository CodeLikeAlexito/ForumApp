using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        void Insert(T obj);
        void Delete(int Id);
        void Save();
        void Update(T obj);
    }
}
