using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.LocationServices
{
    class LocationServices: ISave, ILoad
    {
        // Send the location to the DataCollection Object for saving
        public bool SaveData()
        {
            throw new NotImplementedException();
        }

        // Load the settings, from the Settings Object
        public bool LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
