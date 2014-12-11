using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using VisitRoskilde.WeatherModule;

namespace UnitTestWindows.WeatherModule
{
    [TestClass]
    public class WeatherTest
    {
        private WeatherRetriever retriever;
        private Weather weather;

        public WeatherTest()
        {
            retriever = new WeatherRetriever();
        }


        [TestMethod]
        public void WeatherRetrieverRefreshData()
        {
            bool test = false;
            weather = retriever.Weather;
            if (weather.Humidity != "" || weather.Humidity != null)
            {
                test = true;
            }
            weather.SaveData();
            Assert.IsTrue(test);
        }


        [TestMethod]
        public void WeatherTimeStamp()
        {
            weather = new Weather();
            bool test = false;
            weather.TimeStamp.AddHours(-6);
            if (DateTime.Now > weather.TimeStamp)
            {
                test = true;
            }
            Assert.IsTrue(test);
        }

    }
}
