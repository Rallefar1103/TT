/********EXAMPLE OF THE URL COMMUNICATION USED IN THE AUTHORISATION **********/
//
//
////Request
//Uri Request = new Uri(
//    @"GET https://accounts.google.com/o/oauth2/auth?
//            scope=openid email&
//            redirect_uri=https://app.example.com/oauth2/callback&
//            response_type=code&
//            client_id=812741506391&
//            state=af0ifjsldkj");
////Response
//Uri Response = new Uri(
//    @"HTTP /1.1 302 Found
//            Location: https://app.example.com/oauth2/callback?
//            code=MsCeLvIaQm6bTrgtp7&state=af0ifjsldkj");

////The code returned is the authorization grant and state is to
////ensure it's not forged and it's from the same request.
////And the authorization grant for tokens response contains an ID token.");

////Request
//Uri Request = new Uri(
//    @"POST /oauth2/v3/token HTTP/1.1
//            Host: www.googleapis.com
//            Content-Type: application/x-www-form-urlencoded

//            code = MsCeLvIaQm6bTrgtp7 & client_id = 812741506391 &
//              client_secret ={ client_secret}&
//              redirect_uri=https://app.example.com/oauth2/callback&
//              grant_type=authorization_code");
////Response
//string response = @"{
//                          "access_token": "2YotnFZFEjr1zCsicMWpAA",
//                          "token_type": "Bearer",
//                          "expires_in": 3600,
//                          "refresh_token": "tGzv3JOkF0XG5Qx2TlKWIA",
//                          "id_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFlOWdkazcifQ..."
//                        }";

/******* github med sourcekode til xamarin auth   *************/
/* https://github.com/xamarin/Xamarin.Auth/blob/master/source/Core/Xamarin.Auth.Common.LinkSource/OAuth2Authenticator.cs#L845 */
 
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using Xamarin.Auth;
using Xamarin.Auth.Presenters;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication.Connection
{

    public enum Flow
    {
        Implicit,
        Authentication,
        Refresh

    }
    public class TurfTankAuth:OAuth2Authenticator
    {


        //public OAuth2Authenticator Auth2;
        ICredentials _cred; 
        public OAuthLoginPresenter Presenter;

 
        #region constructor
        /// <summary>
        /// Completed is called when a login has been attempted
        /// Error is called when something went wrong in the login lige Faulty state, wrong code recieved (access_token/ auth_code)
        /// </summary>
        /// <param name="cred"></param>
        /// <param name="flow"></param>
        public TurfTankAuth(ICredentials cred, Flow flow = Flow.Authentication):base(cred.ClientId, null, cred.Scope, new Uri(cred.AuthorizeURL), new Uri(cred.RedirectURL), new Uri(cred.AccessTokenURL), isUsingNativeUI: false)
        {
            _cred = cred;
            Presenter = new OAuthLoginPresenter();
            Completed += Handle_CompletetLogin;
            Error += Handle_LoginError;
         }
        
        #endregion constructor

        /// <summary>
        /// Purpose is to check if the Page is forgery, 
        /// the overide Handles a problem with the state allways being bad, TODO this should be fixed, maybe by securing that only 1 OAuth2Authenticator is used throughout program. 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="query"></param>
        /// <param name="fragment"></param>
        protected override void OnPageEncountered(Uri url, IDictionary<string, string> query, IDictionary<string, string> fragment)
        {
            //for at undgå stae error, lige nu passer den state som sættes automatisk ikke med den der kommer tilbage.
            if (query.ContainsKey("state"))
            {
                query.Remove("state");
            }

            if (fragment.ContainsKey("state"))
            {
                fragment.Remove("state");
            }

            base.OnPageEncountered(url, query, fragment);
        }


        /// <summary>
        /// called when the initial URL is being constructed.
        /// </summary>
        /// <param name="query"></param>
        protected override void OnCreatingInitialUrl(IDictionary<string, string> query)
        {
            base.OnCreatingInitialUrl(query);
        }

        /// <summary>
        /// Stores all tokens and authcodes in Securestorrage for quick access and the account for persistence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Handle_CompletetLogin(Object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                App.account = e.Account;
                var user = App.account.Username;
                //var request = new OAuth2Request("GET", new Uri("https://dev-ggbq2i2p.us.auth0.com/users"), null, e.Account);
                //var response = await request.GetResponseAsync();
                await SecureStorage.SetAsync("TTRA", App.account.Serialize());

                if (App.account.Properties.ContainsKey("access_token"))
                {
                    App.OAuthCredentials.AccessToken = e.Account.Properties["access_token"];
                    await SecureStorage.SetAsync("access_token", App.account.Properties["access_token"]);
                    App.WifiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", App.OAuthCredentials.AccessToken);
                }
                if (App.account.Properties.ContainsKey("refresh_token"))
                {
                    App.OAuthCredentials.RefreshToken = e.Account.Properties["refresh_token"];
                    await SecureStorage.SetAsync("refresh_token", App.account.Properties["refresh_token"]);
                }
                if (App.account.Properties.ContainsKey("code"))
                {
                    App.OAuthCredentials.AccessToken = e.Account.Properties["Authentication_code"];
                    await SecureStorage.SetAsync("Authentication_code", App.account.Properties["Authentication_code"]);
                }
            }
        }




        /// <summary>
        /// When login does not succeed it triggers an event, This function is the callback
        ///  that should handle the exceptions when i.e.
        ///  "did not recieve Authorization_code or accessToken",
        ///  "Did not succeed in retrieving Username"
        ///  "State variable does not match"
        ///  TODO Handle the errors based on response errorcodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Handle_LoginError(Object sender, AuthenticatorErrorEventArgs e)
        {
            // IMPLEMENT THIS FUNCTION!
            var exep = e.Exception;
            var message = e.Message;
            //lav en switch som checker for de forskellige error codes
            //switch (e.Message)
            //{
            //    case: 
            //    default:
            //        break;
            //}
            
                var queryValues = new Dictionary<string, string>
            {
                {"refresh_token", App.OAuthCredentials.RefreshToken},
                {"client_id", this.ClientId},
                {"grant_type", "refresh_token"}
            };

                if (!string.IsNullOrEmpty(this.ClientSecret))
                {
                    queryValues["client_secret"] = this.ClientSecret;
                }

                var tokens = await this.RequestAccessTokenAsync(queryValues)
                        .ContinueWith(result =>
                        {
                            var accountProperties = result.Result;

                            this.OnRetrievedAccountProperties(accountProperties);

                            return int.Parse(accountProperties["expires_in"]);
                        });
            
        }


        #region Refresh Token

        /// <summary>
        /// Gets a new token by showing refreshtoke
        /// </summary>
        /// <returns></returns>
        public async Task RefreshTokenAsync()
        {
            var queryValues = new Dictionary<string,string>
            {
                { "grant_type", "refresh_token"},
                { "refresh_token", _cred.RefreshToken},
                { "client_id", _cred.ClientId},
                {"client_secret", _cred.ClientSecret }

            };
            var result = await this.RequestAccessTokenAsync(queryValues);

            await UpdateTokensAsync(result);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task GetAccessTokenAsync()
        {
            var queryValues = new Dictionary<string, string>
            {
                { "code", _cred.AuthorizeCode},
                { "client_id", _cred.ClientId},
                {"client_secret", _cred.ClientSecret },
                { "grant_type", "Authorization_code"},
                {"redirect_uri", _cred.RedirectURL }

            };
            var result = await this.RequestAccessTokenAsync(queryValues);

            await UpdateTokensAsync(result);

        }



        /// <summary>
        /// Updates the the AppCredentials to match persistent storrage
        /// updates TTRA account
        /// Todo set up to be used when creating the account on the initial program startup
        /// </summary>
        /// <param name="refreshResult"></param>
        /// <returns></returns>
        public async Task UpdateTokensAsync(IDictionary<string,string> refreshResult)
        {
            string serializedString = await SecureStorage.GetAsync("TTRA");
            Account account = Account.Deserialize(serializedString);

            if (refreshResult.ContainsKey("access_token"))
            {
                App.OAuthCredentials.AccessToken = account.Properties["access_token"];
                await SecureStorage.SetAsync("access_token", refreshResult["access_token"]);
            }
            if (refreshResult.ContainsKey("refresh_token"))
            {
                App.OAuthCredentials.RefreshToken = account.Properties["refresh_token"];
                await SecureStorage.SetAsync("refresh_token", refreshResult["refresh_token"]);
            }
            if (refreshResult.ContainsKey("code"))
            {
                App.OAuthCredentials.AccessToken = account.Properties["code"];
                await SecureStorage.SetAsync("Authentication_code", refreshResult["code"]);
            }

            await SecureStorage.SetAsync("TTRA", account.Serialize());
        }

        #endregion

        #region POST and GET Data

        /// <summary>
        /// Get text from Resource server using the Access_token
        /// </summary>
        /// <param name="fromResourceServer"></param>
        /// <param name="properties"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public async Task<string> TTGetTextAsync(Uri fromResourceServer, IDictionary<string, string> properties, Account acc)
        {
            var request = new OAuth2Request("GET", fromResourceServer, properties, acc);
            var response = await request.GetResponseAsync();
            string text = await response.GetResponseTextAsync();
            return text;

        }

        /// <summary>
        /// Puts text on Resource server using the Access_token
        /// </summary>
        /// <param name="toResourceServer"></param>
        /// <param name="properties"></param>
        /// <param name="acc"></param>
        /// <returns></returns>
        public async Task<string> TTPutDataAsync(Uri toResourceServer, IDictionary<string, string> properties, Account acc)
        {
            var request = new OAuth2Request("POST", toResourceServer, properties, acc);
            var response = await request.GetResponseAsync();
            string text = await response.GetResponseTextAsync();
            return text;
        }

        #endregion


    }
}

