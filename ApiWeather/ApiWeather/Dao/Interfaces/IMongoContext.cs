using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ApiWeather.Dao.Interfaces
{
    public interface IMongoContext
    {
        Task<List<T>> ListOrEmpty<T>(BsonDocument filter);
        Task<List<T>> SortedListOrEmpty<T>(BsonDocument filter, BsonDocument sort, int limit);
    }
}