using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using WeatherApp.Interfaces;
using WeatherAppData;

namespace WeatherProviders
{
    /// <summary>
    /// Yahoo Weather Provider that fetches weather info based on country and city
    /// </summary>
    public class YahooWeatherProvider : IWeatherProvider
    {
        private WeatherDetails _weatherinfo = new WeatherDetails() {CityInfo = new CityInfo(),WeatherInfo = new List<WeatherInfo>()};
        private IApiProvider _provider;
        private IYahooWeatherFormatter _formatter;

        public YahooWeatherProvider(IApiProvider provider,IYahooWeatherFormatter formatter)
        {
            _provider = provider;
            _formatter = formatter;
        }

        /// <summary>
        /// Fetch Weather Information based on city and countrycode.
        /// Fetch only if is not in cache
        /// </summary>
        /// <param name="city"></param>
        /// <param name="countrycode"></param>
        /// <returns></returns>
        public async Task<WeatherDetails> Fetch5DayWeatherData(string city, string countrycode)
        {
            if (CacheManager.Instance.Get<WeatherDetails>(city + countrycode + "YP") != null)
                _weatherinfo = CacheManager.Instance.Get<WeatherDetails>(city + countrycode + "YP");
            else
            {
                await FetchWeatherData(city, countrycode);

            }
            return _weatherinfo;
        }

        //Method that actually does the formatting work.
        private async Task FetchWeatherData(string city, string countrycode)
        {
            RootObject listofweather;
            try
            {
                var request =
                    @"https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" +
                    city + "%2C%20" + countrycode +
                    "%22)%20and%20u%3D'c'&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";
                var result = _provider.Request(request);
                await result.ContinueWith(arg =>
                {
                    var response = result.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        listofweather = JsonConvert.DeserializeObject<RootObject>(jsonString.Result);
                        _weatherinfo = _formatter.FilterWeatherData(listofweather);
                    }
                    var cacheduration = int.Parse(ConfigurationManager.AppSettings["CacheDurationforYahoo"]);
                    CacheManager.Instance.Put<WeatherDetails>(city + countrycode + "YP", _weatherinfo,
                       new TimeSpan(cacheduration, 0, 0));
                });

            }
            catch (AggregateException exception)
            {
                _weatherinfo.HasErrors = true;
                _weatherinfo.ErrorMessage = exception.InnerException.Message;
            }
            catch (Exception ex)
            {
                _weatherinfo.HasErrors = true;
                _weatherinfo.ErrorMessage = ex.InnerException.Message;
            }
        }
    }
}
