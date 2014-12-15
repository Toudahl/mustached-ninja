using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.WeatherModule;

namespace VisitRoskilde.ViewModel
{
    class WeatherViewModel
    {
        private WeatherRetriever weatherRetriever;

        public WeatherViewModel()
        {
            weatherRetriever = new WeatherRetriever();
        }

        public string Temperature
        {
            get { return weatherRetriever.Weather.Temperature; } 
        }

        public string Cloudiness
        {
            get { return weatherRetriever.Weather.Cloudiness; }
        }

        public string Wind
        {
            get { return weatherRetriever.Weather.Wind; } 
        }

        public string Humidity
        {
            get { return weatherRetriever.Weather.Humidity; }
        }

        public string Sunset
        {
            get { return weatherRetriever.Weather.Sunset; }
        }

        public string Sunrise
        {
            get { return weatherRetriever.Weather.Sunrise; }
        }

        public string TimeStamp
        {
            get { return "Last update " +weatherRetriever.Weather.TimeStamp; }
        }

        public string TimeTillNextRefresh
        {
            get { return "Next update" + weatherRetriever.Weather.TimeStamp.AddHours(6); }
        }
    }
}
