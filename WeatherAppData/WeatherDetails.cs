using System.Collections.Generic;

namespace WeatherAppData
{
    /// <summary>
    /// This Model is Responsible for displaying Weatherdetails of search request
    /// </summary>
    public class WeatherDetails :BaseModel
    {
        public List<WeatherInfo> WeatherInfo { get; set; }

        public  CityInfo CityInfo { get; set; }

    }
}