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

        public SignInPage()
        {
            ICredentials cred = new Constants();
            TurfTankAuth auth = new TurfTankAuth(cred);
            SignInViewModel2 SignInVM = new SignInViewModel2(Navigation,auth);
            BindingContext = SignInVM;
            InitializeComponent();
        }
    }
}
