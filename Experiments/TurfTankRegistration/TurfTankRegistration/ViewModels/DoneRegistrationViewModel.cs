using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TurfTankRegistration.Views;
using Xamarin.Forms;

namespace TurfTankRegistration.ViewModels
{
    class DoneRegistrationViewModel
    {
        public DoneRegistrationViewModel()
        {
            ResetCommand = new Command(execute: () => { Reset(); });
        }
        public ICommand ResetCommand { get; }
        public async void Reset()
        {
            await App.Current.MainPage.Navigation.PushAsync(new StartRegistrationPage());
        }
    }
}
