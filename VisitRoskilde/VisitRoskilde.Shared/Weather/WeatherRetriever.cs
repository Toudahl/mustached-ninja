﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Data.Xml.Dom;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Weather
{
    // Author: Morten Toudahl
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

        private async void RefreshData()
        {
            if (_weather.TimeStamp.AddHours(6) <= DateTime.Now)
            {
                Uri weatherUrl = new Uri(dataLocation);

                XmlDocument doc = await XmlDocument.LoadFromUriAsync(weatherUrl);

                _weather.Temperature = doc.GetElementsByTagName("temperature")[0].Attributes[0].NodeValue.ToString();
                _weather.Humidity = doc.GetElementsByTagName("humidity")[0].Attributes[0].NodeValue.ToString();
                _weather.Wind = doc.GetElementsByTagName("speed")[0].Attributes[1].NodeValue.ToString();
                _weather.Sunrise = doc.GetElementsByTagName("sun")[0].Attributes[0].NodeValue.ToString().Substring(11);
                _weather.Sunset = doc.GetElementsByTagName("sun")[0].Attributes[1].NodeValue.ToString().Substring(11);
                _weather.Cloudiness = doc.GetElementsByTagName("clouds")[0].Attributes[1].NodeValue.ToString();
                _weather.TimeStamp = DateTime.Now;
            }
        }

        public Weather Weather
        {
            get { return _weather; }
        }
    }
}
