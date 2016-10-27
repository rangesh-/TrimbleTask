using System;
using System.Collections.Generic;
using System.Globalization;
using WeatherApp.Interfaces;
using WeatherAppData;

namespace WeatherProviders
{
    /// <summary>
    /// OpenWeather Formatter that formats the JSON to ViewModel
    /// </summary>
    public class OpenWeatherFormatter : IOpenWeatherFormatter
    {
        private WeatherDetails _weatherinfo;

        public OpenWeatherFormatter()
        {
            _weatherinfo = new WeatherDetails()
                {
                    CityInfo = new CityInfo(),
                    WeatherInfo = new List<WeatherInfo>()
                };
        }
        /// <summary>
        /// Formatter of Json to ViewModel
        /// </summary>
        /// <param name="weatherinfo"></param>
        /// <returns></returns>
        public WeatherDetails FilterWeatherData(object weatherinfo)
        {
            var weather = weatherinfo as ListofWeather;
            _weatherinfo.WeatherInfo.Clear();
            var currentdate = DateTime.Now.Date;
            if (weather != null)
            {
                foreach (var item in weather.Weather)
                {

                    if (item.WeatherDate.Date.Day == currentdate.Date.Day)
                    {

                        _weatherinfo.WeatherInfo.Add(new WeatherInfo()
                        {
                            WeatherDate =
                                item.WeatherDate.DayOfWeek.ToString() + "  " + item.WeatherDate.Day + "/" +
                                item.WeatherDate.Month,
                            Description = item.Weatherinfo[0].Description,
                            Temperature = item.DetailedWeatherInfo.Temperature,
                            Icon = @"http://openweathermap.org/img/w/" + item.Weatherinfo[0].Icon + ".png"
                        });

                        currentdate = currentdate.AddDays(1);

                    }

                }

                _weatherinfo.CityInfo = new CityInfo()
                {
                    Latitude = weather.city.coord.lat.ToString(CultureInfo.InvariantCulture),
                    Longitude = weather.city.coord.lon.ToString(CultureInfo.InvariantCulture)
                };
            }
            return _weatherinfo;
        }
    }
}
