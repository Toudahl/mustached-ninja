using Windows.UI.Xaml.Media.Imaging;

namespace VisitRoskilde.FacebookIntegrationModule
{
    class FacebookUser
    {
        private static string _userName;
        private static string _userAge;
        private static string _userGender;
        private static BitmapImage _userProfileImage;
        private static string _userHomeCity;

        public static string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public static string UserAge
        {
            get { return _userAge; }
            set { _userAge = value; }
        }

        public static string UserGender
        {
            get { return _userGender; }
            set { _userGender = value; }
        }

        public static BitmapImage UserProfileImage
        {
            get { return _userProfileImage; }
            set { _userProfileImage = value; }
        }

        public static string UserHomeCity
        {
            get { return _userHomeCity; }
            set { _userHomeCity = value; }
        }
    }
}
