using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TurfTankRegistration.Views;
using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace TurfTankRegistration.ViewModels
{
    public class LoginModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string username)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(username));
        }

        // LoginModel Initializer
        public LoginModel()
        {
            ShowOrHideCommand = new Command(() => { ShowOrHidePassword(); });
        }

        // Username Business Logic
        private string username = string.Empty;
        public string Username
        {
            get => username;
            set
            {
                if (username == value)
                    return;
                else
                    username = value;

                OnPropertyChanged(nameof(Username));
            }
        }

        // Password Business Logic
        public string ShowOrHide { get; set; } = "Show";
        public bool HidePassword { get; set; } = true;
        private string password = string.Empty;
        private string displayPassword = string.Empty;
        public string Password
        {
            get => password;
            set
            {
                if (password == value)
                    return;
                else if (HidePassword == true) // THIS DOESNT WORK YET!
                {
                    password = value;
                    displayPassword = Regex.Replace(password,@"[a-zA-Z0-9]","*");
                }
                else if (HidePassword == false)
                {
                    password = value;
                    displayPassword = value;
                }
                else
                    throw new Exception("Setting of password failed, please restart app");

                OnPropertyChanged(nameof(displayPassword));
            }
        }
        public ICommand ShowOrHideCommand { get; }
        private bool ShowOrHidePassword()
        {
            if (HidePassword == true)
            {
                ShowOrHide = "Hide";
                HidePassword = false;
            }
            else
            {
                ShowOrHide = "Show";
                HidePassword = true;
            }
            OnPropertyChanged(nameof(ShowOrHide));
            return HidePassword;
        }
    }
}
