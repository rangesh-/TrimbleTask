using System.Runtime.Serialization;

namespace WeatherProviders
{
    [DataContract]
    public class DetailedWeatherInfo
    {
        [DataMember(Name = "temp")]
        public string Temperature { get; set; }

        [DataMember(Name = "humidity")]
        public string Humdity { get; set; }
    }
}