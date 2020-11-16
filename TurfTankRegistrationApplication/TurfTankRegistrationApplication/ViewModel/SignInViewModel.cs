using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class SignInViewModel
    {
        #region Public Attributes
        public IEmployee User { get; set; }
        public ICommand LoginCommand { get; set; }
        //public string UsernameLabel { get => _usernameLabel; set { SetProperty(ref _usernameLabel, value); } }
        //public string PasswordLabel { get => _passwordLabel; set { SetProperty(ref _passwordLabel, value); } }
        //public string ErrorMsgLabel { get => _errorMsgLabel; set { SetProperty(ref _errorMsgLabel, value); } }

        #endregion Public Attributes

        #region Constructors
        public void Initialize(IEmployee user, INavigationService nav)
        {
            User = user;
            _navigation = nav;

            //LoginCommand = new Command(execute: () => LoginUser(), canExecute: () => true);
        }
        public SignInViewModel()
        {
            Initialize(new Employee(), new NavigationService());
        }

        public SignInViewModel(IEmployee user)
        {
            Initialize(user, new NavigationService());
        }

        public SignInViewModel(IEmployee user, INavigationService nav)
        {
            Initialize(user, nav);
        }



        #endregion Constructors

        #region Public Methods

        //public void LoginUser()
        //{
        //    ErrorMsgLabel = "";
        //    if (UsernameLabel == "")
        //    {
        //        ErrorMsgLabel += "No Username Given to Form. ";
        //    }
        //    if (PasswordLabel == "")
        //    {
        //        ErrorMsgLabel += "No Password Given to Form. ";
        //    }

        //    if (ErrorMsgLabel == "" && User.Login())
        //    {
        //        _navigation.NavigateToMenu();
        //    }
        //    else
        //    {
        //        ErrorMsgLabel += "Please Try to login again.";
        //    }
        //}

        #endregion Public Methods

        #region Private Attributes

        private INavigationService _navigation;

        private string _errorMsgLabel;

        private string _passwordLabel;

        private string _usernameLabel;

        #endregion Private Attributes

        #region Private Methods

        #endregion Private Methods
    }


}
