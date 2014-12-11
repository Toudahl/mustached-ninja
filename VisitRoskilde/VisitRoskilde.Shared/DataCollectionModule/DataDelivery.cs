using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
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

        /// <summary>
        /// This code will be used to Upload the Data to a data collector.
        /// </summary>
        private async void UploadData()
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(_fileName);
                Stream fileStream = await file.OpenStreamForReadAsync();
                Uri resourceAddress = new Uri("Http://f0cus.myds.me/visitRoskilde/datacollector.php");
                HttpContent content = new StreamContent(fileStream);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                HttpResponseMessage wcfResponse = await httpClient.PostAsync(resourceAddress, content);
            }
            catch (HttpRequestException hre)
            {
                //NotifyUser("Error:" + hre.Message);
            }
            catch (TaskCanceledException)
            {
                //NotifyUser("Request canceled.");
            }
            catch (Exception ex)
            {
                //NotifyUser(ex.Message);
            }
            finally
            {
                if (httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }
            } 
        }

        ~DataDelivery()
        {
            Serialize();
            //UploadData();
        }
    }
}
