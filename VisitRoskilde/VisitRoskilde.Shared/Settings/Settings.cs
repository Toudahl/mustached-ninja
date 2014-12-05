using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Settings
{
    class Settings: ISave, ILoad
    {
        // Serialize the settings 
        public bool SaveData()
        {
            throw new NotImplementedException();
        }

        // Deserialize the settings
        public bool LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
