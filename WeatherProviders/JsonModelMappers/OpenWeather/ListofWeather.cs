using System.Runtime.Serialization;

namespace WeatherProviders
{
    [DataContract]
    public class ListofWeather
    {
        [DataMember(Name = "list")]
        public Weather[] Weather { get; set; }
        [DataMember(Name = "city")]
        public City city { get; set; }
    }
}