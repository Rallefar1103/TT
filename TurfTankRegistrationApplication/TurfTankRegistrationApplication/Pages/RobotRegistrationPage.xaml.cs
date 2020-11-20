using System;
using System.Collections.Generic;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Views.Registration_views;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Pages
{
    public partial class RobotRegistrationPage : ContentPage
    {
        readonly RobotPackage robot = new RobotPackage();

        [Obsolete]
        public RobotRegistrationPage()
        {
            InitializeComponent();
            
            RobotRegistrationViewModel RobotRegVM = new RobotRegistrationViewModel(Navigation, robot);
            BindingContext = RobotRegVM;

            ComponentBtn ChassisBtn = new ComponentBtn("Chassis", RobotRegVM.DidChangeChassisSN);
            ComponentBtn ControllerBtn = new ComponentBtn("Controller", RobotRegVM.DidChangeControllerSN);
            ComponentBtn RoverBtn = new ComponentBtn("Rover", RobotRegVM.DidChangeRoverSN);
            ComponentBtn BaseBtn = new ComponentBtn("Base", RobotRegVM.DidChangeBaseSN);
            ComponentBtn TabletBtn = new ComponentBtn("Tablet", RobotRegVM.DidChangeTabletSN);

            SNLabel ChassisSN = new SNLabel();
            SNLabel ControllerSN = new SNLabel();
            SNLabel RoverSN = new SNLabel();
            SNLabel BaseSN = new SNLabel();
            SNLabel TabletSN = new SNLabel();

            SNLabel RoverSIM = new SNLabel();
            SNLabel BaseSIM = new SNLabel();
            SNLabel TabletSIM = new SNLabel();

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
                        ChassisBtn,
                        ChassisSN,
                        ControllerBtn,
                        ControllerSN,
                        RoverBtn,
                        RoverSN,
                        BaseBtn,
                        BaseSN,
                        TabletBtn,
                        TabletSN,
                        SizedBoxBottom,
                        SaveBtn,
                    }
                }
            };

        }

    }
}
