using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RoverPreregistrationPage : ContentPage
    {
        public RoverPreregistrationPage(string rover)
        {
            RoverPreregistrationViewModel RoverPreRegVM = new RoverPreregistrationViewModel(Navigation);
            BindingContext = RoverPreRegVM;

            Title = "Pre-registrate " + rover;

            Button ScanQR = new Button
            {
                HeightRequest = 60,
                WidthRequest = 250,
                CornerRadius = 25,
                Margin = 15,
                Text = "Scan QR",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Button ScanBarcode = new Button
            {
                HeightRequest = 60,
                WidthRequest = 250,
                CornerRadius = 25,
                Margin = 15,
                Text = "Scan Barcode",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            Button ConfirmAndSave = new Button
            {
                HeightRequest = 60,
                WidthRequest = 300,
                Text = "Confirm Preregistration",
                TextColor = Color.Black,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.EndAndExpand,
            };

            BoxView box = new BoxView
            {
                HeightRequest = 350,
            };

            StackLayout layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
            };


            ScanQR.SetBinding(Button.CommandProperty, "ScanRoverQR");
            ScanBarcode.SetBinding(Button.CommandProperty, "ScanRoverSim");
            ConfirmAndSave.SetBinding(Button.CommandProperty, "ConfirmPreregistration");

            ScanQR.SetBinding(Button.IsEnabledProperty, "CanScanQR");
            ScanBarcode.SetBinding(Button.IsEnabledProperty, "CanScanBarcode");

            ScanQR.SetBinding(Button.BackgroundColorProperty, "ScanQRColor");
            ScanBarcode.SetBinding(Button.BackgroundColorProperty, "ScanBarcodeColor");

            ConfirmAndSave.SetBinding(Button.IsVisibleProperty, "CanConfirm");


            layout.Children.Add(ScanQR);
            layout.Children.Add(ScanBarcode);
            layout.Children.Add(box);
            layout.Children.Add(ConfirmAndSave);

            Content = layout;

            InitializeComponent();
        }
    }
}
