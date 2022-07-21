
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Play.Catalog.Service.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
         Task<IReadOnlyCollection<IEntity>> GetAllAsync();
         Task<T> GetAsync(Guid id);
         Task CreateAsync(IEntity entity);
    }
}