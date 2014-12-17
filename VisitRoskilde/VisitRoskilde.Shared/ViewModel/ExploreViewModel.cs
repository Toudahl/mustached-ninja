using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
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
        FacebookIntegration fbHandler = FacebookIntegration.GetInstance();
        LocationServices locationHandler;
        Settings settingsHandler;
        //ICommands
        private ICommand _fbLoginCommand;
        private ICommand _updateuser;
        //UserInformation
        private bool _status;
        private ObservableCollection<string> _fbUserName;
        private ObservableCollection<BitmapImage> _fbUserProfilePicture;
        private ObservableCollection<string> _fbUserAge;
        private ObservableCollection<string> _fbUserGender;
        private ObservableCollection<string> _fbUserHomeCity;
        private ObservableCollection<FacebookLocationModel> _listofLocations;
        private FacebookLocationModel _selectedLocation;

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

        public ObservableCollection<string> fbUserName
        {
            get
            {   return fbHandler.UserName;  }
            set
            {
                fbHandler.UserName = value;
                OnPropertyChanged("fbUserName");
            }
        }

        public ObservableCollection<BitmapImage> fbUserProfilePicture
        {
            get {
                if (fbHandler.UserProfilePicture != null)
                {
                    _fbUserProfilePicture.Add(new BitmapImage(new Uri(fbHandler.UserProfilePicture[0])));
                    return _fbUserProfilePicture;
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

        public ObservableCollection<string> fbUserHomeCity
        {
            get
            {   return fbHandler.UserHomeCity;  }
            set
            {
                fbHandler.UserHomeCity = value;
                OnPropertyChanged("fbUserHomeCity");
            }
        }

        public ObservableCollection<string> fbUserGender
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

        public ObservableCollection<string> fbUserAge
        {
            get
            {   return fbHandler.UserAge;   }
            set
            {
                fbHandler.UserAge = value;
                OnPropertyChanged("fbUserAge");
            }
        }

        public ObservableCollection<FacebookLocationModel> ListofLocations
        {
            get { return fbHandler.fbLocation; }
            set
            {
                fbHandler.fbLocation = value; 
                OnPropertyChanged("ListofLocations");
            }
        }

        public FacebookLocationModel SelectedLocation
        {
            get
            {
                    return _selectedLocation;
            }
            set
            {
                _selectedLocation = value; 
                OnPropertyChanged("SelectedLocation");
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
            fbUserProfilePicture = new ObservableCollection<BitmapImage>();
        }
        #endregion

        public void fbLogin()
        {
            fbHandler.LogIn();
        }

        public void updateuser()
        {
            try
            {
                fbUserName = fbHandler.UserName;
                fbUserGender = fbHandler.UserGender;
                fbUserHomeCity = fbHandler.UserHomeCity;
                fbUserProfilePicture.Add(new BitmapImage(new Uri(fbHandler.UserProfilePicture[0])));
                OnPropertyChanged("fbUserProfilePicture");
                OnPropertyChanged("fbUserName");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void updateLocations()
        {
            try
            {
                _listofLocations = fbHandler.fbLocation;
            }
            catch (Exception)
            {
                updateLocations();
                throw;
            }
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
