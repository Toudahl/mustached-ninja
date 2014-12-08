using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.Weather
{
    [DataContract]
    class Weather: Serializer<Weather>, ISave, IMyDataPersists, IDataCollectable
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
            Deserialize();
        }

        #region Fields
        public string Temperature
        {
            get { return _temperature; }
            set { _temperature = value; }
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


        public bool SaveData()
        {
            try
            {
                Serialize();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("The saving of the weather data failed with the following message: " + exception);
            }
        }

        ~Weather()
        {
            SaveData();
        }
    }
}
