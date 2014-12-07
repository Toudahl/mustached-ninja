using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.DataCollection
{
    [Serializable]
    class DataDelivery
    {
        public bool SaveData(List<IDataCollectable> collectedInformation)
        {
            throw new NotImplementedException();
        }

        ~DataDelivery()
        {
            // Serialize the list.
        }
    }
}
