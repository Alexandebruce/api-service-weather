using Microsoft.Extensions.Configuration;

namespace ApiWeather.Dao
{
    public class MongoSettings
    {
        private readonly IConfiguration configuration;

        public MongoSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string ConnectionString => configuration.GetValue<string>("MongoDbConnectionString");
        public string Database => configuration.GetValue<string>("MongoDbName");
        public string Collection => configuration.GetValue<string>("WeatherCollectionName");
    }
}