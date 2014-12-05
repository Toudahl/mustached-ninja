using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Weather
{
    class WeatherRetriever: ILoad
    {
        // Deserialize the weather data if no internet connection, or old data.
        public bool LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
