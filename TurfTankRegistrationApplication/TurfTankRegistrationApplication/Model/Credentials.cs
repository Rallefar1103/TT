using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth.OAuth2;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

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

    public enum SelectedAuthServer
    {
        testserver,
        productionserver,
        developmentserver,
    }

    public class Credentials : ICredentials
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

        class AuthTestCredentials : ICredentials
        {
            public string ClientId { get; set; } // //"OT3kqkpjDvmSZl7WMjvwo4U72k1MWVUw";//
            public string ClientSecret { get; set; }
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
            public bool IsLoggedIn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public AuthTestCredentials()
            {
                ClientId = GetSecret("Test", "ClientId");
                ClientSecret = GetSecret("Test", "ClientSecret");
            }
            

            #region Private testAttributes

            private string _accessToken;
            private string _refreshToken;
            string _oAuthState = "qwertyasdfgzxcvbo";
            string _redirectURL = "dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback";//"cloud.turftank.osm://login-callback";//

            #endregion
        }

        class AuthTurfTankCredentials:ICredentials
        {
            //IsLoggedIn Skal ændres til false når databasen ikke længere anerkender access_token
            //så skal refreshtoken sendes til auth0 for at få et nyt access-token 
            public bool IsLoggedIn { get; set; } = false;

            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string Domain { get; set; } = "auth.turftank.cloud"; //"dev-ggbq2i2p.us.auth0.com";   //
            public string AuthorizeURL { get => $"https://{Domain}/authorize"; }      //"https://dev-ggbq2i2p.us.auth0.com/authorize";//"https://auth.turftank.cloud/oauth/authorize";//?audience=https://auth.turftank.cloud/u/login";
            public string RedirectURL { get => _redirectURL; }
            public string AccessTokenURL { get => $"https://{Domain}/token"; }          //"https://dev-ggbq2i2p.us.auth0.com/oauth/token";//"https://auth.turftank.cloud/token";

            public string Scope { get; set; } = "openid profile email offline_access";
            public string AccessToken { get => _accessToken; set { _accessToken = value; IsLoggedIn = true; } }
            public DateTime AccessTokenExpiration { get; set; }
            public string RefreshToken { get => _refreshToken; set => _refreshToken = value; }
            public string AuthorizeCode { get; set; }
            public string OAuthState { get => _oAuthState; set => _oAuthState = value; }

            public AuthTurfTankCredentials()
            {
                ClientId = GetSecret("Production", "ClientId");
                ClientSecret = GetSecret("Production", "ClientSecret");
            }

            #region Private Turftank Attributes

            private string _accessToken;
            private string _refreshToken;
            string _oAuthState = "qwertyasdfgzxcvbo";
            string _redirectURL = "cloud.turftank.osm://login-callback";//"dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback";//"

            #endregion
        }

        /// <summary>
        /// TODO: ER IKKE SAT OP KORREKT SOM DET STÅR NU!!!
        /// </summary>
        class AuthDevelopmentCredentials:ICredentials
        {
            //IsLoggedIn Skal ændres til false når databasen ikke længere anerkender access_token
            //så skal refreshtoken sendes til auth0 for at få et nyt access-token 
            public bool IsLoggedIn { get; set; }

            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string Domain { get; set; } = "turftank-dev.eu.auth0.com";
            public string AuthorizeURL { get => $"https://{Domain}/authorize"; }
            public string RedirectURL { get => _redirectURL; }
            public string AccessTokenURL { get => $"https://{Domain}/token"; }

            public string Scope { get; set; } = "openid profile email offline_access";
            public string AccessToken { get => _accessToken; set { _accessToken = value; IsLoggedIn = true; } }
            public DateTime AccessTokenExpiration { get; set; }
            public string RefreshToken { get => _refreshToken; set => _refreshToken = value; }
            public string AuthorizeCode { get; set; }
            public string OAuthState { get => _oAuthState; set => _oAuthState = value; }


            public AuthDevelopmentCredentials()
            {
                ClientId = GetSecret("Development", "ClientId");
                ClientSecret = GetSecret("Development", "ClientSecret");
                IsLoggedIn = false;
            }

            #region Private Turftank Attributes

            private string _accessToken;
            private string _refreshToken;
            string _oAuthState = "qwertyasdfgzxcvbo";
            string _redirectURL = "cloud.turftank.sw3://login-callback";//"dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback";//"

            #endregion
        }

        //IsLoggedIn Skal ændres til false når databasen ikke længere anerkender access_token
        //så skal refreshtoken sendes til auth0 for at få et nyt access-token 
        public bool IsLoggedIn { get; set; }

        public string ClientId { get; set; } 
        public string ClientSecret { get; set; } 
        public string Domain { get; set; } 
        public string AuthorizeURL { get => $"https://{Domain}/authorize"; }      
        public string RedirectURL { get => _redirectURL; }
        public string AccessTokenURL { get => $"https://{Domain}/oauth/token"; }        

        public string Scope { get; set; } 
        public string AccessToken { get => _accessToken; set { _accessToken = value; IsLoggedIn = true; } }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get => _refreshToken; set => _refreshToken = value; }
        public string AuthorizeCode { get; set; }
        public string OAuthState { get => _oAuthState; set => _oAuthState = value; }


        #endregion

        #region Private testAttributes

        private string _accessToken;
        private string _refreshToken;
        string _oAuthState;
        string _redirectURL;

        #endregion


        #region Constructor

        public void Initialize(
            string clientid, string clientsecret, string domain, string scope, string accesstoken,
            string accesstokenexpiration, string refreshtoken, string authorizecode, string state, string redirecturl)
        {
            //IsLoggedIn = false;

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

        public void Initialize(ICredentials cred)
        {
            Initialize(cred.ClientId, cred.ClientSecret, cred.Domain, cred.Scope, cred.AccessToken,
                       cred.AccessTokenExpiration.ToString(), cred.RefreshToken, cred.AuthorizeCode, state: "", cred.RedirectURL);
        }

        //constructors 
        public Credentials(ICredentials cred)
        {
            Initialize(cred);
        }

        public Credentials(SelectedAuthServer selectedAuthServer)
        {
            switch (selectedAuthServer)
            {
                case SelectedAuthServer.testserver:
                    Initialize(new AuthTestCredentials());
                    break;

                case SelectedAuthServer.productionserver:
                    Initialize(new AuthTurfTankCredentials());
                    break;

                case SelectedAuthServer.developmentserver:
                    Initialize(new AuthDevelopmentCredentials());
                    break;

                default:
                    Initialize(new AuthTestCredentials());
                    break;
            }
        }

        public Credentials(string clientid, string clientsecret, string domain, string scope, string accesstoken,
            string accesstokenexpiration, string refreshtoken, string authorizecode, string state, string redirecturl)
        {
            Initialize(clientid, clientsecret, domain, scope, accesstoken,
                       accesstokenexpiration, refreshtoken, authorizecode, state, redirecturl);
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
        #endregion

        #region Private Methods

        private static string GetSecret(string secretScope, string secretId)
        {
            string secret = "No Secret";
            if(secretScope == "Test")
            {
                secret = secretId == "ClientSecret" ? CredentialKeys.TestClientSecret : CredentialKeys.TestClientId;
            }
            else if (secretScope == "Development")
            {
                secret = secretId == "ClientSecret" ? CredentialKeys.DevelopmentClientSecret : CredentialKeys.DevelopmentClientId;
            }
            else if (secretScope == "Production")
            {
                secret = secretId == "ClientSecret" ? CredentialKeys.ProductionClientSecret : CredentialKeys.ProductionClientId;
            }
            return secret;
        }


        #endregion Private Methods
    }
}







