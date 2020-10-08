using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TurfTankRegistration.Views;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;
using TurfTankRegistration.Models;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;

namespace TurfTankRegistration.ViewModels
{
    public class LoginModel : BaseViewModel
    {
         // LoginModel Initializer
        public LoginModel()
        {
            ShowOrHideCommand = new Command(execute: () => { ShowOrHidePassword(); });
            LoginCommand = new Command(execute: () => { Login(); });
        }

        // Username Entry
        private string username = string.Empty;
        public string Username
        {
            get => username;
            set { SetProperty(ref username, value); }
        }

        // Password Entry
        public string HidePassword { get; set; } = "True";
        private string password = string.Empty;
        public string Password
        {
            get => password;
            set { SetProperty(ref password, value); }
        }
        public ICommand ShowOrHideCommand { get; }
        private void ShowOrHidePassword()
        {
            HidePassword = HidePassword == "True" ? "False" : "True";
            OnPropertyChanged(nameof(HidePassword));
        }

        // Login Button
        private string randomdata = "Login works with any username and password 'Test'";
        public string RandomData { get => randomdata; set { SetProperty(ref randomdata, value); } }
        public ICommand LoginCommand { get; }
        public async void Login()
        {
            ProductionEmployee User = new ProductionEmployee(Username, Password);
            if (User.LoginVerified() && User.Password == "Test") 
                await App.Current.MainPage.Navigation.PushAsync(new StartRegistrationPage());
            else
                Password = "";
        }
    }
}