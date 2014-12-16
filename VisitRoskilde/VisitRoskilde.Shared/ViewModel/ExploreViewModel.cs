using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Imaging;
using TouristAppV4.Common;
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
        FacebookIntegration fbHandler = FacebookIntegration.GetInstance();
        LocationServices locationHandler;
        Settings settingsHandler;
        private ICommand _fbLoginCommand;
        private ICommand _updateuser;
        private string _fbUserName;
        private BitmapImage _fbUserProfilePicture;
        private string _fbUserAge;
        private string _fbUserGender;
        private string _fbUserHomeCity;
        #endregion

        #region Properties
        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool Status
        {
            get { return fbHandler.Status; }
            private set { fbHandler.Status = value; }
        }

        public string fbUserName
        {
            get
            {   return fbHandler.UserName;  }
            set
            {
                fbHandler.UserName = value;
                OnPropertyChanged("fbUserName");
            }
        }

        public BitmapImage fbUserProfilePicture
        {
            get {
                if (_fbUserProfilePicture == null)
                {
                    return new BitmapImage(new Uri(fbHandler.UserProfilePicture));
                }
                else
                {
                    return _fbUserProfilePicture;
                }
            }
            set
            {
                _fbUserProfilePicture = value;
                OnPropertyChanged("fbUserProfilePicture");
            }
        }

        public string fbUserHomeCity
        {
            get
            {   return fbHandler.UserHomeCity;  }
            set
            {
                fbHandler.UserHomeCity = value;
                OnPropertyChanged("fbUserHomeCity");
            }
        }

        public string fbUserGender
        {
            get
            {
                    return fbHandler.UserGender;
            }
            set
            {
                fbHandler.UserGender = value;
                OnPropertyChanged("fbUserGender");
            }
        }

        public string fbUserAge
        {
            get
            {   return fbHandler.UserAge;   }
            set
            {
                fbHandler.UserAge = value;
                OnPropertyChanged("fbUserAge");
            }
        }

        public ICommand fbLoginCommand
        {
            get { return _fbLoginCommand; }
            set { _fbLoginCommand = value; }
        }
        public ICommand UpdateUserCommand
        {
            get { return _updateuser; }
            set { _updateuser = value; }
        }
        #endregion
        #region Methods
        #region Constructor
        public ExploreViewModel()
        {
            LocationServices locationHandler = new LocationServices();
            _fbLoginCommand = new RelayCommand(fbLogin);
            _updateuser = new RelayCommand(updateuser);
        }
        #endregion

        public void fbLogin()
        {
            fbUserProfilePicture = new BitmapImage(new Uri("http://i367.photobucket.com/albums/oo117/unclk/th_loading-gif-animation.gif"));
            fbHandler.LogIn();
        }

        public void updateuser()
        {
            fbUserName = fbHandler.UserName;
            fbUserGender = fbHandler.UserGender;
            fbUserHomeCity = fbHandler.UserHomeCity;
            fbUserProfilePicture = new BitmapImage(new Uri(fbHandler.UserProfilePicture));
        }
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
