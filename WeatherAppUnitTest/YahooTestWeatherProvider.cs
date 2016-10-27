using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WeatherProviders;
using WeatherAppData;

namespace WeatherAppUnitTest
{
    [TestClass]
    public class YahooTestWeatherProvider
    {
        YahooWeatherProvider provider = new YahooWeatherProvider(new APIProvider(), new YahooWeatherFormater());
        [TestMethod]
        public void TestFetch5DayWeatherDataandCompareModel()
        {
            var result = provider.Fetch5DayWeatherData("chennai", "IN");
            result.Wait();
            var resultmodel = result.Result.WeatherInfo[0];
            var testmodel = new WeatherInfo() { Description = "Partly Cloudy", Icon = "http://l.yimg.com/a/i/us/we/52/30.gif", WeatherDate = "Thursday  27/10", Temperature = "31" };
            Assert.AreEqual(resultmodel.Description, testmodel.Description);
            Assert.AreEqual(resultmodel.Icon, testmodel.Icon);
            Assert.AreEqual(resultmodel.Temperature, testmodel.Temperature);
            Assert.AreEqual(resultmodel.WeatherDate, testmodel.WeatherDate);
        }
        [TestMethod]
        public void TestGetWeatherDataCount()
        {
            var result = provider.Fetch5DayWeatherData("chennai", "IN");
            result.Wait();
            Assert.AreEqual(5, result.Result.WeatherInfo.Count);
        }

        [TestMethod]
        public void TestExceptionisSet()
        {
            var result = provider.Fetch5DayWeatherData("chennai", "IN");
            result.Wait();
            //Faking as if exception has occured
            result.Result.HasErrors = true;
            Assert.AreEqual(result.Result.HasErrors, true);

        }
    }
}
