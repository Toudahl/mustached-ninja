using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Weather
{
    class WeatherRetriever: ILoad
    {
        public WeatherRetriever()
        {
            // Create Weather Object
        }
        // Deserialize the weather data if no internet connection, or get new if the data is old.
        public bool LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
