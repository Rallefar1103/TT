using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class ListWifiPage : ContentPage
    {
        public ListWifiPage()
        {

            InitializeComponent();
            RoverRegistrationViewModel roverVM = new RoverRegistrationViewModel(Navigation);
            BindingContext = roverVM;

            Label LoadingText = new Label
            {
                Text = "Scanning....",
                FontSize = 30,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,

            };

            Button StartScanning = new Button
            {
                Text = "Start Scanning",
                BackgroundColor = Color.Green,
                TextColor = Color.Black,
                Margin = 10,
                CornerRadius = 25,
                HeightRequest = 70,
                WidthRequest = 275,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Button Connect = new Button
            {
                Text = "Connect",
                BackgroundColor = Color.Green,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HeightRequest = 70,
            };

            ListView wifiList = new ListView()
            {
                VerticalOptions = LayoutOptions.Start,
            };

            StackLayout stackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
            };


            StartScanning.SetBinding(Button.CommandProperty, "ScanForWifi");
            StartScanning.SetBinding(Button.IsVisibleProperty, "HasNotStartedWifiLoading");

            Connect.SetBinding(Button.IsVisibleProperty, "WifiListIsReady");
            Connect.SetBinding(Button.CommandProperty, "ConnectToSelectedWifi");


            LoadingText.SetBinding(IsVisibleProperty, "ShowLoadingLabel");

            wifiList.SetBinding(IsVisibleProperty, "WifiListIsReady");
            wifiList.SetBinding(ListView.ItemsSourceProperty, "wifiResults");
            wifiList.SetBinding(ListView.SelectedItemProperty, "SelectedNetwork");


            stackLayout.Children.Add(StartScanning);
            stackLayout.Children.Add(LoadingText);
            stackLayout.Children.Add(wifiList);
            stackLayout.Children.Add(Connect);


            Content = stackLayout;
            
        }

        //protected override void OnAppearing()
        //{
        //    RoverRegistrationViewModel rover = new RoverRegistrationViewModel(Navigation);
        //    rover.Scanner();
        //    base.OnAppearing();
        //}


    }
}
