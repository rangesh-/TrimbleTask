using System;

namespace WeatherProviders
{
    public class Forecast
    {
        public string code { get; set; }
        public DateTime date { get; set; }
        public string day { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string text { get; set; }
    }
}