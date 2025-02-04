﻿using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RobotRegistrationPage : ContentPage
    {

        public RobotRegistrationPage(string ssid)
        {
            RobotRegistrationViewModel RobotRegVM = new RobotRegistrationViewModel(Navigation);
            BindingContext = RobotRegVM;

            Title = "Register Robot: " + ssid;

            // View Components
            ComponentBtn ChassisBtn = new ComponentBtn("Chassis", RobotRegVM.ChangeChassisSN);
            ComponentBtn ControllerBtn = new ComponentBtn("Controller", RobotRegVM.ChangeControllerSN);
            ComponentBtn RoverBtn = new ComponentBtn("Rover", RobotRegVM.ChangeRoverSN);
            ComponentBtn BaseBtn = new ComponentBtn("Base", RobotRegVM.ChangeBaseSN);
            ComponentBtn TabletBtn = new ComponentBtn("Tablet", RobotRegVM.ChangeTabletSN);

            SNLabel ChassisSN = new SNLabel();
            SNLabel ControllerSN = new SNLabel();
            SNLabel ControllerPASSWORD = new SNLabel();
            SNLabel ControllerSSID = new SNLabel();
            SNLabel RoverSN = new SNLabel();
            SNLabel BaseSN = new SNLabel();
            SNLabel TabletSN = new SNLabel();

            SNLabel RoverSIM = new SNLabel();
            SNLabel BaseSIM = new SNLabel();
            SNLabel TabletSIM = new SNLabel();

            BoxView SizedBoxTop = new BoxView
            {
                HeightRequest = 20,
            };

            BoxView SizedBoxBottom = new BoxView
            {
                HeightRequest = 20,
            };

            Label SpacerLabelOne = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
            };

            Label SpacerLabelTwo = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
            };

            Button SaveBtn = new Button
            {
                Text = "Save",
                FontSize = 22,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.DarkGreen,
                TextColor = Color.White,
                HeightRequest = 65,
            };


            // View Bindings
            ChassisSN.SetBinding(Label.TextProperty, new Binding("ChassisSN"));
            ControllerSN.SetBinding(Label.TextProperty, new Binding("ControllerSN"));
            ControllerSSID.SetBinding(Label.TextProperty, new Binding("ControllerSSID"));
            ControllerPASSWORD.SetBinding(Label.TextProperty, new Binding("ControllerPASSWORD"));
            RoverSN.SetBinding(Label.TextProperty, new Binding("RoverSN"));
            BaseSN.SetBinding(Label.TextProperty, new Binding("BaseSN"));
            RoverSIM.SetBinding(Label.TextProperty, new Binding("RoverSIM"));
            BaseSIM.SetBinding(Label.TextProperty, new Binding("BaseSIM"));

            SaveBtn.SetBinding(Button.CommandProperty, new Binding("DidSaveRobot"));

            
            // View Component Attributes
            ChassisSN.TextColor = Color.DarkGreen;
            ChassisSN.FontSize = 18;
            ChassisSN.FontAttributes = FontAttributes.Bold;

            ControllerSN.TextColor = Color.DarkGreen;
            ControllerSN.FontSize = 18;
            ControllerSN.FontAttributes = FontAttributes.Bold;

            ControllerSSID.TextColor = Color.DarkGreen;
            ControllerSSID.FontSize = 18;
            ControllerSSID.FontAttributes = FontAttributes.Bold;

            ControllerPASSWORD.TextColor = Color.DarkGreen;
            ControllerPASSWORD.FontSize = 18;
            ControllerPASSWORD.FontAttributes = FontAttributes.Bold;

            RoverSN.TextColor = Color.DarkGreen;
            RoverSN.FontSize = 18;
            RoverSN.FontAttributes = FontAttributes.Bold;

            RoverSIM.TextColor = Color.Blue;
            RoverSIM.FontSize = 18;
            RoverSIM.FontAttributes = FontAttributes.Bold;

            BaseSN.TextColor = Color.DarkGreen;
            BaseSN.FontSize = 18;
            BaseSN.FontAttributes = FontAttributes.Bold;

            BaseSIM.TextColor = Color.Blue;
            BaseSIM.FontSize = 18;
            BaseSIM.FontAttributes = FontAttributes.Bold;


            // Page Layout
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        SizedBoxTop,
                        ChassisBtn,
                        ChassisSN,
                        SpacerLabelOne,
                        ControllerBtn,
                        ControllerSN,
                        ControllerSSID,
                        ControllerPASSWORD,
                        SpacerLabelTwo,
                        RoverBtn,
                        RoverSN,
                        RoverSIM,
                        BaseBtn,
                        BaseSN,
                        BaseSIM,
                        TabletBtn,
                        TabletSN,
                        SizedBoxBottom,
                        SaveBtn,
                    }
                }
            };

            InitializeComponent();


        }


    }
}
