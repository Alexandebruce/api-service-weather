using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiWeather.Models
{
    public class BaseElement
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public DateTime Date { get; set; }
        public List<CityWeather> Data { get; set; }
    }
    
    public class CityWeather
    {
        public CityWeather()
        {
            WeatherByDays = new List<DayWeather>();
        }
        
        public string CityName { get; set; }
        public DateTime Date { get; set; }
        public List<DayWeather> WeatherByDays { get; set; }
    }

    public class DayWeather
    {
        public DailyTemperature TemperatureC { get; set; }
        public string WeatherDescription { get; set; }
        public string WindSpeed { get; set; }
        public string Precipitation { get; set; }
        public DailyPressure PressureAtm { get; set; }
        public string Humidity { get; set; }
        public string Geomagnetic { get; set; }
    }
    
    public class DailyTemperature
    {
        public string Minimum { get; set; }
        public string Maximum { get; set; }
    }
    
    public class DailyPressure
    {
        public string Minimum { get; set; }
        public string Maximum { get; set; }
    }
}