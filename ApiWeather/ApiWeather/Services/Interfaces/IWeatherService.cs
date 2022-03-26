using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiWeather.Models;

namespace ApiWeather.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<List<CityWeather>> GetWeather(string startDate, string endDate);
    }
}