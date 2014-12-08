using System;
using System.Collections.Generic;
using System.Text;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.Persistence
{
    class Serializer<T>
    {

        public void Serialize(T objectForSaving)
        {
            // Serialize object as per instructions in the example at
            // https://github.com/SoftwareConstruction-1semester/PersistenceStoreApp
            // Make it take an object of the type <T>, and use the filename of the class for
            // the file it creates.

            // Throw a custom exception if failed.
        }

        public IMyDataPersists Deserialize(T objectForRestoring)
        {
            // Deserialize object as per instructions in the example at
            // https://github.com/SoftwareConstruction-1semester/PersistenceStoreApp
            // Make it load data from the file matching the filename of the class that requests deserilization

            return null;
        }
    }
}
