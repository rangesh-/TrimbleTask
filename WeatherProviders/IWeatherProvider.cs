using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WeatherProviders
{
    public interface IWeatherProvider
    {
         Task<ObservableCollection<OpenWeatherAppProvider.WeatherInfo>> Fetch5DayWeatherData(string city, string countrycode);
    }
}