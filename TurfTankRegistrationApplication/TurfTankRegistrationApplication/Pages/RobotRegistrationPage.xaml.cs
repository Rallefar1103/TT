using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RobotRegistrationPage : ContentPage
    {

        [Obsolete]
        public RobotRegistrationPage()
        {
            RobotRegistrationViewModel RobotRegVM = new RobotRegistrationViewModel(Navigation);
            BindingContext = RobotRegVM;


            ComponentBtn Chassis = new ComponentBtn("Chassis", RobotRegVM.DidChangeChassisSN);
            ComponentBtn Controller = new ComponentBtn("Controller", RobotRegVM.DidChangeControllerSN);
            ComponentBtn Rover = new ComponentBtn("Rover", RobotRegVM.DidChangeRoverSN);
            ComponentBtn Base = new ComponentBtn("Base", RobotRegVM.DidChangeBaseSN);
            ComponentBtn Tablet = new ComponentBtn("Tablet", RobotRegVM.DidChangeTabletSN);

            SNLabel ChassisSN = new SNLabel();
            SNLabel ControllerSN = new SNLabel();
            SNLabel RoverSN = new SNLabel();
            SNLabel BaseSN = new SNLabel();
            SNLabel TabletSN = new SNLabel();

            ChassisSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.ChassisSN);
            ControllerSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.ControllerSN);
            RoverSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.RoverSN);
            BaseSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.BaseSN);
            TabletSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.TabletSN);


            BoxView SizedBoxTop = new BoxView
            {
                HeightRequest = 20,
            };

            BoxView SizedBoxBottom = new BoxView
            {
                HeightRequest = 20,
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

            

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        SizedBoxTop,
                        Chassis,
                        ChassisSN,
                        Controller,
                        ControllerSN,
                        Rover,
                        RoverSN,
                        Base,
                        BaseSN,
                        Tablet,
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
