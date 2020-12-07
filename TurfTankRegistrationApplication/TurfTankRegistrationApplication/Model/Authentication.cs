//        //Request
//        Uri Request = new Uri(
//            @"GET https://accounts.google.com/o/oauth2/auth?
//            scope=openid email&
//            redirect_uri=https://app.example.com/oauth2/callback&
//            response_type=code&
//            client_id=812741506391&
//            state=af0ifjsldkj");
//        //Response
//        Uri Response = new Uri(
//            @"HTTP /1.1 302 Found
//            Location: https://app.example.com/oauth2/callback?
//            code=MsCeLvIaQm6bTrgtp7&state=af0ifjsldkj");

//        //The code returned is the authorization grant and state is to
//        //ensure it's not forged and it's from the same request.
//        //And the authorization grant for tokens response contains an ID token.");

//        //Request
//        Uri Request = new Uri(
//            @"POST /oauth2/v3/token HTTP/1.1
//            Host: www.googleapis.com
//            Content-Type: application/x-www-form-urlencoded

//            code = MsCeLvIaQm6bTrgtp7 & client_id = 812741506391 &
//              client_secret ={ client_secret}&
//              redirect_uri=https://app.example.com/oauth2/callback&
//              grant_type=authorization_code");
////Response
//  string response =  @"{
//                          "access_token": "2YotnFZFEjr1zCsicMWpAA",
//                          "token_type": "Bearer",
//                          "expires_in": 3600,
//                          "refresh_token": "tGzv3JOkF0XG5Qx2TlKWIA",
//                          "id_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjFlOWdkazcifQ..."
//                        }";
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication.Connection
{

    public enum Flow
    {
        Implicit,
        Authentication,
        Refresh

    }
    public class TurfTankAuth
    {


        public OAuth2Authenticator Auth2;
        ICredentials _cred = new Constants();
        



        public OAuth2Authenticator InstantiateAuthenticator(Flow flow)
        {
            switch (flow)
            {
                case Flow.Implicit:
                    Auth2 = new OAuth2Authenticator(_cred.ClientId, _cred.Scope, new Uri(_cred.AuthorizeURL), new Uri(_cred.RedirectURL), isUsingNativeUI: true);
                    break;
                case Flow.Authentication:
                case Flow.Refresh:
                    Auth2 = new OAuth2Authenticator(_cred.ClientId, _cred.ClientSecret, _cred.Scope, new Uri(_cred.AuthorizeURL), new Uri(_cred.RedirectURL), new Uri(_cred.AccessTokenURL), isUsingNativeUI: true);
                    break;
                default:
                    break;
            }
            Auth2.Completed += Handle_CompletetLogin;
            Auth2.Error += Handle_LoginError;
            return Auth2;
        }


        #region constructor
        public TurfTankAuth(ICredentials cred)
        {
            _cred = cred;
            InstantiateAuthenticator(flow: Flow.Authentication);
         }
        #endregion constructor




        async void Handle_CompletetLogin(Object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {

                Account a = e.Account;
                await SecureStorage.SetAsync("TTRA", a.Serialize());
                
                if (a.Properties.ContainsKey("access_token"))
                {
                    App.OAuthCredentials.AccessToken = e.Account.Properties["access_token"];
                    await SecureStorage.SetAsync("access_token", a.Properties["access_token"]);
                }
                if (a.Properties.ContainsKey("refresh_token"))
                {
                    App.OAuthCredentials.RefreshToken = e.Account.Properties["refresh_token"];
                    await SecureStorage.SetAsync("refresh_token", a.Properties["refresh_token"]);
                }
                if (a.Properties.ContainsKey("code"))
                {
                    App.OAuthCredentials.AccessToken = e.Account.Properties["Authentication_code"];
                    await SecureStorage.SetAsync("Authentication_code", a.Properties["Authentication_code"]);
                }
            }
        }



        //did not recieve Authorization_code or accessToken, 
        //Did not succeed in retrieving Username 
        void Handle_LoginError(Object sender, AuthenticatorErrorEventArgs e)
        {
            var exep = e.Exception;
            var message = e.Message;
        }


        #region Refresh Token

        /// <summary>
        /// Gets a new token by showing refreshtoken
        /// </summary>
        /// <returns></returns>
        public async Task RefreshTokenAsync()
        {
            var queryValues = new Dictionary<string,string>
            {
                { "refresh_token", _cred.RefreshToken},
                { "client_id", _cred.ClientId},
                { "grant_type", "refresh_token"},
                {"client_secret", _cred.ClientSecret }

            };
            var result = await Auth2.RequestAccessTokenAsync(queryValues);

            await UpdateTokensAsync(result);

        }


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
                App.OAuthCredentials.AccessToken = account.Properties["Authentication_code"];
                await SecureStorage.SetAsync("Authentication_code", refreshResult["Authentication_code"]);
            }

            await SecureStorage.SetAsync("TTRA", account.Serialize());
        }

        #endregion

        #region POST and GET Data


        public async Task<string> TTGetTextAsync(Uri fromResourceServer, IDictionary<string, string> properties, Account acc)
        {
            var request = new OAuth2Request("GET", fromResourceServer, properties, acc);
            var response = await request.GetResponseAsync();
            string text = await response.GetResponseTextAsync();
            return text;

        }


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

