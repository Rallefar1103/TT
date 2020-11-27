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

            };

            Button StartLoading = new Button
            {
                Text = "Start Scanning",
                BackgroundColor = Color.Green,
            };

            Button Connect = new Button
            {
                Text = "Connect",
                BackgroundColor = Color.Green,
            };

            ListView wifiList = new ListView();
            StackLayout stackLayout = new StackLayout();

            StartLoading.SetBinding(Button.CommandProperty, "ScanForWifi");
            StartLoading.SetBinding(Button.IsVisibleProperty, "HasNotStartedLoading");

            Connect.SetBinding(Button.IsVisibleProperty, "WifiListIsReady");
            Connect.SetBinding(Button.CommandProperty, "ConnectToSelectedWifi");


            LoadingText.SetBinding(IsVisibleProperty, "IsDoneLoading");

            wifiList.SetBinding(IsVisibleProperty, "WifiListIsReady");
            wifiList.SetBinding(ListView.ItemsSourceProperty, "wifiResults");
            wifiList.SetBinding(ListView.SelectedItemProperty, "SelectedNetwork");

           
            stackLayout.Children.Add(StartLoading);
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
