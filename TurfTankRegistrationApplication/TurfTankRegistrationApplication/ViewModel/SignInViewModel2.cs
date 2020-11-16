using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Pages;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class SignInViewModel2 
    {
        public INavigation Navigation { get; set; }

        public SignInViewModel2(INavigation navigation)
        {
            this.Navigation = navigation;
            NavigateToMenuPage = new Command( async () => await GoToMenuPage());
        }


        public INavigation navigation;

        public Command NavigateToMenuPage { get; }

        public async Task GoToMenuPage()
        {
            await Navigation.PushAsync(new MenuPage());
        }
        

    }
}
