using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherProviders;

namespace WeatherAppUnitTest
{
    [TestClass]
    public class OpenWeatherTestFormatter
    {
        OpenWeatherFormatter formatter = new OpenWeatherFormatter() ;

        [TestMethod]
        public void TestOpenWeatherformatforcity()
        {
            var listofdetails = new ListofWeather();
            listofdetails.city = new City() { coord = new Coord() { lat = 10.11, lon = 10 } };
            listofdetails.Weather = new Weather[1];
             listofdetails.Weather[0] = new Weather()
            {
                DetailedWeatherInfo = new DetailedWeatherInfo() { Humdity = "11", Temperature = "90" },
                WeatherDate = DateTime.Now.Date,
                              
            };
            listofdetails.Weather[0].Weatherinfo = new OpenWeatherinfo[1];

            listofdetails.Weather[0].Weatherinfo[0]= new OpenWeatherinfo(){ Description = "Clear Sky", Icon = "http://sampleurl", WeatherType = "Sunny" };

            var result=formatter.FilterWeatherData(listofdetails);
            Assert.AreEqual(result.CityInfo.Latitude, "10.11");
        }
        [TestMethod]
        public void TestOpenWeatherformatforWeatherInfo()
        {
            var listofdetails = new ListofWeather();
            listofdetails.city = new City() { coord = new Coord() { lat = 10.11, lon = 10 } };
            listofdetails.Weather = new Weather[1];
            listofdetails.Weather[0] = new Weather()
            {
                DetailedWeatherInfo = new DetailedWeatherInfo() { Humdity = "11", Temperature = "90" },
                WeatherDate = DateTime.Now.Date,

            };
            listofdetails.Weather[0].Weatherinfo = new OpenWeatherinfo[1];

            listofdetails.Weather[0].Weatherinfo[0] = new OpenWeatherinfo() { Description = "Clear Sky", Icon = "http://sampleurl", WeatherType = "Sunny" };

            var result = formatter.FilterWeatherData(listofdetails);
            Assert.AreEqual(result.WeatherInfo[0].Description, "Clear Sky");
        }
    }
}
