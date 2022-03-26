using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiWeather.Dao.Interfaces
{
    public interface IMongoContext
    {
        Task<List<T>> ListOrEmpty<T>(string filter);
    }
}