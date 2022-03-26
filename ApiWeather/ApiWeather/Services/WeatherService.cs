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

        public async Task<List<BaseElement>> GetWeather(string startDate, string endDate)
        {
            var dateFrom = Convert.ToDateTime(DateTime.Parse(startDate).ToUniversalTime().ToString("O"), CultureInfo.InvariantCulture);
            var dateTo = Convert.ToDateTime(DateTime.Parse(endDate).ToUniversalTime().ToString("O"), CultureInfo.InvariantCulture);

            var filter = new BsonDocument("$and", new BsonArray{
                 
                new BsonDocument("Date",new BsonDocument("$gte", dateFrom)),
                new BsonDocument("Date", new BsonDocument("$lte", dateTo))
            });
            
            return await mongoContext.ListOrEmpty<BaseElement>(filter);
        }
    }
}