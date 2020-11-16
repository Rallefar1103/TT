using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class PreRegistrationPage : ContentPage
    {
        public PreRegistrationPage()
        {
            PreRegistrationViewModel PreRegVM = new PreRegistrationViewModel(Navigation);
            BindingContext = PreRegVM;
            NavigationPage.SetHasNavigationBar(this, false);

            ComponentBtn RoverBtn = new ComponentBtn(PreRegVM.Rover, PreRegVM.RegistrateRover);
            ComponentBtn BaseBtn = new ComponentBtn(PreRegVM.Base, PreRegVM.RegistrateBase);
            
            Label Header = new Label
            {
                Text = "Choose Component",
                TextColor = Color.White,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
            };

            BoxView SizedBox = new BoxView
            {
                HeightRequest = 75,
            };

            Content = new StackLayout
            {
                Children =
                {
                    SizedBox,
                    Header,
                    SizedBox,
                    RoverBtn,
                    BaseBtn,
                }
            };
            InitializeComponent();
        }

    }
}
