using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Windows.Storage;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Persistence
{
    /// <summary>
    /// Abstract class used for providing persistence to select Objects.
    /// Set the string _fileName to be the name of the xml file that will contain the information of the object
    /// </summary>
    abstract class Serializer<T>
    {
        private StorageFolder _storageFolder;
        protected string _fileName;
        private DataContractSerializer _serializer;
        protected T _restoredObject;

        public Serializer()
        {
            _storageFolder = ApplicationData.Current.LocalFolder;
            _serializer = new DataContractSerializer(typeof(T));
        }
        
        /// <summary>
        /// Use this method to Serialize the subclass.
        /// </summary>
        public async void Serialize()
        {
            Stream stream = await _storageFolder.OpenStreamForWriteAsync(_fileName, CreationCollisionOption.ReplaceExisting);
            _serializer.WriteObject(stream, this);
        }

        /// <summary>
        /// Use this method to Deserialize the subclass. The result will
        /// </summary>
        public async void Deserialize()
        {
            Stream stream = await _storageFolder.OpenStreamForReadAsync(_fileName);
            _restoredObject = (T)_serializer.ReadObject(stream);
        }
    }
}
