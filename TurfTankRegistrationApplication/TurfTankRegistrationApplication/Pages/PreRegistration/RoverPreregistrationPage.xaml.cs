using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
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
            SNLabel QRScannedData = new SNLabel();


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
            SNLabel BarcodeSN = new SNLabel();


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

            Label ToggleQR = new Label
            {
                Text = "Check QR Label Mounted",
                TextColor = Color.White,
                FontSize = 15,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            CheckBox checkQRMounted = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Color = Color.White,
                HeightRequest = 100,
                WidthRequest = 100,
                
            };

            TapGestureRecognizer registerQRMounted = new TapGestureRecognizer();
            registerQRMounted.Tapped += (s, e) => {
                RoverPreRegVM.toggleQRTapped();
            };
            checkQRMounted.GestureRecognizers.Add(registerQRMounted);

            Label ToggleSimcardInserted = new Label
            {
                Text = "Check Simcard inserted",
                TextColor = Color.White,
                FontSize = 15,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            CheckBox checkSimcardInserted = new CheckBox
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Color = Color.White,
                HeightRequest = 100,
                WidthRequest = 100,

            };

            TapGestureRecognizer registerSimcardInserted = new TapGestureRecognizer();
            registerQRMounted.Tapped += (s, e) => {
                RoverPreRegVM.toggleSimcardTapped();
            };
            checkQRMounted.GestureRecognizers.Add(registerSimcardInserted);

            Grid layout = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = 20,

                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Auto },

                },

                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                },
            };


            ScanQR.SetBinding(Button.CommandProperty, "ScanRoverQR");
            QRScannedData.SetBinding(Label.TextProperty, "QRScannedData");
            ScanBarcode.SetBinding(Button.CommandProperty, "ScanRoverSim");
            BarcodeSN.SetBinding(Label.TextProperty, "BarcodeSN");

            ConfirmAndSave.SetBinding(Button.CommandProperty, "ConfirmPreregistration");

            ScanQR.SetBinding(Button.IsEnabledProperty, "CanScanQR");
            ScanBarcode.SetBinding(Button.IsEnabledProperty, "CanScanBarcode");

            ScanQR.SetBinding(Button.BackgroundColorProperty, "ScanQRColor");
            ScanBarcode.SetBinding(Button.BackgroundColorProperty, "ScanBarcodeColor");

            ConfirmAndSave.SetBinding(Button.IsVisibleProperty, "CanConfirm");

            checkQRMounted.SetBinding(CheckBox.IsCheckedProperty, "toggledQRMounted");
            checkSimcardInserted.SetBinding(CheckBox.IsCheckedProperty, "toggleSimcardMounted");

            Grid.SetRow(ScanQR, 0);
            Grid.SetRow(QRScannedData, 1);

            Grid.SetRow(ToggleQR, 1);
            Grid.SetRow(checkQRMounted, 1);
            Grid.SetRow(ScanBarcode, 2);
            Grid.SetRow(BarcodeSN, 2);

            Grid.SetRow(ToggleSimcardInserted, 3);
            Grid.SetRow(checkSimcardInserted, 3);
            Grid.SetRow(box, 4);
            Grid.SetRow(ConfirmAndSave, 5);

            Grid.SetColumnSpan(ScanQR, 2);
            Grid.SetColumn(ToggleQR, 0);
            Grid.SetColumn(checkQRMounted, 1);
            Grid.SetColumnSpan(ScanBarcode, 2);
            Grid.SetColumn(ToggleSimcardInserted, 0);
            Grid.SetColumn(checkSimcardInserted, 1);
            Grid.SetColumnSpan(box, 2);
            Grid.SetColumnSpan(ConfirmAndSave, 2);

            layout.Children.Add(ScanQR);
            layout.Children.Add(QRScannedData);
            layout.Children.Add(ToggleQR);
            layout.Children.Add(checkQRMounted);
            layout.Children.Add(ScanBarcode);
            layout.Children.Add(BarcodeSN);
            layout.Children.Add(ToggleSimcardInserted);
            layout.Children.Add(checkSimcardInserted);
            layout.Children.Add(box);
            layout.Children.Add(ConfirmAndSave);
           

            Content = layout;

            InitializeComponent();
        }
    }
}
