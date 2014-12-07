using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.LocationServices
{
    class LocationServices: ISave, ILoad, IDataCollectable
    {
        public LocationServices()
        {
            CheckSettings();
            LoadData();
        }

        private void CheckSettings()
        {
            // Check the Settings Object. Insert permissions in fields in this Object
            throw new NotImplementedException();
        }

        // Send the location to the DataCollection Object for saving, if allowed
        public bool SaveData()
        {
            CheckSettings();
            throw new NotImplementedException();
        }

        // Get current location
        public bool LoadData()
        {
            CheckSettings();
            throw new NotImplementedException();
        }
    }
}
