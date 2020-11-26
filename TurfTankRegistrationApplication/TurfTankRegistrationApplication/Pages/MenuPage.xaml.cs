using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class MenuPage : ContentPage
    {   
        public MenuPage()
        {
            MenuPageViewModel MenuPageVM = new MenuPageViewModel(Navigation);
            BindingContext = MenuPageVM;
            InitializeComponent();
        }
    }
}
