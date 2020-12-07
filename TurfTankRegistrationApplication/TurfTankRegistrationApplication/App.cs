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
		public static TurfTankAuth Authenticator { get; private set; }
		public Account account;
		public static Constants OAuthCredentials { get; set; }
		public static string AppName { get; private set; } = "TTRA";

        public App()
		{
			// dette skal med fordi at Markup stadig er experimental.
			Device.SetFlags(new string[] { "Markup_Experimental" });

			OAuthCredentials =  new Constants();
			ApiClient = new Client(new HttpClient());
			MainPage = new NavigationPage (new SignInPage());
		}

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

        protected override async void OnAppLinkRequestReceived(Uri uri)
        {
            if (uri.AbsoluteUri.Contains(OAuthCredentials.RedirectURL))
            {
				try
				{
					//Checking if access token can be created from callback
					AccessToken token = new AccessToken(uri.Fragment);

					OAuthCredentials.AccessToken = token.access_token;
					OAuthCredentials.Scope = token.scope;
					OAuthCredentials.AccessTokenExpiration = DateTime.UtcNow.AddSeconds(Convert.ToInt32(token.expires_in));
					OAuthCredentials.OAuthState = token.state;
					OAuthCredentials.IsLoggedIn = true;
					await OAuthCredentials.Save();

					await MainPage.Navigation.PushAsync(new MenuPage());
				}
				catch
                {
					await MainPage.Navigation.PushAsync(new SignInPage());
					//Authenticator.AuthenticatorAuth2.OnError("User is not logged in");
                }
            }
        }

    }
}

