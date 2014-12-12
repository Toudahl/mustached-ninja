﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Facebook;
using Newtonsoft.Json;
using TouristAppV4.Common;
using VisitRoskilde.Annotations;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.FacebookIntegrationModule
{
    [DataContract]
    class FacebookIntegration: ILoad, IDataCollectable, INotifyPropertyChanged
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
        private string _userName;
        private BitmapImage _userProfilePicture;
        [DataMember]
        private string _userHomeCity;
        [DataMember]
        private string _userGender;
        [DataMember]
        private string _userAge;
        private bool _status;
        FacebookClient _fbClient;
        private ICommand _loginCommand;
        #endregion

        #region Properties
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public BitmapImage UserProfilePicture
        {
            get { return _userProfilePicture; }
            set
            {
                _userProfilePicture = value;
                OnPropertyChanged("UserProfilePicture");
            }
        }

        public string UserHomeCity
        {
            get { return _userHomeCity; }
            private set
            {
                _userHomeCity = value;
                OnPropertyChanged("UserHomeCity");
            }
        }

        public string UserGender
        {
            get { return _userGender; }
            private set
            {
                _userGender = value;
                OnPropertyChanged("UserGender");
            }
        }

        public string UserAge
        {
            get { return _userAge; }
            private set
            {
                _userAge = value;
                OnPropertyChanged("UserAge");
            }
        }

        /// <summary>
        /// Returns true if the user is logged in.
        /// </summary>
        public bool Status
        {
            get { return _status; }
            private set { _status = value; }
        }

        public ICommand LoginCommand
        {
            get { return _loginCommand; }
            set { _loginCommand = value; }
        }
        #endregion

        #region Methods
        #region Constructor
        public FacebookIntegration()
        {
            //Instances FacebookClient class with the proper parameters
            CreateClient();
            _loginCommand = new RelayCommand(LogIn);
        }
        #endregion
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

        public async void LogIn()
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
            try
            {
                UserProfilePicture = new BitmapImage(new Uri("http://i367.photobucket.com/albums/oo117/unclk/th_loading-gif-animation.gif"));
            }
            catch (Exception)
            {
                UserProfilePicture = null;
            }
            GetUserInformation();
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
                    UserName = (string)_result["name"];
                }
                catch (Exception)
                {
                    UserName = null;
                }
                try
                {
                    UserGender = (string)_result["gender"];
                }
                catch (Exception)
                {
                    UserGender = null;
                }
                try
                {
                    UserAge = (string)_result["age_range"]["min"];
                }
                catch (Exception)
                {
                    UserAge = null;
                }
                try
                {
                    UserHomeCity = (string)_result["hometown"]["name"];
                }
                catch (Exception)
                {
                    UserHomeCity = null;
                }
                try
                {
                    //TODO: _result.id is a very messy way to get the user id.
                    string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", _result.id, "large", _fbClient.AccessToken);
                    UserProfilePicture = new BitmapImage(new Uri(profilePictureUrl));
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
