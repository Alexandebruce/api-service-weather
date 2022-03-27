using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ApiWeather.Dao.Interfaces;
using ApiWeather.Mapping;
using ApiWeather.Models;
using ApiWeather.Services.Interfaces;
using MongoDB.Bson;

namespace ApiWeather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMongoContext mongoContext;

        public WeatherService(IMongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public async Task<List<DbBaseElement>> GetWeather(string startDate, string endDate)
        {
            var dateFrom = Convert.ToDateTime(DateTime.Parse(startDate).ToUniversalTime().ToString("O"), CultureInfo.InvariantCulture);
            var dateTo = Convert.ToDateTime(DateTime.Parse(endDate).ToUniversalTime().ToString("O"), CultureInfo.InvariantCulture);

            var filter = new BsonDocument("$and", new BsonArray{
                 
                new BsonDocument("Date",new BsonDocument("$gte", dateFrom)),
                new BsonDocument("Date", new BsonDocument("$lte", dateTo))
            });
            
            return await mongoContext.ListOrEmpty<DbBaseElement>(filter);
        }
        
        public async Task<CityWeather> GetConcreteWeather(string targetDate, string city)
        {
            var targetDateDateTime = DateTime.Parse(targetDate);
            
            var startTime = new DateTime(targetDateDateTime.Year, targetDateDateTime.Month, targetDateDateTime.Day, 0, 0,0).AddDays(-10);
            var endTime = new DateTime(targetDateDateTime.Year, targetDateDateTime.Month, targetDateDateTime.Day, 23, 59,59);
            
            var dateFrom = Convert.ToDateTime(startTime.ToUniversalTime().ToString("O"), CultureInfo.InvariantCulture);
            var dateTo = Convert.ToDateTime(endTime.ToUniversalTime().ToString("O"), CultureInfo.InvariantCulture);

            var filter = new BsonDocument("$and", new BsonArray{
                new BsonDocument("Date",new BsonDocument("$gte", dateFrom)),
                new BsonDocument("Date", new BsonDocument("$lte", dateTo))
            });
            
            var sort = new { Date = -1 };
            
            var sortedWeatherList = await mongoContext.SortedListOrEmpty<DbBaseElement>(filter, sort.ToBsonDocument(), 10);

            var dbLastSortedWeatherRecord = sortedWeatherList.Select(x => x.Data)
                       .SelectMany(d => d)
                       .FirstOrDefault(x => x.CityName == city)
                   ?? new DbCityWeather();

            return dbLastSortedWeatherRecord.MapToDomain();
        }
    }
}