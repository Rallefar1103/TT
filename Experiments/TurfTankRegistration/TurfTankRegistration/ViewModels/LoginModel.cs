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


namespace TurfTankRegistration.ViewModels
{
    public class LoginModel : BaseViewModel
    {
        // Models in LoginViewModel
        // CANT CREATE MODELS, ie: var User = new ProductionEmployee();

        // LoginModel Initializer
        public LoginModel()
        {
            ShowOrHideCommand = new Command(execute: () => { ShowOrHidePassword(); });
            LoginCommand = new Command(execute: () => { UserLogin(); });
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
        public string RandomData { get; set; } = "Press Login to get some RandomData from the U.S. National Library of Medicine";
        public ICommand LoginCommand { get; }
        public void UserLogin()
        {
            Username = "Login Not Working Yet";
        }
    }
}
