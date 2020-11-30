using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.ViewModel
{
    public interface IRegistrateRobot
    {
        void RegistrateChassis(QRSticker result);
        void RegistrateController(QRSticker result);
        void RegistrateRoverSimcard(QRSticker result);
        void RegistrateBaseSimcard(QRSticker result);
        void RegistrateTablet();
        void SaveRobot();
    }

    public class RobotRegistrationViewModel : INotifyPropertyChanged, IRegistrateRobot
    {
        public RobotPackage robotItem { get; set; }

        public string ChassisSN { get; set; } = "";
        public string ControllerSN { get; set; } = "";
        public string RoverSN { get; set; } = "";
        public string BaseSN { get; set; } = "";
        public string TabletSN { get; set; } = "";
        public string RoverSIM { get; set; } = "";
        public string BaseSIM { get; set; } = "";
        public string TabletSIM { get; set; } = "";

        public Command DidChangeChassisSN { get; }
        public Command DidChangeControllerSN { get; }
        public Command DidChangeRoverSN { get; }
        public Command DidChangeBaseSN { get; }
        public Command DidChangeTabletSN { get; }
        public Command DidSaveRobot { get;  }

        public Action<object, string> Callback { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation { get; set; }

        public RobotRegistrationViewModel(INavigation navigation, RobotPackage robot)
        {
            this.robotItem = robot;
            this.Navigation = navigation;
            robotItem.SetAsSelected();
            DidChangeChassisSN = new Command(() => NavigateToScanPage("Robot"));
            DidChangeControllerSN = new Command(() => NavigateToScanPage("Controller"));
            DidChangeTabletSN = new Command(() => NavigateToScanPage("Tablet"));
            DidChangeRoverSN = new Command(() => NavigateToRoverPage());
            DidChangeBaseSN = new Command(() => NavigateToBasePage());
            DidSaveRobot = new Command(() => SaveRobot());
            Callback = new Action<object, string>(OnDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);
        }

        private async void OnDataReceived(object sender, string data)
        {
            try
            {
                QRSticker qrSticker = new QRSticker(data);
                
                switch (qrSticker.ofType)
                {
                    case QRType.RobotPackage:
                        RegistrateChassis(qrSticker);
                        break;
                    case QRType.Base:
                        RegistrateBaseSimcard(qrSticker);
                        break;
                    case QRType.Controller:
                        RegistrateController(qrSticker);
                        break;
                    case QRType.Rover:
                        RegistrateRoverSimcard(qrSticker);
                        break;
                    case QRType.Tablet:
                        RegistrateTablet();
                        break;
                    default:
                        await Application.Current.MainPage.DisplayAlert("OBS!", "Scan did not work as expected!", "Ok");
                        break;
                }
            }
            catch (ValidationException e)
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", e.Message, "Ok");
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
        public async void RegistrateChassis(QRSticker result)
        {
            robotItem.QR = result;

            if (string.IsNullOrEmpty(robotItem.SerialNumber))
            {
                robotItem.SerialNumber = result.ID;
                ChassisSN = "Robot SN: " + robotItem.SerialNumber;
                OnPropertyChanged(nameof(ChassisSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Chassis is already scanned", "Ok");
            }
        }

        public async void RegistrateController(QRSticker result)
        {
            if (result is ControllerQRSticker)
            {
                robotItem.Controller.QR = result as ControllerQRSticker;
            }

            if (string.IsNullOrEmpty(robotItem.Controller.ID))
            {
                robotItem.Controller.ID = result.ID;
                ControllerSN = "Controller SN: " + robotItem.Controller.ID;
                OnPropertyChanged(nameof(ControllerSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Controller is already scanned", "Ok");
            }
        }
        

        public async void RegistrateRoverSimcard(QRSticker result)
        {
            // Scan label for Simcard info
            robotItem.RoverGPS.ofType = GPSType.Rover;

            //robotItem.RoverGPS.QR = result;

            if (string.IsNullOrEmpty(robotItem.RoverGPS.Simcard.ID))
            {
                robotItem.RoverGPS.Simcard.ID = result.ID;
                RoverSIM = "Rover Simcard: " + robotItem.RoverGPS.Simcard.ID;
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

        public async void RegistrateBaseSimcard(QRSticker result)
        {
            // Scan label for Simcard info
            robotItem.BaseGPS.ofType = GPSType.Base;

            

            if (string.IsNullOrEmpty(robotItem.BaseGPS.Simcard.ID))
            {
                robotItem.BaseGPS.Simcard.ID = result.ID;
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

        public async void SaveRobot()
        {
            // Not implemented, but will save every component on the robotPackage instance to the DB
            await Application.Current.MainPage.DisplayAlert("Success!", "Robot saved", "OK");
        }
        #endregion
    }
}
