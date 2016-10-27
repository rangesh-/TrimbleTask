using System.Runtime.Serialization;

namespace WeatherProviders
{
    [DataContract]
    public class OpenWeatherinfo
    {
        [DataMember(Name = "main")]
        public string WeatherType { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }
}