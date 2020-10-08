using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TurfTankRegistration.Views;
using Xamarin.Forms;

namespace TurfTankRegistration.ViewModels
{
    public class StartRegistrationViewModel : BaseViewModel
    {
        // Initializers
        public StartRegistrationViewModel()
        {
            StartCommand = new Command(execute: () => { Start(); });
        }
        // Navigation
        public ICommand StartCommand { get; }
        public async void Start()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegistrationPage());
        }
    }
}
