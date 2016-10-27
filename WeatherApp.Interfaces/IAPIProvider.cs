using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp.Interfaces
{
    public interface IApiProvider
    {
        Task<HttpResponseMessage> Request(string querystring);
    }
}
