using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.DataCollection
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
            try
            {
                Serialize(this);
            }
            catch (Exception exception)
            {
                throw new Exception("Saving the collected message failed with the following message: " + exception);
            }
        }
    }
}
