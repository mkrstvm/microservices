
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Play.Common
{
    public interface IRepository<T> where T : IEntity
    {
         Task<IReadOnlyCollection<T>> GetAllAsync();
         Task<T> GetAsync(Guid id);
         Task CreateAsync(T entity);
    }
}