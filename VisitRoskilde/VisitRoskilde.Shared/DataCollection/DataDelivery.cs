using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.DataCollection
{
    [DataContract]
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
