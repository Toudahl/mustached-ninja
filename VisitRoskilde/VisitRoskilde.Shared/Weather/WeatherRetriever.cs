using System;
using System.Collections.Generic;
using System.Text;
using Windows.Data.Xml.Dom;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Weather
{
    class WeatherRetriever
    {
        //TODO: return the weather object in some way.

        private string dataLocation =
            "http://api.openweathermap.org/data/2.5/weather?q=Roskilde,DK&mode=xml&units=metric&APPID=629f3309109159c6beff00ede0af4940";
        private Weather _weather;


        public WeatherRetriever()
        {
            _weather = new Weather();
            RefreshData();
        }

        private async void RefreshData()
        {
            Uri weatherUrl = new Uri(dataLocation);

            XmlDocument doc = await XmlDocument.LoadFromUriAsync(weatherUrl);

            if (_weather.TimeStamp.AddHours(6) <= DateTime.Now)
            {
                _weather.Temperature = doc.GetElementsByTagName("temperature")[0].Attributes[0].NodeValue.ToString();
                _weather.Humidity = doc.GetElementsByTagName("humidity")[0].Attributes[0].NodeValue.ToString();
                _weather.Wind = doc.GetElementsByTagName("speed")[0].Attributes[1].NodeValue.ToString();
                _weather.Sunrise = doc.GetElementsByTagName("sun")[0].Attributes[0].NodeValue.ToString().Substring(11);
                _weather.Sunset = doc.GetElementsByTagName("sun")[0].Attributes[1].NodeValue.ToString().Substring(11);
                _weather.Cloudiness = doc.GetElementsByTagName("clouds")[0].Attributes[1].NodeValue.ToString();
                _weather.TimeStamp = DateTime.Now;
            }
        }
    }
}
