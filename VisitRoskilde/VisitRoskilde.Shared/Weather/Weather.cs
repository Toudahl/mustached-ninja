using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.Weather
{
    [Serializable]
    class Weather: ILoad, IMyDataPersists, IDataCollectable
    {

        public Weather()
        {
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

        }

    }
}
