using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Windows.UI.Xaml.Controls;
using VisitRoskilde.FacebookIntegrationModule;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.SettingsModule
{   
    // Author: Morten Toudahl
    
    class Settings : Serializer<Settings>, ISave, ILoad, IMyDataPersists
    {
        private bool _dataCollectionAllowed;
        private bool _locationServicedEnabled;
        private bool _facebookIsLoggedIn;

        public Settings()
        {
            _fileName = "settings.xml";
            LoadData();
        }

        public bool DataCollectionAllowed
        {
            get { return _dataCollectionAllowed; }
            set { _dataCollectionAllowed = value; }
        }

        // Make a privacy setting, enable/disable location services
        public bool LocationServicedEnabled
        {
            get { return _locationServicedEnabled; }
            set { _locationServicedEnabled = value; }
        }

        // Make a facebook status check

        // Provide log in option for facebook - facilitating contact to FacebookIntegration

        // Provide log out option for facebook - facilitating contact to FacebookIntegration


        // Save the new settings both to this object, and to the StoreData Object.
        public bool SaveData()
        {
            try
            {
                Serialize();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("The saving of the settings failed with the following message: " + exception);
            }
        }

        // Deserialize the settings, get the file to deserialize from the StoreData Object
        public bool LoadData()
        {
            try
            {
                Deserialize();
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception("Loading the settings failed with the following message: " + exception);
            }
        }

        // Serialize the settings
        ~Settings()
        {
            SaveData();
        }
    }
}
