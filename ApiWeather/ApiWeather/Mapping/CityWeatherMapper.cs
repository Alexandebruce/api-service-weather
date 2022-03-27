using System;
using System.Collections.Generic;
using System.Linq;
using ApiWeather.Models;

namespace ApiWeather.Mapping
{
    public static class CityWeatherMapper
    {
        private static DailyPressure MapToDomain(this DbDailyPressure dailyPressure)
        {
            return dailyPressure == null ? null : new DailyPressure
            {
                Minimum = dailyPressure.Minimum,
                Maximum = dailyPressure.Maximum
            };
        }
        
        private static DailyTemperature MapToDomain(this DbDailyTemperature dailyTemperature)
        {
            return dailyTemperature == null ? null : new DailyTemperature
            {
                Minimum = dailyTemperature.Minimum,
                Maximum = dailyTemperature.Maximum
            };
        }
        
        private static DayWeather MapToDomain(this DbDayWeather dayWeather, DateTime recordDate)
        {
            return dayWeather == null ? null : new DayWeather
            {
                Date = recordDate,
                TemperatureC = dayWeather.TemperatureC.MapToDomain(),
                WeatherDescription = dayWeather.WeatherDescription,
                WindSpeed = dayWeather.WindSpeed,
                Precipitation = dayWeather.Precipitation,
                PressureAtm = dayWeather.PressureAtm.MapToDomain(),
                Humidity = dayWeather.Humidity,
                Geomagnetic = dayWeather.Geomagnetic,
            };
        }
        
        private static List<DayWeather> MapToDomain(this IEnumerable<DbDayWeather> dayWeathers, DateTime recordDate)
        {
            return dayWeathers == null ? new List<DayWeather>() : dayWeathers
                .Select((x, y) => new {Index = y, Value = x})
                .Select(v => v.Value.MapToDomain(recordDate.AddDays(v.Index).AddHours(3))).ToList();
        }

        public static CityWeather MapToDomain(this DbCityWeather cityWeather)
        {
            return cityWeather == null
                ? null
                : new CityWeather
                {
                    CityName = cityWeather.CityName,
                    Date = cityWeather.Date,
                    WeatherByDays = cityWeather.WeatherByDays.MapToDomain(cityWeather.Date)
                };
        }
    }
}