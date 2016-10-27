using System.Threading.Tasks;
using WeatherAppData;

namespace WeatherApp.Interfaces
{
    public interface IWeatherProvider
    {
         Task<WeatherDetails> Fetch5DayWeatherData(string city, string countrycode);
    }
}