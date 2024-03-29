﻿using System;
using Windows.Data.Xml.Dom;

namespace VisitRoskilde.WeatherModule
{
    // Author: Morten Toudahl
    /// <summary>
    /// This class is used for retrieving the weather information.
    /// Currently it is being retrieved from openweathermap.org
    /// </summary>
    class WeatherRetriever
    {
        private const string city = "Roskilde,DK";
        private const string dataType = "xml";
        private const string unitType = "metric";
        private const string appID = "629f3309109159c6beff00ede0af4940";
        private const string dataLocation =
            "http://api.openweathermap.org/data/2.5/weather?q="+city+"&mode="+dataType+"&units="+unitType+"&APPID="+appID;
        private Weather _weather;


        public WeatherRetriever()
        {
            _weather = new Weather();
            RefreshData();
        }

        /// <summary>
        /// If 6 hours has passed since the last refresh, get new data from the remote repository
        /// </summary>
        private async void RefreshData()
        {
            if (_weather.TimeStamp.AddHours(6) < DateTime.Now)
            {
                Uri weatherUrl = new Uri(dataLocation);

                XmlDocument doc = await XmlDocument.LoadFromUriAsync(weatherUrl);

                Weather.Temperature = doc.GetElementsByTagName("temperature")[0].Attributes[0].NodeValue.ToString();
                Weather.Humidity = doc.GetElementsByTagName("humidity")[0].Attributes[0].NodeValue.ToString();
                Weather.Wind = doc.GetElementsByTagName("speed")[0].Attributes[1].NodeValue.ToString();
                Weather.Sunrise = doc.GetElementsByTagName("sun")[0].Attributes[0].NodeValue.ToString().Substring(11);
                Weather.Sunset = doc.GetElementsByTagName("sun")[0].Attributes[1].NodeValue.ToString().Substring(11);
                Weather.Cloudiness = doc.GetElementsByTagName("clouds")[0].Attributes[1].NodeValue.ToString();
                Weather.TimeStamp = DateTime.Now;
                Weather.SaveData();
            }
        }

        /// <summary>
        /// Get the weather object
        /// </summary>
        public Weather Weather
        {
            get { return _weather; }
            private set
            {
                _weather = value;
            }
        }
    }
}
