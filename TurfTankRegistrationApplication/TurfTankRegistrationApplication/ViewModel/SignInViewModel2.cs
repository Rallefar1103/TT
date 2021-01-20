using System;
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
            
            

            //Todo Spring presenter over hvis user er logged in
            if (App.OAuthCredentials.IsLoggedIn)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Stored   Access  Token = ").AppendLine($"   {App.OAuthCredentials.AccessToken}");
                sb.Append("Stored   Refresh Token = ").AppendLine($"   {App.OAuthCredentials.RefreshToken}");
                await Application.Current.MainPage.DisplayAlert("Authentication Results", sb.ToString(), "OK");

                await Navigation.PushAsync(new MenuPage());
            }
            else
            {
                var Presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                _authenticator.Error += Handle_loginError;
                Presenter.Completed += Handle_CompletedLoginOnPage;               
                Presenter.Login(authenticator: _authenticator);
                Presenter = null;
                 
            }

        }

        private async void Handle_loginError(object sender, AuthenticatorErrorEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Login did not succeed: ").AppendLine($"{e.Exception}");
            sb.Append("Message: ").AppendLine($"{e.Message}");

            await Application.Current.MainPage.DisplayAlert("Login Error", sb.ToString(), "OK");
            _authenticator.Error -= Handle_loginError;


        }

        private async void Handle_CompletedLoginOnPage(object sender, AuthenticatorCompletedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (e.Account != null && e.Account.Properties != null)
            {
                sb.Append("Recieved Access  Token = ").AppendLine($"   {e.Account.Properties["access_token"]}");
                sb.Append("\n\nStored   Access  Token = ").AppendLine($"   {App.OAuthCredentials.AccessToken}");
                sb.Append("\nStored   Refresh Token = ").AppendLine($"   {App.OAuthCredentials.RefreshToken}");
                App.OAuthCredentials.IsLoggedIn = true;
            }
            else
            {
                sb.Append("Not authenticated ").AppendLine($"Account.Properties does not exist");
                App.OAuthCredentials.IsLoggedIn = false;

            }
            await Application.Current.MainPage.DisplayAlert("Authentication Results", sb.ToString(), "OK");

            await Navigation.PushAsync(new MenuPage());
        }


        


        #endregion Public Methods

    }
}
