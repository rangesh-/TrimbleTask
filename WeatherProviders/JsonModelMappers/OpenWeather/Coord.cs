using System.Runtime.Serialization;

namespace WeatherProviders
{
    public class Coord
    {
        [DataMember(Name = "lon")]
        public double lon { get; set; }
        [DataMember(Name = "log")]
        public double lat { get; set; }
    }
}