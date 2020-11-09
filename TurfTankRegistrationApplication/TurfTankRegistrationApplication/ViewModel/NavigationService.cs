using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication.ViewModel
{
    public interface INavigationService
    {
        // It uses void now, but needs to be changed to Task to make it work properly
        void NavigateToMenu();
        // Task NavigateToMenu();
        // Task NavigateToRobotRegistration();
        // Task NavigateToComponentPreregistration();
        // Add more...
        // Task is just a way to make a function asynchronous. So write "async" before and use "await" in function
    }
    public class NavigationService : INavigationService
    {
        #region Constructors

        public NavigationService()
        {

        }

        #endregion Constructors

        #region Public Methods
        public void NavigateToMenu()
        {
            throw new NotImplementedException();
            // await App.Page.Navigation.PushAsync(new MenuView());
        }

        #endregion Public Methods
    }
}
