using WeatherAppData;

namespace WeatherApp.Interfaces
{
    public interface IOpenWeatherFormatter
    {
        WeatherDetails FilterWeatherData(object weatherinfo);
    }
}
