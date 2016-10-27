using System.Collections.Generic;

namespace WeatherProviders
{
    public class Item
    {
        public List<Forecast> forecast { get; set; }
        public string lat { get; set; }
        public string @long { get; set; }
    }
}