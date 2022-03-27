using System;
using System.Threading.Tasks;
using ApiWeather.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeather.Controllers
{
    [ApiController, Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> GetWeather([FromQuery] string startDate, string endDate)
        {
            return Ok(await weatherService.GetWeather(startDate, endDate));
        }
        
        [HttpGet("/date/{targetDate}/city/{city}")]
        public async Task<IActionResult> GetConcreteWeather([FromRoute] string targetDate, string city)
        {
            return Ok(await weatherService.GetConcreteWeather(targetDate, city));
        }
    }
}