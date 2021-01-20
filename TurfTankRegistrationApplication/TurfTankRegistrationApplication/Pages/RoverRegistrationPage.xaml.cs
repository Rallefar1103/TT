using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RoverRegistrationPage : ContentPage
    {
        public RoverRegistrationPage()
        {
            
            RoverRegistrationViewModel RoverVM = new RoverRegistrationViewModel(Navigation);
            BindingContext = RoverVM;

            Title = "Register Rover";

            Button RoverSN = new Button
            {
                HeightRequest = 60,
                WidthRequest = 150,
                CornerRadius = 25,
                Text = "Rover SN",
                BackgroundColor = Color.White,
            };

            Button RoverSimcard = new Button
            {
                HeightRequest = 60,
                WidthRequest = 150,
                CornerRadius = 25,
                Text = "Rover QR",
                BackgroundColor = Color.Yellow,
            };

            Label RegisterRover = new Label
            {
                Text = "Register Rover",
                TextColor = Color.White,
                FontSize = 35,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,

            };

            RoverSN.SetBinding(Button.CommandProperty, "ChangeRoverSN");
            RoverSimcard.SetBinding(Button.CommandProperty, "ChangeRoverSimcard");
            RoverSimcard.SetBinding(Button.BackgroundColorProperty, "RoverQRButtonColor");



            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = 20,

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = GridLength.Auto },

                },

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = 100 },
                    new RowDefinition { Height = GridLength.Auto },
                },


            };

            Grid.SetRow(RegisterRover, 0);
            Grid.SetColumnSpan(RegisterRover, 2);
            Grid.SetColumn(RoverSN, 0);
            Grid.SetRow(RoverSN, 2);

            Grid.SetColumn(RoverSimcard, 1);
            Grid.SetRow(RoverSimcard, 2);

            grid.Children.Add(RegisterRover);
            grid.Children.Add(RoverSN);
            grid.Children.Add(RoverSimcard);

            Content = grid;


            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                Application.Current.MainPage.DisplayAlert("Attention!", "You are not connected to the internet, please connect", "OK");
            }
            base.OnAppearing();
        }
    }
}
