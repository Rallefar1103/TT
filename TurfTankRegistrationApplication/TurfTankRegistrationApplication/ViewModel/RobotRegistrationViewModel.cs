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
        void RegistrateRoverSimcard(string result);
        void RegistrateBaseSimcard(string result);
        void RegistrateTablet();
        void SaveRobotToDB();
    }

    public class RobotRegistrationViewModel : INotifyPropertyChanged, IRegistrateRobot
    {
        public RobotPackage robotItem { get; set; }

        public string SSIDTitle { get; set; }
        public string ChassisSN { get; set; } 
        public string ControllerSN { get; set; } 
        public string RoverSN { get; set; } 
        public string BaseSN { get; set; }
        public string TabletSN { get; set; } 
        public string RoverSIM { get; set; } 
        public string BaseSIM { get; set; } 
        public string TabletSIM { get; set; } 

        public Command ChangeChassisSN { get; }
        public Command ChangeControllerSN { get; }
        public Command ChangeRoverSN { get; }
        public Command ChangeBaseSN { get; }
        public Command ChangeTabletSN { get; }
        public Command SaveRobot { get;  }

        public Action<object, string> ScanCallback { get; set; }
        public Action<object, string> RoverCallback { get; set; }
        public Action<object, string> WifiCallback { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public RobotRegistrationViewModel(INavigation navigation)
        {
            
            this.robotItem = new RobotPackage();
            this.Navigation = navigation;
            robotItem.SetAsSelected();
            ChangeChassisSN = new Command(() => NavigateToScanPage("Robot"));
            ChangeControllerSN = new Command(() => NavigateToScanPage("Controller"));
            ChangeTabletSN = new Command(() => NavigateToScanPage("Tablet"));
            ChangeRoverSN = new Command(() => NavigateToRoverPage());
            ChangeBaseSN = new Command(() => NavigateToBasePage());
            SaveRobot = new Command(() => SaveRobotToDB());
            ScanCallback = new Action<object, string>(OnScanDataReceived);
            RoverCallback = new Action<object, string>(OnRoverDataReceived);
            WifiCallback = new Action<object, string>(OnWifiDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", ScanCallback);
            MessagingCenter.Subscribe<RoverRegistrationViewModel, string>(this, "RoverSerialNumber", RoverCallback);
            //MessagingCenter.Subscribe<WifiPageViewModel, string>(this, "RobotSSID", WifiCallback);
        }

        private void OnWifiDataReceived(object sender, string data)
        {
            SSIDTitle = data;
            OnPropertyChanged(nameof(SSIDTitle));
            DoWeGetHere(data);
        }

        public void DoWeGetHere(string wifi)
        {
            Console.WriteLine("!!!!!!! -------- WE get here with the wifi network: " + wifi);
        }

        private void OnRoverDataReceived(object sender, string data)
        {
            robotItem.RoverGPS.ID = data;
            RoverSN = "Rover SN: " + robotItem.RoverGPS.ID;
            OnPropertyChanged(nameof(RoverSN));
        }

        private async void OnScanDataReceived(object sender, string data)
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
                RegistrateRoverSimcard(data);
            }
            else if (data.Contains("Base"))
            {
                RegistrateBaseSimcard(data);

            } else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Scan did not work as expected!", "Ok");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Navigator methods
        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        public void NavigateToRoverPage()
        {
            RoverRegistrationPage roverPage = new RoverRegistrationPage();
            Navigation.PushAsync(roverPage);
        }

        public void NavigateToBasePage()
        {
            BaseRegistrationPage basePage = new BaseRegistrationPage();
            Navigation.PushAsync(basePage);
        }
        #endregion

        #region public methods
        public async void RegistrateChassis(string result)
        {
            Console.WriteLine("!!!!!!!!! REGISTRATING CHASSIS !!!!!!!!!!");
            if (string.IsNullOrEmpty(robotItem.SerialNumber))
            {
                robotItem.SerialNumber = result;
                ChassisSN = "Robot SN: " + robotItem.SerialNumber;
                OnPropertyChanged(nameof(ChassisSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Chassis is already scanned", "Ok");
            }
        }

        public async void RegistrateController(string result)
        {
            Console.WriteLine("!!!!!!!!! REGISTRATING CONTROLLER !!!!!!!!!!");
            if (string.IsNullOrEmpty(robotItem.Controller.ID))
            {
                robotItem.Controller.ID = result;
                ControllerSN = "Controller SN: " + robotItem.Controller.ID;
                OnPropertyChanged(nameof(ControllerSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Controller is already scanned", "Ok");
            }
        }
        

        public async void RegistrateRoverSimcard(string result)
        {
            // Scan label for Simcard info
            robotItem.RoverGPS.ofType = GPSType.Rover;

            if (string.IsNullOrEmpty(robotItem.RoverGPS.Simcard.ID))
            {
                robotItem.RoverGPS.Simcard.ID = result;
                RoverSIM = "Rover Simcard: " + robotItem.RoverGPS.Simcard.ID;
                OnPropertyChanged(nameof(RoverSIM));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Rover is already scanned", "Ok");
            }

        }

        public async void RegistrateBaseSimcard(string result)
        {
            // Scan label for Simcard info
            robotItem.BaseGPS.ofType = GPSType.Base;

            if (string.IsNullOrEmpty(robotItem.BaseGPS.Simcard.ID))
            {
                robotItem.BaseGPS.Simcard.ID = result;
                BaseSIM = "Base Simcard: " + robotItem.BaseGPS.Simcard.ID;
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

        public async void SaveRobotToDB()
        {
            // Not implemented, but will save every component on the robotPackage instance to the DB
            await Application.Current.MainPage.DisplayAlert("Success!", "Robot saved", "OK");
        }
        #endregion
    }
}
