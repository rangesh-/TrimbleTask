using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp;
using WeatherApp.Interfaces;

namespace WeatherProviders
{
    /// <summary>
    /// Get List of all Cities associated with country
    /// </summary>
    public class CityProvider : ICityFinderProvider
    {
        private readonly IApiProvider _provider;
        private CityFinderModel _cities;
        public CityProvider(IApiProvider provider)
        {
            _provider = provider;
           _cities= new CityFinderModel();
        }
        public async Task<CityFinderModel> FindCity()
        {
  
            if (CacheManager.Instance.Get<CityFinderModel>("listofcity") != null)
                return _cities;
            else
            {
                return await FetchCity();
            }
        }

        private async Task<CityFinderModel> FetchCity()
        {
            try
            {
                var request = @"https://jsonblob.com/api/jsonBlob/58110150e4b0a828bd1b5c66";
                var result = _provider.Request(request);
                await result.ContinueWith((arg) =>
                {
                    var response = result.Result;
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        _cities = JsonConvert.DeserializeObject<CityFinderModel>(jsonString.Result);
                        _cities.city = _cities.city.Take(100).ToList();
                    }
                    CacheManager.Instance.Put<CityFinderModel>("listofcities", _cities, new TimeSpan(24, 0, 0));
                });

            }
            catch (Exception ex)
            {
                _cities.ErrorMessage = ex.InnerException.Message;
                _cities.HasErrors = true;
            }

            return _cities;
        }
    }
    
}
