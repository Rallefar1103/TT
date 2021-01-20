using System;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Pages;
using TurfTankRegistrationApplication.Views;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class MenuPageViewModel
    {
        public INavigation Navigation { get; set; }

        public MenuPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            NavigateToRobotRegistration = new Command(async () => await GoToRobotRegistration());
            NavigateToPreRegistration = new Command(async () => await GoToPreRegistration());
            NavigateToScanner = new Command(async () => await GoToScanner());
        }

        public INavigation navigation;

        public Command NavigateToRobotRegistration { get; }
        public Command NavigateToPreRegistration { get; }
        public Command NavigateToScanner { get; }

        
        public async Task GoToRobotRegistration()
        {
            await Navigation.PushAsync(new ListWifiPage());
            //await Navigation.PushAsync(new RobotRegistrationPage("TEST"));

        }

        public async Task GoToPreRegistration()
        {
            await Navigation.PushAsync(new PreRegPage());
        }

        public async Task GoToScanner()
        {
            //await Navigation.PushAsync(new ScanPage());
        }
    }
}
