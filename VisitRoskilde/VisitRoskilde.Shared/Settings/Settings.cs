using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.Settings
{
    [Serializable]
    class Settings : ISave, ILoad, IMyDataPersists
    {
        public Settings()
        {
            LoadData();
        }

        // Save the new settings both to this object, and to the StoreData Object.
        public bool SaveData()
        {
            throw new NotImplementedException();
        }

        // Deserialize the settings, get the file to deserialize from the StoreData Object
        public bool LoadData()
        {
            throw new NotImplementedException();
        }

        // Serialize the settings
        ~Settings()
        {
            
        }
    }
}
