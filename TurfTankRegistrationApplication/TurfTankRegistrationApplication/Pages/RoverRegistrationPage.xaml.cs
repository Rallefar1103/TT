using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RoverRegistrationPage : ContentPage
    {
        public RoverRegistrationPage()
        {
            InitializeComponent();
            RoverRegistrationViewModel RoverVM = new RoverRegistrationViewModel(Navigation);
            BindingContext = RoverVM;
        }
    }
}
