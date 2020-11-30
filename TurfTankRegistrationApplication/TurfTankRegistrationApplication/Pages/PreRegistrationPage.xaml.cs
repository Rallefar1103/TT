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

            ComponentBtn ChooseRover = new ComponentBtn("Rover", PreRegVM.ChooseToPreregisterRoverCommand);
            ComponentBtn ChooseBase = new ComponentBtn("Base", PreRegVM.ChooseToPreregisterBaseCommand);
            ComponentBtn ChooseTablet = new ComponentBtn("Tablet", PreRegVM.ChooseToPreregisterTabletCommand);
            ComponentBtn ScanQRBtn = new ComponentBtn("Scan QR", PreRegVM.ScanQRCommand);
            ComponentBtn ScanBarcodeBtn = new ComponentBtn("Scan Barcode", PreRegVM.ScanBarcodeCommand);
            ComponentBtn ConfirmLabellingBtn = new ComponentBtn("Confirm the QR label is on the component", PreRegVM.ConfirmAssemblyAndLabellingCommand);
            ComponentBtn PreregisterBtn = new ComponentBtn("Preregister", PreRegVM.ScanBarcodeCommand);

            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                RowSpacing = 0,
                ColumnSpacing = 0,
                Padding = 20,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(6, GridUnitType.Star) },
                    new RowDefinition(),
                    new RowDefinition { Height = new GridLength(75) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition(),
                    new ColumnDefinition()
                }
            };

            Label ComponentChoiceHeader = new Label
            {
                Text = "Choose Component to Preregister",
                TextColor = Color.White,
                FontSize = 26,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 50,
            };

            Label ScanHeader = new Label
            {
                Text = "Scan QR Sticker and Simcard Barcode",
                TextColor = Color.White,
                FontSize = 26,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 50,
            };

            // grid.Children.Add(Button/Label, Column, Row);
            grid.Children.Add(ComponentChoiceHeader, 0, 0);
            grid.Children.Add(ChooseRover, 0, 1);
            grid.Children.Add(ChooseBase, 1, 1);
            grid.Children.Add(ChooseTablet, 2, 1);
            grid.Children.Add(ScanHeader, 0, 2);
            grid.Children.Add(ScanQRBtn, 0, 3);
            grid.Children.Add(ScanBarcodeBtn, 1, 3);
            grid.Children.Add(ConfirmLabellingBtn, 0, 4);
            grid.Children.Add(PreregisterBtn, 0, 5);

            Content = grid;
            InitializeComponent();
        }

    }
}
