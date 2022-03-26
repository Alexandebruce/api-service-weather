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
    }
}