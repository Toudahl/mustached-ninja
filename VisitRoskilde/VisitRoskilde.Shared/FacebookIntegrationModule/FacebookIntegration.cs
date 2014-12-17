using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Appointments.AppointmentsProvider;
using Windows.Devices.Geolocation;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TouristAppV4.Common;
using VisitRoskilde.Annotations;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.FacebookIntegrationModule
{
    [DataContract]
    class FacebookIntegration: ILoad, IDataCollectable
    {
        #region Variables
        //AppID for testing app (localhost)
        private string _fbClientAppIdTEST = "306430699547145";
        //AppID for live app (https://f0cus.myds.me/visitRoskilde/)
        private string _fbClientAppId = "304738753049673";
        string _fbClientFullScope = "user_about_me,read_stream,publish_stream";
        private string _shortAccessToken;
        private string _longAccessToken;
        [DataMember]
        private ObservableCollection<string> _userName;
        private ObservableCollection<string> _userProfilePicture;
        [DataMember]
        private ObservableCollection<string> _userHomeCity;
        [DataMember]
        private ObservableCollection<string> _userGender;
        [DataMember]
        private ObservableCollection<string> _userAge;
        private bool _status;
        FacebookClient _fbClient;
        private ICommand _loginCommand;
        private ObservableCollection<FacebookUser> _userInfo;
        private ObservableCollection<FacebookLocationModel> _fbLocation;
        private static FacebookIntegration _singleFacebookHandler;

        #endregion

        #region Properties

        public ObservableCollection<FacebookUser> userInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }

        public ObservableCollection<FacebookLocationModel> fbLocation
        {
            get { return _fbLocation; }
            set { _fbLocation = value; }
        }

        public ObservableCollection<string> UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
            }
        }

        public ObservableCollection<string> UserProfilePicture
        {
            get { return _userProfilePicture; }
            set
            {
                _userProfilePicture = value;
            }
        }

        public ObservableCollection<string> UserHomeCity
        {
            get { return _userHomeCity; }
            set
            {
                _userHomeCity = value;
            }
        }

        public ObservableCollection<string> UserGender
        {
            get { return _userGender; }
            set
            {
                _userGender = value;
            }
        }

        public ObservableCollection<string> UserAge
        {
            get { return _userAge; }
            set
            {
                _userAge = value;
            }
        }

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }
        #endregion

        #region Methods
        #region Constructor
        private FacebookIntegration()
        {
            //Instances FacebookClient class with the proper parameters
            CreateClient();
            //_loginCommand = new RelayCommand(LogIn);
            userInfo = new ObservableCollection<FacebookUser>();
            FacebookClient _fbMapClient = new FacebookClient();
            fbLocation = new ObservableCollection<FacebookLocationModel>();
            UserName= new ObservableCollection<string>();
            UserAge = new ObservableCollection<string>();
            UserGender = new ObservableCollection<string>();
            UserHomeCity = new ObservableCollection<string>();
            UserProfilePicture = new ObservableCollection<string>();
        }
        #endregion

        public static FacebookIntegration GetInstance()
        {
            if (_singleFacebookHandler == null)
            {
                _singleFacebookHandler = new FacebookIntegration();
            }
            return _singleFacebookHandler;
        }

        public bool LoadData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Log in using the provided credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public void LogIn(string username, string password)
        {
            // If log in fails, throw an exception. Do not catch it.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Logs out from the currently active user.
        /// </summary>
        public void LogOut()
        {
            // If log out fails thrown an exception. Do not catch it.
            throw new NotImplementedException();
        }

        public virtual FacebookClient CreateClient()
        {
            _fbClient = new FacebookClient();
            //Sets the AppId to the one we got from the developer settings (note: there are 2 values, one for the release app and one for test app)
            _fbClient.AppId = _fbClientAppId;
            //IMPORTANT: SENSITIVE INFORMATION!
            _fbClient.AppSecret = "f99bb675410c4e07c1789cda60fc0af0";
            //Automatically Serializes/Deserializes the returns from FacebookClient.Get methods
            _fbClient.SetJsonSerializers(JsonConvert.SerializeObject, JsonConvert.DeserializeObject);
            return _fbClient;
        }

        public async Task LogIn()
        {
            var redirectUrl = "https://www.facebook.com/connect/login_success.html";
            try
            {
                //Sets the login-url based on the parameters we give it
                var loginUrl = _fbClient.GetLoginUrl(new
                {
                    client_id = _fbClient.AppId,
                    redirect_uri = redirectUrl,
                    scope = _fbClientFullScope,
                    display = "popup",
                    response_type = "token"
                });

                var endUri = new Uri(redirectUrl);
                //This handles the real log-in action
                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                        WebAuthenticationOptions.None,
                                                        loginUrl,
                                                        endUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var callbackUri = new Uri(WebAuthenticationResult.ResponseData.ToString());
                    var facebookOAuthResult = _fbClient.ParseOAuthCallbackUrl(callbackUri);
                    //Gets the short access token, then that is used to get a long access token (short one is invalid after some point)
                    _shortAccessToken = facebookOAuthResult.AccessToken;
                    if (String.IsNullOrEmpty(_shortAccessToken))
                    {
                        // User is not logged in, they may have canceled the login
                    }
                    else
                    {
                        try
                        {
                            //Gets the long term Access Token that is needed for gathering and posting information
                            dynamic longAccessTokenTask = await _fbClient.GetTaskAsync("oauth/access_token", new
                            {
                                client_id = _fbClient.AppId,
                                client_secret = _fbClient.AppSecret,
                                grant_type = "fb_exchange_token",
                                fb_exchange_token = _shortAccessToken
                            });
                            _longAccessToken = longAccessTokenTask.access_token.ToString();
                            _fbClient.AccessToken = _longAccessToken;
                        }
                        catch (FacebookOAuthException)
                        {
                            throw;
                        }
                        // User is logged in and token was returned
                        LoginSucceded(_longAccessToken);
                    }
                }
                else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
                {
                    throw new InvalidOperationException("HTTP Error returned by AuthenticateAsync() : " + WebAuthenticationResult.ResponseErrorDetail.ToString());
                }
                else
                {
                    // The user canceled the authentication
                }
            }
            catch (Exception ex)
            {
                //
                // Bad Parameter, SSL/TLS Errors and Network Unavailable errors are to be handled here.
                //
                MessageDialog noInternet = new MessageDialog("We are unable to connect with Facebook at this time. Please check your internet connection and try again.");
                noInternet.ShowAsync();
            }
        }

        private async void LoginSucceded(string accessToken)
        {
            GetUserInformation();
            Status = true;
        }

        private async void GetUserInformation()
        {
            try
            {
                //This gets the information about the user
                dynamic _result = await _fbClient.GetTaskAsync("me?fields=name,picture,hometown,gender,age_range");
                //TODO: Clean these try methods up. Problem is that when value is null it breaks, these is a workaround in the SerializerSettings. We also should give loader more time to get the information.
                try
                {
                    UserName.Add((string)_result["name"]);
                    //userInfo[0].UserName = UserName;
                }
                catch (Exception)
                {
                    UserName = null;
                }
                try
                {
                    UserGender.Add((string)_result["gender"]);
                    //userInfo[0].UserGender = UserGender;
                }
                catch (Exception)
                {
                    UserGender = null;
                }
                try
                {
                    UserAge.Add((string)_result["age_range"]["min"]);
                    //userInfo[0].UserAge = UserAge;
                }
                catch (Exception)
                {
                    UserAge = null;
                }
                try
                {
                    //TODO: Facebook is not returning hometown, find out why
                    UserHomeCity.Add((string) _result["hometown"]["name"]);
                    //userInfo[0].UserHomeCity = UserHomeCity;
                }
                catch (Exception)
                {
                    UserHomeCity = null;
                }
                try
                {
                    //TODO: _result.id is a very messy way to get the user id.
                    UserProfilePicture.Clear();
                    UserProfilePicture.Add(string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", _result.id, "large", _fbClient.AccessToken));
                    //userInfo[0].UserProfileImage = UserProfilePicture;
                }
                catch (Exception)
                {
                    UserProfilePicture = null;
                }

            }
            catch (FacebookApiException ex)
            {
                // handle error message

            }
            setGeoLocator();
        }
        #endregion

        #region FacebookMapImplementation
        // default location is Roskilde Station
        static double latitude = 55.639533;
        static double longitude = 12.088805;

        private static ObservableCollection<FacebookLocationModel> locations = new ObservableCollection<FacebookLocationModel>();

        public static ObservableCollection<FacebookLocationModel> Locations
        {
            get
            {
                return locations;
            }
        }

        private static bool isRestaurantSelected = false;

        public static bool IsRestaurantSelected
        {
            get
            {
                return isRestaurantSelected;
            }
            set
            {
                isRestaurantSelected = value;
            }
        }

        async private void setGeoLocator()
        {
            Geolocator _geolocator = new Geolocator();
            CancellationTokenSource _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            // Carry out the operation
            Geoposition pos = null;

            try
            {
                // We will wait 100 milliseconds and accept locations up to 48 hours old before we give up
                pos = await _geolocator.GetGeopositionAsync(new TimeSpan(48, 0, 0), new TimeSpan(0, 0, 0, 0, 100)).AsTask(token);
            }
            catch (Exception)
            {
                // this API can timeout, so no point breaking the code flow. Use
                // default latitutde and longitude and continue on.
            }

            if (pos != null)
            {
                latitude = pos.Coordinate.Latitude;
                longitude = pos.Coordinate.Longitude;
            }
            RestaurantList();
        }

        public static FacebookLocationModel SelectedRestaurant { get; set; }

        

        private async void RestaurantList()
        {
            dynamic restaurantsTaskResult = await _fbClient.GetTaskAsync("/search", new { q = "restaurant", type = "place", center = latitude.ToString() + "," + longitude.ToString(), distance = "1000" });
            FacebookLocationModel place1 = new FacebookLocationModel();
            foreach (JProperty property in restaurantsTaskResult)
            {
                foreach (JContainer array in property)
                {
                    foreach (JContainer container in array)
                    {
                        try
                        {
                            fbLocation.Add(new FacebookLocationModel
                            {
                                Street = (string)container["location"]["street"] ?? String.Empty,
                                City = (string)container["location"]["city"] ?? String.Empty,
                                Country = (string)container["location"]["country"] ?? String.Empty,
                                Zip = (string)container["location"]["zip"] ?? String.Empty,
                                Latitude = (string)container["location"]["latitude"] ?? String.Empty,
                                Longitude = (string)container["location"]["longitude"] ?? String.Empty,

                                Category = (string)container["category"] ?? String.Empty,
                                Name = (string)container["name"] ?? String.Empty,
                                Id = (string)container["id"] ?? String.Empty,
                                PictureUri = new Uri(string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", (string)container["location"]["longtitude"], "large", _fbClient.AppId))

                                //// these properties are at the top level in the object
                                //Category = restaurant.ContainsKey("category") ? (string)restaurant["category"] : String.Empty,
                                //Name = locations.Contains("name") ? (string)locations["name"] : String.Empty,
                                //Id = locations.Contains("id") ? (string)locations["id"] : String.Empty,
                                //PictureUri = new Uri(string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", (string)restaurant["id"], "square", _fbClient.AppId))
                            });
                            int test1 = fbLocation.Count() - 1;
                            string test2 = fbLocation[fbLocation.Count() - 1].Category;
                            if (fbLocation[fbLocation.Count() - 1].Category == "Local business")
                            {
                                fbLocation[fbLocation.Count() - 1].Category = null;
                            }
                        }
                        catch (Exception)
                        {
                            
                        }    
                        
                            
                        
                    }
                    
                }
                
            }
            
        }
    }
        #endregion
}
