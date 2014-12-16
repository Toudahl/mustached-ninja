using System;
using System.Collections.Generic;
using System.Text;

namespace VisitRoskilde.FacebookIntegrationModule
{
    class FacebookLocationModel
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public Uri PictureUri { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
