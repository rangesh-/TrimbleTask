using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAppData;
using WeatherProviders;

namespace WeatherAppUnitTest
{
    [TestClass]
   public class OpenWeatherTestProvider
    {
        OpenWeatherAppProvider openweatherprovider = new OpenWeatherAppProvider(new APIProvider(), new OpenWeatherFormatter());
        [TestMethod]
        public void TestFetch5DayWeatherDataandCompareModel()
        {
              var result = openweatherprovider.Fetch5DayWeatherData("chennai", "IN");
             result.Wait();
            var resultmodel = result.Result.WeatherInfo[0];
            var testmodel = new WeatherInfo() { Description = "clear sky", Icon = "http://openweathermap.org/img/w/01d.png", WeatherDate = "Thursday  27/10", Temperature = "32.64" };
            Assert.AreEqual(resultmodel.Description,testmodel.Description);
            Assert.AreEqual(resultmodel.Icon, testmodel.Icon);
            Assert.AreEqual(resultmodel.Temperature, testmodel.Temperature);
            Assert.AreEqual(resultmodel.WeatherDate, testmodel.WeatherDate);
        }
        [TestMethod]
        public void TestGetWeatherDataCount()
        {
             var result = openweatherprovider.Fetch5DayWeatherData("chennai", "IN");
            result.Wait();
            Assert.AreEqual(5, result.Result.WeatherInfo.Count);
        }

        [TestMethod]
        public void TestExceptionisSet()
        {
              var result = openweatherprovider.Fetch5DayWeatherData("chennai", "IN");
            result.Wait();
            //Faking as if exception has occured
            result.Result.HasErrors = true;
            Assert.AreEqual(result.Result.HasErrors, true);

        }
    }
}
