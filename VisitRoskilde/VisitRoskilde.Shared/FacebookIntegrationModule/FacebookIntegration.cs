using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;
using System.Text;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Facebook;
using VisitRoskilde.Interfaces;

namespace VisitRoskilde.FacebookIntegrationModule
{
    [DataContract]
    class FacebookIntegration: ILoad, IDataCollectable
    {
        #region Variables
        //AppID for testing app (localhost)
        private string _fbAppIdTEST = "306430699547145";
        //AppID for live app (https://f0cus.myds.me/visitRoskilde/)
        private string _fbAppId = "304738753049673";
        string _fbScope = "user_about_me,read_stream,publish_stream";
        [DataMember]
        private string _userName;
        private BitmapImage _userProfilePicture;
        [DataMember]
        private string _userHomeCity;
        [DataMember]
        private string _userGender;
        [DataMember]
        private int _userAge;
        private bool _status;
        FacebookClient _fb = new FacebookClient();
        #endregion

        #region Properties
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public BitmapImage UserProfilePicture
        {
            get { return _userProfilePicture; }
            set { _userProfilePicture = value; }
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
        #endregion

        #region Methods
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

        public async void LogIn()
        {
            var redirectUrl = "https://www.facebook.com/connect/login_success.html";
            try
            {
                //fb.AppId = facebookAppId;
                var loginUrl = _fb.GetLoginUrl(new
                {
                    client_id = _fbAppId,
                    redirect_uri = redirectUrl,
                    scope = _fbScope,
                    display = "popup",
                    response_type = "token"
                });

                var endUri = new Uri(redirectUrl);

                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                        WebAuthenticationOptions.None,
                                                        loginUrl,
                                                        endUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var callbackUri = new Uri(WebAuthenticationResult.ResponseData.ToString());
                    var facebookOAuthResult = _fb.ParseOAuthCallbackUrl(callbackUri);
                    var accessToken = facebookOAuthResult.AccessToken;
                    if (String.IsNullOrEmpty(accessToken))
                    {
                        // User is not logged in, they may have canceled the login
                    }
                    else
                    {
                        // User is logged in and token was returned
                        LoginSucceded(accessToken);
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
                throw ex;
            }
        }

        private async void LoginSucceded(string accessToken)
        {
            dynamic parameters = new ExpandoObject();
            parameters.access_token = accessToken;
            parameters.fields = "id";

            dynamic result = await _fb.GetTaskAsync("me", parameters);
            parameters = new ExpandoObject();
            parameters.id = result.id;
            parameters.access_token = accessToken;
            string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", _fb.AppId, "large", _fb.AccessToken);

            UserProfilePicture = new BitmapImage(new Uri(profilePictureUrl));
            UserName = result.name;

            //TODO: Add something here to tell the user he is logged in
            MessageDialog fbLogMDialog = new MessageDialog("You are logged into Facebook now.");

        }
        #endregion

    }
}
