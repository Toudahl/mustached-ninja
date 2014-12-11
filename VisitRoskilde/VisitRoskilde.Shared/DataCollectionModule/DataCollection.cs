using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.DataCollectionModule
{
    class DataCollection
    {
        private static DataCollection obj;
        private List<IDataCollectable> collectedInformation;

        private DataCollection()
        {
            collectedInformation = new List<IDataCollectable>();
            // Check Settings Object for permissions
        }

        public static DataCollection GetInstance()
        {
            if (obj == null)
            {
                obj = new DataCollection();
            }
            return obj;
        }

        public void Collect(IDataCollectable collectable)
        {
            collectedInformation.Add(collectable);
        }

        // Send data to the DataDelivery Object
        ~DataCollection()
        {
            var delivery = new DataDelivery {Collectables = collectedInformation};
        }
    }
}
