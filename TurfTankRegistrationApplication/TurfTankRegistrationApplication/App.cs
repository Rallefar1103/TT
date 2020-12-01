using Xamarin.Forms;

using System.Net;
using System.Net.Http;

using TurfTankRegistrationApplication.Pages;
using TurfTankRegistrationApplication.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication
{
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
		public App()
		{
			// dette skal med fordi at Markup stadig er experimental.
			Device.SetFlags(new string[] { "Markup_Experimental" });
			ApiClient = new Client(new HttpClient());
			MainPage = new NavigationPage (new SignInPage());
		}

		protected override void OnStart()
		{
			System.Console.WriteLine("\n*************\nProgram will start up now enjoy the ride!\n****************\n");
		}

		protected override void OnSleep()
		{
			System.Console.WriteLine("\n*************\nYour tablet is sooooo tired and will go take a morfar nap!\n****************\n");
		}

		protected override void OnResume()
		{
			System.Console.WriteLine("\n*************\nHey there, Im Freshhhh!\n****************\n");
		}

	}
}

