using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using VisitRoskilde.Annotations;
using VisitRoskilde.FacebookIntegrationModule;
using VisitRoskilde.LocationServiceModule;
using VisitRoskilde.SettingsModule;

namespace VisitRoskilde.ViewModel
{
    class ExploreViewModel : INotifyPropertyChanged
    {
        #region Variables
        private bool _status;
        FacebookIntegration fbHandler;
        LocationServices locationHandler;
        Settings settingsHandler;
        #endregion

        #region Properties
        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool Status
        {
            get { return settingsHandler.LocationServicedEnabled; }
            private set { settingsHandler.LocationServicedEnabled = value; }
        }
        #endregion
        #region Methods
        #region Constructor

        public ExploreViewModel()
        {
            FacebookIntegration fbHandler = FacebookIntegration.GetInstance();
            LocationServices locationHandler = new LocationServices();
            Settings settingsHandler = new Settings();


        }
        #endregion
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
