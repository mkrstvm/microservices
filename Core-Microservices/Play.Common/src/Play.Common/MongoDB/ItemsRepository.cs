
using System;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Play.Common.MongoDB
{
    public class ItemsRepository<T> : IRepository<T> where T : IEntity
    {
        private const string collectionName = "items"; //table name

        private readonly IMongoCollection<T> dbCollection;

        private readonly FilterDefinitionBuilder<T> filter = Builders<T>.Filter;

        public ItemsRepository(IMongoDatabase database)        
        {            
            // var mongoCLient = new MongoClient("mongodb://localhost:27017");
            // var database  = mongoCLient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllAsync()
        {
            return await dbCollection.Find(filter.Empty).ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            FilterDefinition<T> filt = filter.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filt).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await dbCollection.InsertOneAsync(entity);
        }

    }
}