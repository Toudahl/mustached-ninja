using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Windows.Storage;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Persistence
{
    // Author: Morten Toudahl

    /// <summary>
    /// Abstract class used for providing persistence to select Objects.
    /// Set the string _fileName to be the name of the xml file that will contain the information of the object
    /// </summary>
    [DataContract]
    abstract class Serializer<T>
    {
        private StorageFolder _storageFolder;
        protected string _fileName;
        private DataContractSerializer _serializer;
        protected T _restoredObject;

        protected Serializer()
        {
            _storageFolder = ApplicationData.Current.LocalFolder;
            _serializer = new DataContractSerializer(typeof(T));
        }
        
        /// <summary>
        /// Use this method to Serialize the subclass.
        /// </summary>
        protected async void Serialize()
        {
            using (Stream stream = await _storageFolder.OpenStreamForWriteAsync(_fileName, CreationCollisionOption.ReplaceExisting))
            {
                _serializer.WriteObject(stream, this);
            }
            // TODO: Make a failed save throw an exception
        }

        /// <summary>
        /// Use this method to Deserialize the subclass. The result will
        /// </summary>
        protected async void Deserialize()
        {
            try
            {
                using (Stream stream = await _storageFolder.OpenStreamForReadAsync(_fileName))
                {
                    _restoredObject = (T)_serializer.ReadObject(stream);
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
