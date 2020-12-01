using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RoverRegistrationPage : ContentPage
    {
        public RoverRegistrationPage()
        {
            InitializeComponent();
            RoverRegistrationViewModel RoverVM = new RoverRegistrationViewModel(Navigation);
            BindingContext = RoverVM;
            var Locationstatus = Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            

        }

        protected override void OnAppearing()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Application.Current.MainPage.DisplayAlert("Attention!", "You are not connected to the internet, please connect", "OK");
            }
            base.OnAppearing();
        }
    }
}
