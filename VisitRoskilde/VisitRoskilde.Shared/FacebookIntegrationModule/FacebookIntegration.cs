using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Windows.UI.Xaml.Controls;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.FacebookIntegrationModule
{
    [DataContract]
    class FacebookIntegration: ILoad, IDataCollectable
    {
        private bool _status;
        [DataMember]
        private string _userHomeCity;
        [DataMember]
        private string _userGender;
        [DataMember]
        private int _userAge;

        public bool LoadData()
        {
            throw new NotImplementedException();
        }

        public string UserHomeCity
        {
            get { return _userHomeCity; }
            private set { _userHomeCity = value; }
        }

        public string UserGender
        {
            get { return _userGender; }
            private set { _userGender = value; }
        }

        public int UserAge
        {
            get { return _userAge; }
            private set { _userAge = value; }
        }

        

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool Status
        {
            get { return _status; }
            private set { _status = value; }
        }

        /// <summary>
        /// Log in using the provided credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LogIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logs out from the currently active user.
        /// </summary>
        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
