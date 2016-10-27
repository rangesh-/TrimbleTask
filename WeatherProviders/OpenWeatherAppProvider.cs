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
    /// Open Weather API Provider that connects to external system and fetches weather info
    /// based on country and city
    /// </summary>
    public class OpenWeatherAppProvider : IWeatherProvider
    {
        private readonly IApiProvider _provider;
        private readonly IOpenWeatherFormatter _formatter;
        private string APIKey = "26a4da74baf5fa2575a3ada3614988cb";
        private WeatherDetails _weatherinfo;
        public OpenWeatherAppProvider(IApiProvider provider,IOpenWeatherFormatter formatter)
        {
            _provider = provider;
            _formatter = formatter;
            _weatherinfo  = new WeatherDetails()
            {
                CityInfo = new CityInfo(),
                WeatherInfo = new List<WeatherInfo>()
            };
        }
        /// <summary>
        /// Fetch 5 day weather forecast only if not in cache.
        /// </summary>
        /// <param name="city"></param>
        /// <param name="countrycode"></param>
        /// <returns></returns>

        public async Task<WeatherDetails> Fetch5DayWeatherData(string city, string countrycode)
        {
            if (CacheManager.Instance.Get<WeatherDetails>(city + countrycode + "OP") != null)
                _weatherinfo = CacheManager.Instance.Get<WeatherDetails>(city + countrycode + "OP");
            else           
                await FetchWeatherData(city, countrycode);     
            return _weatherinfo;
        }
        /// <summary>
        /// Make an External call to API and fetch data in JSOn 
        /// </summary>
        /// <param name="city"></param>
        /// <param name="countrycode"></param>
        /// <returns></returns>
        private async Task FetchWeatherData(string city, string countrycode)
        {
            ListofWeather listofweather;
            try
            {
                var request = @"http://api.openweathermap.org/data/2.5/forecast?q=" + city + "," + countrycode +
                              "&mode=json&appid=26a4da74baf5fa2575a3ada3614988cb&&units=metric";
                var result = _provider.Request(request);
                await result.ContinueWith(arg =>
                {
                    var response = result.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = response.Content.ReadAsStringAsync();
                        listofweather = JsonConvert.DeserializeObject<ListofWeather>(jsonString.Result);
                        _weatherinfo = _formatter.FilterWeatherData(listofweather);
                    }
                    var cacheduration = int.Parse(ConfigurationManager.AppSettings["CacheDurationforOpenWeather"]);
                    CacheManager.Instance.Put<WeatherDetails>(city + countrycode + "OP", _weatherinfo,
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
