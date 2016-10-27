using WeatherAppData;

namespace WeatherApp.Interfaces
{
    public  interface IYahooWeatherFormatter
    {
        WeatherDetails FilterWeatherData(object weatherinfo);
    }
}
