using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.LocationServiceModule
{
    class LocationServices:  ISave, ILoad, IDataCollectable
    {
        private bool locationEnabled;
        private bool dataCollectionEnabled;

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
            return false;
        }

        // Get current location
        public bool LoadData()
        {
            CheckSettings();
            throw new NotImplementedException();
        }
    }
}
