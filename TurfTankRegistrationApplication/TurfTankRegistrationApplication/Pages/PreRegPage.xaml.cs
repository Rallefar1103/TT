using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class PreRegPage : ContentPage
    {
        
        public PreRegPage()
        {
            PreRegViewModel PreRegVM = new PreRegViewModel(Navigation);
            BindingContext = PreRegVM;

            Title = "Pre-Registration";

            Button Rover = new Button
            {
                HeightRequest = 60,
                WidthRequest = 80,
                CornerRadius = 25,
                Margin = 35,
                Text = "Rover",
                BackgroundColor = Color.White,
            };

            Button Base = new Button
            {
                HeightRequest = 60,
                WidthRequest = 80,
                CornerRadius = 25,
                Margin = 35,
                Text = "Base",
                BackgroundColor = Color.White,
            };

            Button Tablet = new Button
            {
                HeightRequest = 60,
                WidthRequest = 80,
                CornerRadius = 25,
                Margin = 35,
                Text = "Tablet",
                BackgroundColor = Color.White,
            };

            Rover.SetBinding(Button.CommandProperty, "NavigateToRoverPreReg");
            Base.SetBinding(Button.CommandProperty, "NavigateToBasePreReg");
            Tablet.SetBinding(Button.CommandProperty, "NavigateToTabletPreReg");

            StackLayout layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
            };

            layout.Children.Add(Rover);
            layout.Children.Add(Base);
            layout.Children.Add(Tablet);

            Content = layout;


            InitializeComponent();
        }
    }
}
