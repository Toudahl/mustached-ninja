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
    class Weather: Serializer<Weather>, ILoad, ISave, IMyDataPersists, IDataCollectable
    {

        public Weather()
        {
            _fileName = "weather.xml";
            LoadData();
            // Send the temperature and weather conditions (rainy, sunny etc.) to be DataCollected. 
        }

        // Get weather information from WeatherRetriever Object, or deserialize the Weather Object if no
        // internet or old (2-3 hours?) data
        public bool LoadData()
        {
            throw new NotImplementedException();
        }

        // Serialize the Weather Object
        ~Weather()
        {
            SaveData();
        }

        public bool SaveData()
        {
            try
            {
                Serialize(this);
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("The saving of the weather data failed with the following message: " + exception);
            }
        }
    }
}
