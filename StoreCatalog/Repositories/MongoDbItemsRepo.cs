using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using StoreCatalog.Entities;

namespace StoreCatalog.Repositories
{
    public class MongoDbItemsRepo : IItemsRepository
    {
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepo(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);
        }
        public async Task<Item> CreateItemAsync(Item itemToCreate)
        {
            itemToCreate.CreatedDate = DateTimeOffset.UtcNow;
            await itemsCollection.InsertOneAsync(itemToCreate);

            return itemToCreate;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
           return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateItemAsync(Item itemToUpdate)
        {
            var filter = filterBuilder.Eq(item => item.Id, itemToUpdate.Id);
            await itemsCollection.ReplaceOneAsync(filter, itemToUpdate);
        }
    }
}