using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class SignInPage : ContentPage
    {
        
       
        public SignInPage()
        {
            SignInViewModel2 SignInVM = new SignInViewModel2(Navigation);
            BindingContext = SignInVM;
            InitializeComponent();
        }
    }
}
