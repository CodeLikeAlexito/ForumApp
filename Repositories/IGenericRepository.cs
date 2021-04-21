using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(int Id);
        Task<T> GetByIdAsync(int Id);
        IEnumerable<Topic> QueriedTopics(string queryString);
        void Insert(T obj);
        void Delete(int Id);
        void Save();
        Task SaveAsync();
        void Update(T obj);
    }
}
