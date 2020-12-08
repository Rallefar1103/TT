using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth.OAuth2;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication.Model
{
    public interface ICredentials
    {

        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string Domain { get; set; }
        string AuthorizeURL { get; }
        string RedirectURL { get; }
        string AccessTokenURL { get; }
        string Scope { get; set; }
        string AccessToken { get; set; }
        DateTime AccessTokenExpiration { get; set; }
        string RefreshToken { get; set; }
        string AuthorizeCode { get; set; }
        string OAuthState { get; set; }
        bool IsLoggedIn { get; set; }


    }

    public class Constants : ICredentials
    {
        /* ********* VIGTIGT ******* */
        //dette bruges hvis OAuth2Authenticator isNativeUI = false

        /* hvis Redirect eller Domain Ændres så skal IntentFilter i MainActivity.cs også ændres

         [IntentFilter(new[] { Android.Content.Intent.ActionView },
          Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable},

  Redirect appname  -> DataScheme     = "dk.turftank.turftankregistrationapplication",
             Domain -> DataHost       = "dev-ggbq2i2p.us.auth0.com",
           Redirect -> DataPathPrefix = "/android/dk.turftank.turftankregistrationapplication/callback",
                       AutoVerify     = true)]
        */



        /// <summary>
        /// vores test auth0 server
        /// Bruger test@saxjax.dk
        /// kode Hyg12345
        /// </summary>
        #region Public testAttributes


        //IsLoggedIn Skal ændres til false når databasen ikke længere anerkender access_token
        //så skal refreshtoken sendes til auth0 for at få et nyt access-token 
        public bool IsLoggedIn { get; set; } = false;

        public string ClientId { get; set; } = "X5goHHBzVSkwc8kwWpbw5fq9NzoVkAof";// //"OT3kqkpjDvmSZl7WMjvwo4U72k1MWVUw";//
        public string ClientSecret { get; set; } = "KAaqom27hUCbyhUr1KtAO1o-ooxLJUOHn2o6YPmX0OopbD4FjgZnzVzhHynF8SM8";
        public string Domain { get; set; } = "dev-ggbq2i2p.us.auth0.com";   //"auth.turftank.cloud"; //
        public string AuthorizeURL { get => $"https://{Domain}/authorize"; }      //"https://dev-ggbq2i2p.us.auth0.com/authorize";//"https://auth.turftank.cloud/oauth/authorize";//?audience=https://auth.turftank.cloud/u/login";
        public string RedirectURL { get => _redirectURL; }
        public string AccessTokenURL { get => $"https://{Domain}/oauth/token"; }          //"https://dev-ggbq2i2p.us.auth0.com/oauth/token";//"https://auth.turftank.cloud/token";

        public string Scope { get; set; } = "openid profile email offline_access";
        public string AccessToken { get => _accessToken; set { _accessToken = value; IsLoggedIn = true; } }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get => _refreshToken; set => _refreshToken = value; }
        public string AuthorizeCode { get; set; }
        public string OAuthState { get => _oAuthState; set => _oAuthState = value; }


        #endregion

        #region Private testAttributes

        private string _accessToken;
        private string _refreshToken;
        string _oAuthState = "qwertyasdfgzxcvbo";
        string _redirectURL = "dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback";//"cloud.turftank.osm://login-callback";//

        #endregion


        /// <summary>
        /// Turftank Production Credentials, copied from OSM app
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="clientsecret"></param>
        /// <param name="domain"></param>
        /// <param name="scope"></param>
        /// <param name="accesstoken"></param>
        /// <param name="accesstokenexpiration"></param>
        /// <param name="refreshtoken"></param>
        /// <param name="authorizecode"></param>
        /// <param name="state"></param>
        /// <param name="redirecturl"></param>
        //#region Public Turftank Attributes


        ////IsLoggedIn Skal ændres til false når databasen ikke længere anerkender access_token
        ////så skal refreshtoken sendes til auth0 for at få et nyt access-token 
        //public bool IsLoggedIn { get; set; } = false;

        //public string ClientId { get; set; } = "OT3kqkpjDvmSZl7WMjvwo4U72k1MWVUw";//"X5goHHBzVSkwc8kwWpbw5fq9NzoVkAof";// //
        //public string ClientSecret { get; set; } = "KAaqom27hUCbyhUr1KtAO1o-ooxLJUOHn2o6YPmX0OopbD4FjgZnzVzhHynF8SM8";
        //public string Domain { get; set; } = "auth.turftank.cloud"; //"dev-ggbq2i2p.us.auth0.com";   //
        //public string AuthorizeURL { get => $"https://{Domain}/authorize"; }      //"https://dev-ggbq2i2p.us.auth0.com/authorize";//"https://auth.turftank.cloud/oauth/authorize";//?audience=https://auth.turftank.cloud/u/login";
        //public string RedirectURL { get => _redirectURL; }
        //public string AccessTokenURL { get => $"https://{Domain}/token"; }          //"https://dev-ggbq2i2p.us.auth0.com/oauth/token";//"https://auth.turftank.cloud/token";

        //public string Scope { get; set; } = "openid profile email offline_access";
        //public string AccessToken { get => _accessToken; set { _accessToken = value; IsLoggedIn = true; } }
        //public DateTime AccessTokenExpiration { get; set; }
        //public string RefreshToken { get => _refreshToken; set => _refreshToken = value; }
        //public string AuthorizeCode { get; set; }
        //public string OAuthState { get => _oAuthState; set => _oAuthState = value; }


        //#endregion

        //#region Private Turftank Attributes

        //private string _accessToken;
        //private string _refreshToken;
        //string _oAuthState = "qwertyasdfgzxcvbo";
        //string _redirectURL = "cloud.turftank.osm://login-callback";//"dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback";//"

        //#endregion

        #region Constructor

        public void Initialize(
            string clientid, string clientsecret, string domain, string scope, string accesstoken,
            string accesstokenexpiration, string refreshtoken, string authorizecode, string state, string redirecturl)
        {
            IsLoggedIn = false;

            ClientId = clientid;
            ClientSecret = clientsecret;
            Domain = domain;

            Scope = scope;
            _accessToken = accesstoken;
            AccessTokenExpiration = Convert.ToDateTime(accesstokenexpiration);
            RefreshToken = refreshtoken;
            AuthorizeCode = authorizecode;

            _oAuthState = state;
            _redirectURL = redirecturl;
        }


        public Constants(string clientid, string clientsecret, string domain, string scope, string accesstoken,
            string accesstokenexpiration, string refreshtoken, string authorizecode, string state, string redirecturl)
        {
            Initialize(clientid, clientsecret, domain, scope, accesstoken,
                       accesstokenexpiration, refreshtoken, authorizecode, state, redirecturl);
        }

        //alle constants er initialiserede
        public Constants()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Restores all values to the values saved in SecureStorrage
        /// The values that are commented out may or may not be usefull at a later time.
        /// </summary>
        /// <returns></returns>
        public async Task Restore()
        {
            //string clientid =  await SecureStorage.GetAsync("client_id"); 
            //string clientsecret = await SecureStorage.GetAsync("client_secret"); 
            //string domain = await SecureStorage.GetAsync("domain"); 
            //string scope = await SecureStorage.GetAsync("scope"); 
            string accesstoken = await SecureStorage.GetAsync("access_token");

            string accesstokenexpiration = await SecureStorage.GetAsync("access_token_expiration");
            string refreshtoken =await SecureStorage.GetAsync("refresh_token"); 
            string authorizecode =await SecureStorage.GetAsync("authrize_code"); 
            string state =await SecureStorage.GetAsync("state");
            //string redirecturl = await SecureStorage.GetAsync("redirect_url");

            Initialize(ClientId, ClientSecret, Domain, Scope, accesstoken,
                       accesstokenexpiration, refreshtoken, authorizecode, state, RedirectURL);
        }

        /// <summary>
        /// Saves all values in secure storrage
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            await SecureStorage.SetAsync("client_id",ClientId);
            await SecureStorage.SetAsync("client_secret", ClientSecret ?? "");
            await SecureStorage.SetAsync("domain", Domain);
            await SecureStorage.SetAsync("scope", Scope ?? "");
            await SecureStorage.SetAsync("access_token", AccessToken ?? "");

            await SecureStorage.SetAsync("access_token_expiration", AccessTokenExpiration.ToString());
            await SecureStorage.SetAsync("refresh_token", RefreshToken ?? "");
            await SecureStorage.SetAsync("authrize_code", AuthorizeCode ?? "");
            await SecureStorage.SetAsync("state", OAuthState ?? "");
            await SecureStorage.SetAsync("redirect_url", RedirectURL);           
        }
    }
    #endregion



   


}







