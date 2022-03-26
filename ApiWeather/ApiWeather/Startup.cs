using ApiWeather.Dao;
using ApiWeather.Dao.Interfaces;
using ApiWeather.Services;
using ApiWeather.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiWeather
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMongoContext>(provider => new MongoContext(new MongoSettings(provider.GetService<IConfiguration>())));
            services.AddTransient<IWeatherService>(provider => new WeatherService(provider.GetService<IMongoContext>()));
            services.AddControllers();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}