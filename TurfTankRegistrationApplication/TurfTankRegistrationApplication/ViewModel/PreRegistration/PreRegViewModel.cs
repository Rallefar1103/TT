using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class PreRegViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public Command NavigateToRoverPreReg { get; }
        public Command NavigateToBasePreReg { get; }
        public Command NavigateToTabletPreReg { get; }

        public PreRegViewModel(INavigation nav)
        {
            this.Navigation = nav;
            NavigateToRoverPreReg = new Command(async () => await NavigateToPreRegistration("Rover"));
            NavigateToBasePreReg = new Command(async () => await NavigateToPreRegistration("Base"));

        }

        public async Task NavigateToPreRegistration(string component)
        {
            if (component == "Rover")
            {
                RoverPreregistrationPage roverPage = new RoverPreregistrationPage(component);
                await Navigation.PushAsync(roverPage);

            } else if (component == "Base")
            {
                BasePreregistrationPage basePage = new BasePreregistrationPage(component);
                await Navigation.PushAsync(basePage);

            } else
            {
                await Navigation.PushAsync(new TabletPreregistrationPage());

            }
        }
    }
}
