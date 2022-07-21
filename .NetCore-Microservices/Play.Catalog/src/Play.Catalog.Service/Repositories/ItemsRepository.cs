
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using Play.Catalog.Service.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Play.Catalog.Service.Repositories
{
    public class ItemsRepository<T> : IRepository<T> where T : IEntity
    {
        private const string collectionName = "items"; //table name

        private readonly IMongoCollection<Item> dbCollection;

        private readonly FilterDefinitionBuilder<Item> filter = Builders<Item>.Filter;

        public ItemsRepository(IMongoDatabase database)        
        {            
            // var mongoCLient = new MongoClient("mongodb://localhost:27017");
            // var database  = mongoCLient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Item>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filter.Empty).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<Item> filt = filter.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filt).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await dbCollection.InsertOneAsync(entity);
        }

    }
}