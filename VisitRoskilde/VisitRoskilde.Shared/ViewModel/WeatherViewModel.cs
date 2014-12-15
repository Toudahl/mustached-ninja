using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TouristAppV4.Common;
using VisitRoskilde.Annotations;
using VisitRoskilde.WeatherModule;

namespace VisitRoskilde.ViewModel
{
    class WeatherViewModel
    {
        private WeatherRetriever weatherRetriever;
        private ICommand _loadCommand;

        public WeatherViewModel()
        {
            weatherRetriever = new WeatherRetriever();
            _loadCommand = new RelayCommand(Load);
        }

        private void Load()
        {
            weatherRetriever.Weather.LoadData();
        }

        public ICommand LoadCommand
        {
            get { return _loadCommand; }
            set { _loadCommand = value; }
        }

        public string Temperature
        {
            get
            {
                return weatherRetriever.Weather.Temperature;
            }
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
