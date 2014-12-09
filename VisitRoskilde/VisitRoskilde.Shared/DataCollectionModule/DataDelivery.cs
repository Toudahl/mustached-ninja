using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.DataCollectionModule
{
    [DataContract]
    class DataDelivery : Serializer<DataDelivery>
    {
        public DataDelivery()
        {
            _fileName = "dataCollection.xml";
        }
        public bool SaveData(List<IDataCollectable> collectedInformation)
        {
            throw new NotImplementedException();
        }

        ~DataDelivery()
        {
            Serialize();
        }
    }
}
