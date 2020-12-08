﻿using System;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Pages;
using System.Text;

namespace TurfTankRegistrationApplication.ViewModel
{
    
    public class SignInViewModel2 : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public INavigation navigation;
        public Command NavigateToMenuPage { get; }
        public string Token { get; set; }

        TurfTankAuth _authenticator;
        
        #region Constructor

        public SignInViewModel2(INavigation navigation,TurfTankAuth auth)
        {
            this.Navigation = navigation;
            NavigateToMenuPage = new Command(async () => await GoToMenuPageAsync());
            _authenticator = auth;
        }
        //til test
        public SignInViewModel2()
        {
            this.Navigation = null;
            NavigateToMenuPage = new Command(async () => await GoToMenuPageAsync());
        }

        #endregion

        #region Public Methods

        public async Task GoToMenuPageAsync()
        {
            
            //_authenticator.Completed -= Handle_CompletedLoginOnPage;
            //_authenticator.Completed += Handle_CompletedLoginOnPage;


            if (_authenticator.IsAuthenticated())
            {
                await navigation.PushAsync(new MenuPage());
            }
            else
            {
                var Presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                Presenter.Completed += Handle_CompletedLoginOnPage;
                Presenter.Login(authenticator: _authenticator);
            }

        }

        private async void Handle_CompletedLoginOnPage(object sender, AuthenticatorCompletedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (e.Account != null && e.Account.Properties != null)
            {
                sb.Append("Token = ").AppendLine($"{e.Account.Properties["access_token"]}");
            }
            else
            {
                sb.Append("Not authenticated ").AppendLine($"Account.Properties does not exist");
            }
            await Application.Current.MainPage.DisplayAlert("Authentication Results", sb.ToString(), "OK");
            //navigation.PushAsync(new MenuPage());
        }


        


        #endregion Public Methods

    }
}
