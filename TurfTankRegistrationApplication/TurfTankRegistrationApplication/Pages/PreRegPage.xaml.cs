﻿using System;
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
                BackgroundColor = Color.Yellow,
            };

            Button Base = new Button
            {
                HeightRequest = 60,
                WidthRequest = 80,
                CornerRadius = 25,
                Margin = 35,
                Text = "Base",
                BackgroundColor = Color.LightBlue,
            };

            Button Tablet = new Button
            {
                HeightRequest = 60,
                WidthRequest = 80,
                CornerRadius = 25,
                Margin = 35,
                Text = "Tablet",
                BackgroundColor = Color.LightSalmon,
            };

            Image preregistratioImage = new Image
            {
                Source = "qrprocess2.png",
                HeightRequest = 140,
                WidthRequest = 200,
                Margin = 35,
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
            layout.Children.Add(preregistratioImage);


            Content = layout;


            InitializeComponent();
        }
    }
}
