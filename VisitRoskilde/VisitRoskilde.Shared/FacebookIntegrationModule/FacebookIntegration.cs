using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.FacebookIntegrationModule
{
    class FacebookIntegration: ILoad
    {
        // Load information from the Settings Object
        public bool LoadData()
        {
            throw new NotImplementedException();
        }

        public bool Status { get; set; }

        public void LogIn(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}
