using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using VisitRoskilde.Interfaces;
using VisitRoskilde.Persistence;

namespace VisitRoskilde.DataCollectionModule
{
    [DataContract]
    class DataDelivery : Serializer<DataDelivery>
    {
        [DataMember]
        private List<IDataCollectable> _collectables;

        public DataDelivery()
        {
            _fileName = "dataCollection.xml";
        }

        public List<IDataCollectable> Collectables
        {
            get { return _collectables; }
            set { _collectables = value; }
        }

        private async void UploadData()
        {
            StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(_fileName);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://f0cus.myds.me/visitRoskilde/datacollector.php");
            request.Method = "POST";
            Stream stream = await storageFile.OpenStreamForWriteAsync();
            //AsyncCallback callback = new AsyncCallback();
            //request.BeginGetRequestStream();
            //HttpWebResponse resp = request.BeginGetResponse() as HttpWebResponse;
            stream.Dispose();
            // And all is happy with the uploaded data.
        }

        ~DataDelivery()
        {
            Serialize();
            UploadData();
        }
    }
}
