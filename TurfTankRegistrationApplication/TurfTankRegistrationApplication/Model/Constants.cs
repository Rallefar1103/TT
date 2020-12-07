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
        List<string> AllowedCallbackUrls { get; }
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
        //!!!!!VIGTIGT!!!!!! hvis Redirect eller Domain Ændres så skal MainActivity.cs også ændres
        /*
         [IntentFilter(new[] { Android.Content.Intent.ActionView },
        Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable
    },
  Redirect appname  -> DataScheme     = "dk.turftank.turftankregistrationapplication",
             Domain -> DataHost       = "dev-ggbq2i2p.us.auth0.com",
           Redirect -> DataPathPrefix = "/android/dk.turftank.turftankregistrationapplication/callback",
                       AutoVerify     = true)]
        */

        //Skal ændres til false når databasen ikke længere anerkender access_token
        //så skal refreshtoken sendes til auth0 for at få et nyt access-token 
        public bool IsLoggedIn { get; set; } = false;
        //static const AUTH0_DOMAIN = 'auth.turftank.cloud';
        //static const AUTH0_CLIENT_ID = 'OT3kqkpjDvmSZl7WMjvwo4U72k1MWVUw';
        //static const AUTH0_REDIRECT_URI = 'cloud.turftank.osm://login-callback';
        //static const AUTH0_ISSUER = 'https://$AUTH0_DOMAIN';

        public string ClientId { get; set; } = "OT3kqkpjDvmSZl7WMjvwo4U72k1MWVUw";//"X5goHHBzVSkwc8kwWpbw5fq9NzoVkAof";// //
        public string ClientSecret { get; set; } = "KAaqom27hUCbyhUr1KtAO1o-ooxLJUOHn2o6YPmX0OopbD4FjgZnzVzhHynF8SM8";
        public string Domain { get; set; } = "auth.turftank.cloud"; //"dev-ggbq2i2p.us.auth0.com";   //
        public string AuthorizeURL { get => $"https://{Domain}/authorize"; }      //"https://dev-ggbq2i2p.us.auth0.com/authorize";//"https://auth.turftank.cloud/oauth/authorize";//?audience=https://auth.turftank.cloud/u/login";
        public string RedirectURL { get => _redirectURL; }
        public List<string> AllowedCallbackUrls => _allowedCallbackURLs;
        public string AccessTokenURL { get => $"https://{Domain}/token"; }          //"https://dev-ggbq2i2p.us.auth0.com/oauth/token";//"https://auth.turftank.cloud/token";

        public string Scope { get; set; } = "openid profile email offline_access";
        public string AccessToken { get => _accessToken; set { _accessToken = value; IsLoggedIn = true; } }
        //TODO lav en eventhandler som reagerer når denne bliver sat, evt er det bedre med en exception til når den ikke virker og det er tid til at kalde refresh token
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get => _refreshToken; set => _refreshToken = value; }
        public string AuthorizeCode { get; set; }
        public string OAuthState { get => _oAuthState; set => _oAuthState = value; }

        private string _accessToken;
        private string _refreshToken;
        string _oAuthState = "qwertyasdfgzxcvbo";
        string _redirectURL = "cloud.turftank.osm://login-callback";//"dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback";//"
        List<string> _allowedCallbackURLs = new List<string>()
        {
            "https://dk.turftank.turftankregistrationapplication://dev-ggbq2i2p.us.auth0.com/android/dk.turftank.turftankregistrationapplication/callback",
            "https://www.oauthdebugger.com",
            "https://oauthdebugger.com",
            "https://oauthdebugger.com/debug",
            "https://cloud.turftank.osm://auth.turftank.cloud/android/cloud.turftank.osm/login-callback"
        };


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

        public Constants()
        {

        }

        


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
    
}

            


      
    

