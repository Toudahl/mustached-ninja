using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Windows.Devices.Geolocation;
using VisitRoskilde.DataCollectionModule;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;
using VisitRoskilde.SettingsModule;

namespace VisitRoskilde.LocationServiceModule
{
    // Author: Morten Toudahl
    /// <summary>
    /// This class is used for locating the users device.
    /// </summary>
    [DataContract]
    class LocationServices:  ISave, ILoad, IDataCollectable
    {
        private Settings settings;
        private Geolocator locator;
        [DataMember]
        private Dictionary<string, string> _position;
        private DataCollection collector;
        private string _locationError;
        private bool _locationEnabled;
        private bool _dataCollectionEnabled;

        public LocationServices()
        {
            settings = new Settings();
            CheckSettings();
            locator = new Geolocator();
            collector = DataCollection.GetInstance();
            if (_locationEnabled)
            {
                LoadData();
                SaveData();
            }
        }

        /// <summary>
        /// Dictionary for containing the location of the user.
        /// Keys are: Latitude and Longitude.
        /// If the location is not enabled, it will return null.
        /// </summary>
        public Dictionary<string, string> Position
        {
            get
            {
                if (_locationEnabled)
                {
                    return _position;
                }
                return null;
            }
            private set { _position = value; }
        }

        /// <summary>
        /// This will contain an error message if getting the location has failed.
        /// If the location is not enabled, it will return "Location not enabled".
        /// If there was no error in getting the location, this will be null.
        /// </summary>
        public string LocationError
        {
            get
            {
                if (!_locationEnabled)
                {
                    return "Location not enabled";
                }
                return _locationError;
            }
            private set { _locationError = value; }
        }

        /// <summary>
        /// Make sure that the Locations Services have the nessesary permissions for its actions.
        /// </summary>
        private void CheckSettings()
        {
            _locationEnabled = settings.LocationServicedEnabled;
            _dataCollectionEnabled = settings.DataCollectionAllowed;
        }

        /// <summary>
        /// Passes the current instance of this object to the DataCollection object.
        /// </summary>
        /// <returns></returns>
        public bool SaveData()
        {
            CheckSettings();
            if (_dataCollectionEnabled)
            {
                if (Position.Count > 0)
                {
                    collector.Collect(this);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This is used for retrieving the location from the device in use.
        /// It will return false, if location services is disabled.
        /// It will return true, if finding the location of the device succeded.
        /// It will pass an error message to the property LocationError if it failed.
        /// </summary>
        /// <returns></returns>
        public bool LoadData()
        {
            CheckSettings();
            if (_locationEnabled)
            {
                try
                {
                    LocateDevice();
                    LocationError = null;
                    return true;
                }
                catch (Exception exception)
                {
                    LocationError = exception.Message;
                }
            }
            return false;
        }

        /// <summary>
        /// Does the actual locating of the device.
        /// </summary>
        private async void LocateDevice()
        {
            if (_locationEnabled)
            {
                if (!locator.LocationStatus.Equals(PositionStatus.Disabled))
                {
                    Position = new Dictionary<string, string>();
                    var location = await locator.GetGeopositionAsync();
                    Position["Latitude"] = location.Coordinate.Latitude.ToString();
                    Position["Longitude"] = location.Coordinate.Longitude.ToString();
                    if (Position.Count == 0)
                    {
                        throw new Exception("Location was not found.");
                    }
                }
            }
        }



    }
}
