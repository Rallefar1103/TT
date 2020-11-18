using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;

namespace TurfTankRegistrationApplication.ViewModel
{
    public interface IRegistrateRobot
    {
        void RegistrateChassis(string result);
        void RegistrateController(string result);
        void RegistrateRover(string result);
        void RegistrateBase(string result);
        void RegistrateTablet();
        void SaveRobot();
    }

    public class RobotRegistrationViewModel : INotifyPropertyChanged, IRegistrateRobot
    {
        public RobotPackage robotItem = new RobotPackage();

        public string ChassisSN { get; set; } = "";
        public string ControllerSN { get; set; } = "";
        public string RoverSN { get; set; } = "";
        public string BaseSN { get; set; } = "";
        public string TabletSN { get; set; } = "";
        public string RoverSIM { get; set; } = "";
        public string BaseSIM { get; set; } = "";
        public string TabletSIM { get; set; } = "";

        public INavigation Navigation { get; set; }

        public RobotRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            robotItem.SetAsSelected();

            DidChangeChassisSN = new Command(() => NavigateToScanPage("Chassis"));
            DidChangeControllerSN = new Command(() => NavigateToScanPage("Controller"));
            DidChangeRoverSN = new Command(() => NavigateToScanPage("Rover"));
            DidChangeBaseSN = new Command(() => NavigateToScanPage("Base"));
            DidChangeTabletSN = new Command(() => NavigateToScanPage("Tablet"));
            Callback = new Action<object, string>(OnDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);

        }

        private async void OnDataReceived(object sender, string data)
        {
            if (data.Contains("Robot"))
            {
                RegistrateChassis(data);
            }
            else if (data.Contains("Controller"))
            {
                RegistrateController(data);
            }
            else if (data.Contains("Rover"))
            {
                RegistrateRover(data);
            }
            else if (data.Contains("Base"))
            {
                RegistrateBase(data);

            } else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Scan did not work as expected!", "Ok");
            }
        }

        public Action<object, string> Callback { get; set; }
        public Command DidChangeChassisSN { get; }
        public Command DidChangeControllerSN { get; }
        public Command DidChangeRoverSN { get; }
        public Command DidChangeBaseSN { get; }
        public Command DidChangeTabletSN { get; }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region helperfunctions
        public void NavigateToScanPage(string component)
        {
            Navigation.PushAsync(new ScanPage(component));
        }

   

        #endregion

        #region public methods
        public async void RegistrateChassis(string result)
        {
            if (string.IsNullOrEmpty(robotItem.SerialNumber))
            {
                robotItem.SerialNumber = result;
                ChassisSN = robotItem.SerialNumber;
                OnPropertyChanged(nameof(ChassisSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Chassis is already scanned", "Ok");
            }
        }

        public async void RegistrateController(string result)
        {
            if (string.IsNullOrEmpty(robotItem.Controller.ID))
            {
                robotItem.Controller.ID = result;
                ControllerSN = robotItem.Controller.ID;
                OnPropertyChanged(nameof(ControllerSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Controller is already scanned", "Ok");
            }
        }
        

        public async void RegistrateRover(string result)
        {
            // Scan label for Simcard info
            robotItem.RoverGPS.ofType = GPS.GPSType.Rover;

            if (string.IsNullOrEmpty(robotItem.RoverGPS.Simcard.ID))
            {
                robotItem.RoverGPS.Simcard.ID = result;
                RoverSIM = robotItem.RoverGPS.Simcard.ID;
                OnPropertyChanged(nameof(RoverSIM));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Rover is already scanned", "Ok");
            }

            // Retrieve SimCard SN from DB

            // Connect to Controller WiFi
            robotItem.Controller.SetupWifi();

            // Retrieve Rover SN from Controller

        }

        public async void RegistrateBase(string result)
        {
            // Scan label for Simcard info
            robotItem.BaseGPS.ofType = GPS.GPSType.Base;

            if (string.IsNullOrEmpty(robotItem.BaseGPS.Simcard.ID))
            {
                robotItem.BaseGPS.Simcard.ID = result;
                BaseSIM = robotItem.BaseGPS.Simcard.ID;
                OnPropertyChanged(nameof(BaseSIM));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Base is already scanned", "Ok");
            }

            // Set up connection to Base through Bluetooth
            // Retrieve Base SN 
        }

        public async void RegistrateTablet()
        {
            // To be implemented
        }

        public void SaveRobot()
        {
            // Not implemented, but will save every component on the robotPackage instance to the DB
        }
        #endregion
    }
}
