using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiWeather.Models
{
    public class DbBaseElement
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public List<DbCityWeather> Data { get; set; }
    }
    
    public class DbCityWeather
    {
        public DbCityWeather()
        {
            WeatherByDays = new List<DbDayWeather>();
        }
        
        public string CityName { get; set; }
        public DateTime Date { get; set; }
        public List<DbDayWeather> WeatherByDays { get; set; }
    }

    public class DbDayWeather
    {
        public DbDailyTemperature TemperatureC { get; set; }
        public string WeatherDescription { get; set; }
        public string WindSpeed { get; set; }
        public string Precipitation { get; set; }
        public DbDailyPressure PressureAtm { get; set; }
        public string Humidity { get; set; }
        public string Geomagnetic { get; set; }
    }
    
    public class DbDailyTemperature
    {
        public string Minimum { get; set; }
        public string Maximum { get; set; }
    }
    
    public class DbDailyPressure
    {
        public string Minimum { get; set; }
        public string Maximum { get; set; }
    }
}