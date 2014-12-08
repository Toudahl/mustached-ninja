using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Windows.Storage;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Persistence
{
    abstract class Serializer<T>
    {
        private StorageFolder _storageFolder;
        protected string _fileName;
        private DataContractSerializer _serializer;
        protected T _restoredObject;

        /// <summary>
        /// Input the filename in the constructor.
        /// </summary>
        /// <param name="fileName">file.xml</param>
        public Serializer()
        {
            _storageFolder = ApplicationData.Current.LocalFolder;
            _serializer = new DataContractSerializer(typeof(T));
        }


        public async void Serialize(T objectForSaving)
        {
            Stream stream = await _storageFolder.OpenStreamForWriteAsync(_fileName, CreationCollisionOption.ReplaceExisting);
            _serializer.WriteObject(stream, objectForSaving);
        }

        public async void Deserialize()
        {
            Stream stream = await _storageFolder.OpenStreamForReadAsync(_fileName);
            _restoredObject = (T)_serializer.ReadObject(stream);
        }
    }
}
