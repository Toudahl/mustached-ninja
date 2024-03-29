﻿using System;
using System.Runtime.Serialization;
using VisitRoskilde.FacebookIntegrationModule;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.SettingsModule
{   
    // Author: Morten Toudahl
    /// <summary>
    /// This class manages the settings of the application.
    /// </summary>
    [DataContract]
    class Settings : Serializer<Settings>, ISave, ILoad, IMyDataPersists
    {
        private FacebookIntegration facebook;
        [DataMember]
        private bool _dataCollectionAllowed;
        [DataMember]
        private bool _locationServicedEnabled;
        private string _facebookLogMessage = "";

        public Settings()
        {
            _fileName = "settings.xml";
            facebook = FacebookIntegration.GetInstance();
            //LoadData();
        }

        /// <summary>
        /// Used to enable or disable DataCollection.
        /// </summary>
        public bool DataCollectionAllowed
        {
            get { return _dataCollectionAllowed; }
            set { _dataCollectionAllowed = value; }
        }

        /// <summary>
        /// Used to enable or disable the LocationServices.
        /// </summary>
        public bool LocationServicedEnabled
        {
            get { return _locationServicedEnabled; }
            set { _locationServicedEnabled = value; }
        }

        /// <summary>
        /// Contains the message from the log in or log out process, if it failed.
        /// </summary>
        public string FacebookLogMessage
        {
            get { return _facebookLogMessage; }
            private set { _facebookLogMessage = "The action failed with the message: " + value; }
        }

        /// <summary>
        /// Informs you if the user is currently logged into facebook or not.
        /// </summary>
        /// <returns></returns>
        public bool FacebookStatus()
        {
            if (facebook.Status)
                return true;
            return false;
        }

        /// <summary>
        /// Used for logging into facebook. Supply username and password.
        /// </summary>
        /// <param name="username">example@email.com</param>
        /// <param name="password">Passw0rd</param>
        public void FacebookLogIn()
        {
                facebook.LogIn();
        }

        /// <summary>
        /// Used for logging out of facebook.
        /// </summary>
        public void FacebookLogOut()
        {
            if (FacebookStatus())
            {
                try
                {
                    facebook.LogOut();
                }
                catch (Exception exception)
                {
                    FacebookLogMessage = exception.Message;
                }
            }
        }

        /// <summary>
        /// Saves the settings to xml
        /// </summary>
        /// <returns></returns>
        public bool SaveData()
        {
                Serialize();
                return true;
        }

        /// <summary>
        /// Loads the settings from xml
        /// </summary>
        /// <returns></returns>
        public bool LoadData()
        {
            Deserialize();
            if (_restoredObject != null)
            {
                _dataCollectionAllowed = _restoredObject._dataCollectionAllowed;
                _locationServicedEnabled = _restoredObject._locationServicedEnabled;
                return true;
            }
            return false;
        }
    }
}
