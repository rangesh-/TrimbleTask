using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Interfaces;

namespace WeatherProviders
{
    /// <summary>
    /// APi Provider that is responsible for making api calls.
    /// Its a router that connects external system.
    /// </summary>
    public class APIProvider : IApiProvider
    {

        public async Task<HttpResponseMessage> Request(string querystring)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                 response = await client.GetAsync(querystring);
            }
            return response;
        }
    }
}
