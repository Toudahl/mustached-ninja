using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TouristAppV4.Common;
using VisitRoskilde.Annotations;
using VisitRoskilde.SettingsModule;

namespace VisitRoskilde.ViewModel
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private Settings settings;
        private string _faceBookStatusImage;
        private ICommand _facebookCommand;
        private ICommand _saveCommand;

        public SettingsViewModel()
        {
            settings = new Settings();
            settings.LoadData();

           
            FacebookChangeStatusImage();
            FacebookChangeCommandFunction();

            _saveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            settings.SaveData();
        }

        private void FacebookChangeStatusImage()
        {
            if (settings.FacebookStatus())
            {
                FaceBookStatusImage = "/Assets/Facebook/LogOut.jpg";
            }
            else
            {
                FaceBookStatusImage = "/Assets/Facebook/LogIn.jpg";
            }
        }

        private void FacebookChangeCommandFunction()
        {
            if (settings.FacebookStatus())
            {
                _facebookCommand = new RelayCommand(settings.FacebookLogOut);
            }
            else
            {
                _facebookCommand = new RelayCommand(settings.FacebookLogIn);
            }
        }

        public bool DataCollection
        {
            get
            {
                return settings.DataCollectionAllowed;
            }
            set
            {
                settings.DataCollectionAllowed = value;
            }
        }

        public bool LocationService
        {
            get
            {
                return settings.LocationServicedEnabled;
            }
            set
            {
                settings.LocationServicedEnabled = value;
            }
        }

        public string FaceBookStatusImage
        {
            get { return _faceBookStatusImage; }
            private set { _faceBookStatusImage = value; }
        }

        public ICommand FacebookCommand
        {
            get
            {
                FacebookChangeCommandFunction();
                FacebookChangeStatusImage();
                return _facebookCommand;
            }
            set
            {
                FacebookChangeCommandFunction();
                FacebookChangeStatusImage();
                _facebookCommand = value;
            }
        }

        public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set { _saveCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
