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


            // View Components
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


            // View Bindings
            ChassisSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.ChassisSN);
            ControllerSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.ControllerSN);
            RoverSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.RoverSN);
            BaseSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.BaseSN);
            TabletSN.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.TabletSN);

            RoverSIM.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.RoverSIM);
            BaseSIM.SetBinding<RobotRegistrationViewModel>(Label.TextProperty, vm => vm.BaseSIM);


            // View Component Attributes
            ChassisSN.TextColor = Color.DarkGreen;
            ChassisSN.FontSize = 18;
            ChassisSN.FontAttributes = FontAttributes.Bold;

            ControllerSN.TextColor = Color.DarkGreen;
            ControllerSN.FontSize = 18;
            ControllerSN.FontAttributes = FontAttributes.Bold;

            RoverSN.TextColor = Color.DarkGreen;
            RoverSN.FontSize = 18;
            RoverSN.FontAttributes = FontAttributes.Bold;

            RoverSIM.TextColor = Color.DarkBlue;
            RoverSIM.FontSize = 18;
            RoverSIM.FontAttributes = FontAttributes.Bold;

            BaseSN.TextColor = Color.DarkGreen;
            BaseSN.FontSize = 18;
            BaseSN.FontAttributes = FontAttributes.Bold;

            BaseSIM.TextColor = Color.DarkBlue;
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
                        ControllerBtn,
                        ControllerSN,
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

        }

    }
}
