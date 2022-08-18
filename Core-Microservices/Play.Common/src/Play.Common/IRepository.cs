
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Play.Common
{
    public interface IRepository<T> where T : IEntity
    {
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Guid id);
        Task CreateAsync(T entity);
    }
}