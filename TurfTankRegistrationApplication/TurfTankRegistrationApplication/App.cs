using Xamarin.Forms;

using System.Net;
using System.Net.Http;

using TurfTankRegistrationApplication.Pages;
using TurfTankRegistrationApplication.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using System;
using Xamarin.Auth;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication
{
	public interface ITTAuthentication
    {

    };


	public interface IWifiConnector
	{
		void ConnectToWifi(string ssid);
		Task<List<string>> GetAvailableNetworks();
		string GetSSID();
		bool CheckWifiStatus();
	};

	public class App : Application
	{
		/// <summary>
		/// HttpClient is the client which handles all web based interaction.
		/// It must only be initialized ones, and all references to it is to App.ApiClient
		/// </summary>
		public static Client ApiClient { get; private set; }
		public static Account account;
		public static Credentials OAuthCredentials { get; set; }
		public static TurfTankAuth Authenticator;
		public static string AppName { get; private set; } = "TTRA";


		public static HttpClient WifiClient { get; private set; }
		public App()
		{
			OAuthCredentials = new Credentials(selectedAuthServer: SelectedAuthServer.developmentserver ) ;

			// dette skal med fordi at Markup stadig er experimental.
			Device.SetFlags(new string[] { "Markup_Experimental" });
			account = new Account();
			Authenticator = new TurfTankAuth(OAuthCredentials);
			WifiClient = new HttpClient();
			ApiClient = new Client(WifiClient);
			MainPage = new NavigationPage (new SignInPage(Authenticator));
            Console.WriteLine("THE CLIENT ID IS HERE!!!!!! " + OAuthCredentials.ClientId + "ANNDANNDAND THE OTHER " + OAuthCredentials.ClientSecret);
		}


		/// <summary>
        /// Restore OAuthCredentials from persistent storrage
        /// </summary>
		protected override async void OnStart()
		{
			//Clears login token
			var serializedString = await SecureStorage.GetAsync("TTRA");
			if (serializedString != null)
			{
				account = Account.Deserialize(serializedString);
				await OAuthCredentials.Restore();
			}

            System.Console.WriteLine("\n*************\nProgram will start up now enjoy the ride!\n****************\n");
			
		}

		protected override async void OnSleep()
		{

			System.Console.WriteLine("\n*************\nYour tablet is sooooo tired and will go take a morfar nap!\n****************\n");
            await OAuthCredentials.Save();
        }

		protected override async void OnResume()
		{
			await OAuthCredentials.Restore();
			System.Console.WriteLine("\n*************\nHey there, Im Freshhhh!\n****************\n");
		}


		/// <summary>
		/// Is called when the Auth0 server redirects back to the app with autn_code or tokens
		/// Is only used when OAuth2Authenticator IsNativUI = true
		/// Todo, use TurfTankAuth.cs to handle codes, Exchange code for accesstoken, do refresh
		/// see: /******* github  sourcekode for xamarin auth   *************/
		/* https://github.com/xamarin/Xamarin.Auth/blob/master/source/Core/Xamarin.Auth.Common.LinkSource/OAuth2Authenticator.cs#L845 */
		/// </summary>
		/// <param name="uri"></param>
		protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.AbsoluteUri.Contains(OAuthCredentials.RedirectURL))
            {
                //check for accesstoken
                try
                {

                    AccessToken token = new AccessToken(uri.Fragment);

                    await MainPage.Navigation.PushAsync(new MenuPage());
                }
                catch
                {
                    //check for auth_code
                    try
                    {

                        IDictionary<string, string> dict = new Dictionary<string, string>();
                        string[] pairs = uri.Query.TrimStart('?').Split('&');
                        foreach (string pair in pairs)
                        {
                            string[] nameValaue = pair.Split(new char[] { '=' });
                            dict.Add(nameValaue[0], nameValaue[1]);
                        }
                        OAuthCredentials.AuthorizeCode = dict["code"];
                        //await Authenticator.RequestAccessTokenAsync(OAuthCredentials.AuthorizeCode);
                        await MainPage.Navigation.PushAsync(new MenuPage());
                    }
                    catch
                    {
                    }

                    await MainPage.Navigation.PushAsync(new SignInPage(Authenticator));
                }
            }
        }

    }
}

