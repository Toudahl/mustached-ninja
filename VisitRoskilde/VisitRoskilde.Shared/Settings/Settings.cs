using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.Settings
{
    [DataContract]
    class Settings : Serializer<Settings>, ISave, ILoad, IMyDataPersists
    {
        public Settings()
        {
            _fileName = "settings.xml";
            LoadData();
        }

        // Save the new settings both to this object, and to the StoreData Object.
        public bool SaveData()
        {
            try
            {
                Serialize(this);
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
            throw new NotImplementedException();
        }

        // Serialize the settings
        ~Settings()
        {
            SaveData();
        }
    }
}
