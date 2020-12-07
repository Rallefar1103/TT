using System;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Connection;

namespace TurfTankRegistrationApplication.ViewModel
{
    
    public class SignInViewModel2 : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public INavigation navigation;
        public Command NavigateToMenuPage { get; }
        public string Token { get; set; }

        TurfTankAuth _auth;
        OAuth2Authenticator _auth2;

        
        #region Constructor

        public SignInViewModel2(INavigation navigation,TurfTankAuth auth)
        {
            this.Navigation = navigation;
            NavigateToMenuPage = new Command(async () => await GoToMenuPageAsync());
            _auth = auth;
            _auth2 = auth.Auth2;
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
            var Presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            Presenter.Login(authenticator: _auth2);
        }

        #endregion Public Methods

    }
}
