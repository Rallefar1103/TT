using System;
using Xamarin.Forms;
using Xamarin.Forms.Markup;
using TurfTankRegistrationApplication.Pages;

namespace TurfTankRegistrationApplication
{
	public class App : Application
	{
		public App()
		{
			// dette skal med fordi at Markup stadig er experimental.
			Device.SetFlags(new string[] { "Markup_Experimental" });
			MainPage = new NavigationPage (new TurfTankRegistrationApplication.Pages.LoginPage());
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

