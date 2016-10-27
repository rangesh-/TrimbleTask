using System;
using System.Runtime.Serialization;

namespace WeatherProviders
{
    [DataContract]
    public class Weather
    {
        [DataMember(Name = "main")]
        public DetailedWeatherInfo DetailedWeatherInfo { get; set; }

        [DataMember(Name = "weather")]
        public OpenWeatherinfo[] Weatherinfo { get; set; }
        [DataMember(Name = "dt_txt")]
        public DateTime WeatherDate { get; set; }

    }
}