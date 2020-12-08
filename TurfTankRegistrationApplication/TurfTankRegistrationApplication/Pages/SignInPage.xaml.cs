using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class SignInPage : ContentPage
    {

        public SignInPage(TurfTankAuth authenticator)
        {
            SignInViewModel2 SignInVM = new SignInViewModel2(Navigation,authenticator);
            BindingContext = SignInVM;
            InitializeComponent();
        }
    }
}
