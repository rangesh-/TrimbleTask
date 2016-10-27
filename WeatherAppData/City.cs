using System.Collections.Generic;
using WeatherAppData;

namespace WeatherApp
{
    /// <summary>
    /// This class is the model for Getting all cities of India.
    /// </summary>
    public class CityFinderModel : BaseModel
    {
        public List<string> city { get; set; }
    }
}
