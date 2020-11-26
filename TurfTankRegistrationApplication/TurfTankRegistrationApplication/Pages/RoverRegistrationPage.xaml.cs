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
    }
}
