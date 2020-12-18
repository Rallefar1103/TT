using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RoverSerialNumberPage : ContentPage
    {
        public RoverSerialNumberPage()
        {
            RoverSerialNumberViewModel RoverSNVM = new RoverSerialNumberViewModel(Navigation);
            BindingContext = RoverSNVM;

            Title = "Rover Serial Number";

            Label StoppingInmark = new Label
            {
                Text = "Setting the robot in configuration mode....",
                FontSize = 23,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };

            Label StartingInmark = new Label
            {
                Text = "Setting the robot in operation mode....",
                FontSize = 23,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };

            Label RetrievingSerial = new Label
            {
                Text = "Retrieving serial number....",
                FontSize = 23,
                TextColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };



            Button StopInmark = new Button
            {
                Text = "Stop Inmark",
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                BorderColor = Color.Black,
                BorderWidth = 2,
                Margin = 10,
                CornerRadius = 25,
                HeightRequest = 70,
                WidthRequest = 275,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Button StartInmark = new Button
            {
                Text = "Start Inmark",
                BackgroundColor = Color.Green,
                TextColor = Color.White,
                BorderColor = Color.Black,
                BorderWidth = 2,
                Margin = 10,
                CornerRadius = 25,
                HeightRequest = 70,
                WidthRequest = 275,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Button RoverSerialNumber = new Button
            {
                Text = "Get Serial Number",
                BackgroundColor = Color.White,
                TextColor = Color.Black,
                BorderColor = Color.Black,
                BorderWidth = 2,
                Margin = 10,
                CornerRadius = 25,
                HeightRequest = 70,
                WidthRequest = 275,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            StackLayout stackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
            };

            StopInmark.SetBinding(Button.CommandProperty, "stoppingInmark");
            StartInmark.SetBinding(Button.CommandProperty, "startingInmark");
            RoverSerialNumber.SetBinding(Button.CommandProperty, "RetrieveSerialNumber");

            StopInmark.SetBinding(Button.IsVisibleProperty, "showStopInmark");
            StartInmark.SetBinding(Button.IsVisibleProperty, "showStartInmark");
            RoverSerialNumber.SetBinding(Button.IsVisibleProperty, "showGetRoverSerial");

            StoppingInmark.SetBinding(Label.IsVisibleProperty, "showStopInmarkLabel");
            StartingInmark.SetBinding(Label.IsVisibleProperty, "showStartInmarkLabel");
            RetrievingSerial.SetBinding(Label.IsVisibleProperty, "showRoverRetrievalLabel");



            stackLayout.Children.Add(StopInmark);
            stackLayout.Children.Add(StartInmark);
            stackLayout.Children.Add(RoverSerialNumber);
            stackLayout.Children.Add(StoppingInmark);
            stackLayout.Children.Add(StartingInmark);
            stackLayout.Children.Add(RetrievingSerial);

            Content = stackLayout;

            InitializeComponent();
        }
    }
}
