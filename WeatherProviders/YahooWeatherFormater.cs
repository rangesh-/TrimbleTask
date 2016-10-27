using System;
using System.Collections.Generic;
using System.Linq;
using WeatherApp.Interfaces;
using WeatherAppData;

namespace WeatherProviders
{
    /// <summary>
    /// Yahoo weather formatter for JSON to ViewModel
    /// </summary>
    public class YahooWeatherFormater : IYahooWeatherFormatter
    {
        private WeatherDetails _weatherinfo;

        public YahooWeatherFormater()
        {
          _weatherinfo = new WeatherDetails()
            {
                CityInfo = new CityInfo(),
                WeatherInfo = new List<WeatherInfo>()
            };
        }
        /// <summary>
        /// JSON to ViewModel
        /// </summary>
        /// <param name="weatherinfo"></param>
        /// <returns></returns>
        public WeatherDetails FilterWeatherData(object weatherinfo)
        {

            var weather = weatherinfo as RootObject;
            _weatherinfo.WeatherInfo?.Clear();

            var currentdate = DateTime.Now.Date;
            int i = 0;
            if (weather != null)
            {
                var result = weather.query.results.channel.item.forecast.Take(5);
                foreach (var item in result)
                {

                    if (item.date.Day == currentdate.Date.Day)
                    {
                        if (_weatherinfo.WeatherInfo != null)
                            _weatherinfo.WeatherInfo.Add(new WeatherInfo()
                            {
                                WeatherDate = item.date.DayOfWeek.ToString() + "  " + item.date.Day + "/" + item.date.Month,
                                Description = item.text,
                                Temperature = item.high,
                                Icon = @"http://l.yimg.com/a/i/us/we/52/" + item.code + ".gif"
                            }
                                );
                        currentdate = currentdate.AddDays(1);
                    }
                    _weatherinfo.CityInfo = new CityInfo() { Latitude = weather.query.results.channel.item.lat, Longitude = weather.query.results.channel.item.@long };


                }
            }
            return _weatherinfo;
        }
    }
}
