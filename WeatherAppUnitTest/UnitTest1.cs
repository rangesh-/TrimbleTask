using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace WeatherAppUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test5DayWeatherData()
        {
            
                WeatherProviders.OpenWeatherAppProvider provider = new WeatherProviders.OpenWeatherAppProvider();
             var task=provider.Fetch5DayWeatherData("london","us");
            task.Wait();
            Console.WriteLine("Sd");
            
        }
    }
}
