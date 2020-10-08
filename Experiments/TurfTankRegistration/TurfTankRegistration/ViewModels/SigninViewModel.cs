using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TurfTankRegistration;
using TurfTankRegistration.Views;

namespace TurfTankRegistration.ViewModels
{
    class SigninViewModel: BaseViewModel
    {
        public SigninViewModel()
        {
            SigninCommand = new Command(execute: () => { Signin(); });
        }
        public ICommand SigninCommand { get; }
        public async void Signin()
        {
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
