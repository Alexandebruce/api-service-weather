using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ApiWeather.Dao.Interfaces;
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

        public async Task<List<CityWeather>> GetWeather(string startDate, string endDate)
        {
            var df = DateTime.Parse(startDate);
            var dt = DateTime.Parse(endDate);
            var dateFrom = DateTime.Parse(startDate).ToUniversalTime().ToString("O");
            var dateTo = DateTime.Parse(endDate).ToUniversalTime().ToString("O");
            //var fromDate = Convert.ToDateTime(startDate, CultureInfo.InvariantCulture);
            //var toDAte = Convert.ToDateTime("2022-03-26T11:25:13.354Z", CultureInfo.InvariantCulture);
            
            var filter = new BsonDocument("$and", new BsonArray{
                 
                new BsonDocument("Date",new BsonDocument("$gte", dateFrom)),
                new BsonDocument("Date", new BsonDocument("$lte", dateTo))
            });
            
            return await mongoContext.ListOrEmpty<CityWeather>(filter.ToString());
        }
    }
}