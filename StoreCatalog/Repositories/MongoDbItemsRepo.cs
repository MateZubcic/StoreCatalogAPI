using System;
using System.Collections.Generic;
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
        public Item CreateItemAsync(Item itemToCreate)
        {
            itemToCreate.CreatedDate = DateTimeOffset.UtcNow;
            itemsCollection.InsertOne(itemToCreate);

            return itemToCreate;
        }

        public void DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        public Item GetItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItemsAsync()
        {
           return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItemAsync(Item itemToUpdate)
        {
            var filter = filterBuilder.Eq(item => item.Id, itemToUpdate.Id);
            itemsCollection.ReplaceOne(filter, itemToUpdate);
        }
    }
}