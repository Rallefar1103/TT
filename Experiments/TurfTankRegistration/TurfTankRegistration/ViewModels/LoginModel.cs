using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using TurfTankRegistration.Views;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Threading;

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
            ShowOrHideCommand = new Command(execute: () => { ShowOrHidePassword(); });
            LoginCommand = new Command(execute: () => { Login(); });
        }

        // Username Entry
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

        // Password Entry
        public string HidePassword { get; set; } = "True";
        private string password = string.Empty;
        public string Password
        {
            get => password;
            set
            {
                if (password == value)
                    return;
                else
                    password = value;
                
                OnPropertyChanged(nameof(Password));
            }
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
        private void Login()
        {
            var rxcui = "198440";
            var request = System.Net.HttpWebRequest.Create(string.Format(@"https://rxnav.nlm.nih.gov/REST/RxTerms/rxcui/{0}/allinfo", rxcui));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (System.Net.HttpWebResponse response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (System.IO.StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        RandomData = content;
                        OnPropertyChanged(nameof(RandomData));
                    }
                }
            }
        }

    }   
        
}
