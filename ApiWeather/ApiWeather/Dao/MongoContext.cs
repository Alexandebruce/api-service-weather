using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWeather.Dao.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiWeather.Dao
{
    public class MongoContext : IMongoContext
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly string collectionName;

        public MongoContext(MongoSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.Database);
            collectionName = settings.Collection;
        }
        
        public async Task<List<T>> ListOrEmpty<T>(BsonDocument filter)
        {
            return await Execute<T>(query => query.Find(filter).ToListAsync());
        }
        
        public async Task<List<T>> SortedListOrEmpty<T>(BsonDocument filter, BsonDocument sort, int limit)
        { 
            return await Execute<T>(query => query.Find(filter).Sort(
                new BsonDocumentSortDefinition<T>(sort)
            ).Limit(limit).ToListAsync());
        }

        private async Task<List<T>> Execute<T>(Func<IMongoCollection<T>, Task<List<T>>> query)
        {
            IMongoCollection<T> collection = database.GetCollection<T>(collectionName);
            return await query(collection);
        }
    }
}