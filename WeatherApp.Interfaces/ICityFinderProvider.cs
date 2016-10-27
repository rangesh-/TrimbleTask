using System.Threading.Tasks;

namespace WeatherApp.Interfaces
{
    public interface ICityFinderProvider
    {
        Task<CityFinderModel> FindCity(); 
    }
}
