using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using VisitRoskilde.Annotations;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.WeatherModule
{
    // Author: Morten Toudahl

    /// <summary>
    /// This class contains the Weather.
    /// You should use the WeatherRetriever class if you wish to get the updated weather data
    /// </summary>
    [DataContract]
    class Weather: Serializer<Weather>, ISave, ILoad, IMyDataPersists, IDataCollectable
    {
        [DataMember]
        private string _temperature;
        [DataMember]
        private string _wind;
        [DataMember]
        private string _sunrise;
        [DataMember]
        private string _sunset;
        [DataMember]
        private string _cloudiness;
        [DataMember]
        private DateTime _timeStamp;
        [DataMember]
        private string _humidity;

        public Weather()
        {
            _fileName = "weather.xml";
            LoadData();
        }

        #region Fields
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
            }
        }

        public string Humidity
        {
            get { return _humidity; }
            set { _humidity = value; }
        }

        public string Wind
        {
            get { return _wind; }
            set { _wind = value; }
        }

        public string Sunrise
        {
            get { return _sunrise; }
            set { _sunrise = value; }
        }

        public string Sunset
        {
            get { return _sunset; }
            set { _sunset = value; }
        }

        public string Cloudiness
        {
            get { return _cloudiness; }
            set { _cloudiness = value; }
        }

        public DateTime TimeStamp
        {
            get { return _timeStamp; }
            set { _timeStamp = value; }
        }


        #endregion

        /// <summary>
        /// Serialize the weather data
        /// </summary>
        /// <returns></returns>
        public bool SaveData()
        {
            Serialize();
            return true;
        }

        /// <summary>
        /// Deserialize the weather data
        /// </summary>
        /// <returns></returns>
        public bool LoadData()
        {
            Deserialize();
            if (_restoredObject != null)
            {
                TimeStamp = _restoredObject.TimeStamp;
                Temperature = _restoredObject.Temperature;
                Humidity = _restoredObject.Humidity;
                Wind = _restoredObject.Wind;
                Sunrise = _restoredObject.Sunrise;
                Sunset = _restoredObject.Sunset;
                Cloudiness = _restoredObject.Cloudiness;
                return true;
            }
            return false;
        }
    }
}
