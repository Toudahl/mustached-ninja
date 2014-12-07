using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Persistence
{
    class StoreData: ISave, ILoad
    {
        // Make this object a singleton, and make it store settings etc as a 'File' or something
        // This will ensure that we do not have multiple instances of a Settings Object, with different settings.
        private StoreData()
        {
            
        }

        public bool SaveData()
        {
            throw new NotImplementedException();
        }

        public bool LoadData()
        {
            throw new NotImplementedException();
        }
    }
}
