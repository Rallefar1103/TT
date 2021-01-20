using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;
using TurfTankRegistrationApplication.Exceptions;
using System.Linq;

namespace TurfTankRegistrationApplication.ViewModel
{
    public interface IRegistrateRobot
    {
        void RegistrateChassis(QRSticker result);
        Task RegistrateController(QRSticker result);
        void RegistrateRoverSimcard(QRSticker result);
        Task RegistrateBaseSimcard(QRSticker result);
        void RegistrateTablet();
        void SaveRobotToDB();
    }

    public class RobotRegistrationViewModel : INotifyPropertyChanged, IRegistrateRobot
    {
        public RobotPackage robotItem { get; set; }

        public string ChassisSN { get; set; }
        public string ControllerSN { get; set; }
        public string ControllerSSID { get; set; }
        public string ControllerPASSWORD { get; set; }
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
        public Action<object, string> BaseCallback { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation Navigation { get; set; }

        public RobotRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            this.robotItem = new RobotPackage();
            robotItem.SetAsSelected();
            ChangeChassisSN = new Command(() => NavigateToScanPage("robot"));
            ChangeControllerSN = new Command(() => NavigateToScanPage("controller"));
            ChangeTabletSN = new Command(() => NavigateToScanPage("tablet"));
            ChangeRoverSN = new Command(() => NavigateToRoverPage());
            ChangeBaseSN = new Command(() => NavigateToBasePage());
            SaveRobot = new Command(() => SaveRobotToDB());
            ScanCallback = new Action<object, string>(OnScanDataReceived);
            RoverCallback = new Action<object, string>(OnRoverDataReceived);
            BaseCallback = new Action<object, string>(OnBaseDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", ScanCallback);
            MessagingCenter.Subscribe<RoverSerialNumberViewModel, string>(this, "RoverSerialNumber", RoverCallback);
            MessagingCenter.Subscribe<BaseRegistrationViewModel, string>(this, "BaseSerialNumber", BaseCallback);
        }

        private void OnRoverDataReceived(object sender, string data)
        {
            robotItem.RoverGPS.ID = data;
            RoverSN = "Rover SN: " + robotItem.RoverGPS.ID;
            OnPropertyChanged(nameof(RoverSN));
        }

        private void OnBaseDataReceived(object sender, string data)
        {
            robotItem.BaseGPS.ID = data;
            BaseSN = "Base SN: " + robotItem.BaseGPS.ID;
            OnPropertyChanged(nameof(BaseSN));
        }

        private async void OnScanDataReceived(object sender, string data)
        {
            data.Trim();
            try
            {
                QRSticker qrSticker;

                if (data.Contains("controller"))
                {
                    //Todo  fix denne constructor den crasher når den kommer tilbage med manual data
                    ControllerQRSticker controllerQrSticker = new ControllerQRSticker(data);
                    qrSticker = controllerQrSticker;
                }
                else
                {
                    qrSticker = new QRSticker(data);
                }

                switch (qrSticker.OfType)
                {
                    case QRType.robot:
                        RegistrateChassis(qrSticker);
                        break;
                    case QRType.basestation:
                        await RegistrateBaseSimcard(qrSticker);
                        break;
                    case QRType.controller:
                        await RegistrateController(qrSticker);
                        break;
                    case QRType.rover:
                        RegistrateRoverSimcard(qrSticker);
                        break;
                    case QRType.tablet:
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
            catch (Exception e)
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
            ScanPage scanPage = new ScanPage(returntypeKey:"Result");
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
                await Application.Current.MainPage.DisplayAlert("Success!", "Chassis is successfully scanned", "Ok");
                robotItem.SerialNumber = result.ID;
                ChassisSN = "Robot SN: " + robotItem.SerialNumber;
                OnPropertyChanged(nameof(ChassisSN));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Chassis is already scanned", "Ok");
            }
        }

        // TODO: Update all registrate functions to match this form
        // Refactor property changed to be used in both RobotPackage and the different components.
        public async Task RegistrateController(QRSticker result)
        {

            if (result is ControllerQRSticker)
            {
                robotItem.Controller.QR = result as ControllerQRSticker;
                robotItem.Controller = await Controller.API.GetById(result.ID);
                if (robotItem.Controller != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Success!", "Controller is successfully scanned", "Ok");
                    ControllerSN = robotItem.Controller.SerialNumber;
                    ControllerSSID = "SSID: " + robotItem.Controller.ActiveSSID;
                    ControllerPASSWORD = "Password: " + robotItem.Controller.ActivePassword;
                    OnPropertyChanged(nameof(ControllerSN));
                    OnPropertyChanged(nameof(ControllerSSID));
                    OnPropertyChanged(nameof(ControllerPASSWORD));
                }
                
            }

            if (string.IsNullOrEmpty(robotItem.Controller.ID))
            {
                robotItem.Controller.ID = result.ID;
                ControllerSN = $"ID:\n " + robotItem.Controller.ID;
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
                await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Rover QR code! " + result.ID, "Ok");
                RoverSIM = "Rover QR: " + robotItem.RoverGPS.Simcard.ID;
                OnPropertyChanged(nameof(RoverSIM));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", "Rover is already scanned", "Ok");
            }

        }

        public async Task RegistrateBaseSimcard(QRSticker result)
        {
            // Scan label for Simcard info
            robotItem.BaseGPS.ofType = GPSType.Base;

           
            if (string.IsNullOrEmpty(robotItem.BaseGPS.Simcard.ID))
            {
                try
                {
                    GPS response = await GPS.API.GetById(result.ID.Trim());
                    if (response != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Base QR code! " + response.ID, "Ok");
                        BaseSIM = "Base Simcard ID: " + response.ID;
                        OnPropertyChanged(nameof(BaseSIM));

                    }
                    
                }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong..." + e.Message);
                }
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
            await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Tablet QR code!", "Ok");
            TabletSN = "Registered";
            OnPropertyChanged(nameof(TabletSN));
        }

        public async void SaveRobotToDB()
        {
            // Not implemented, but will save every component on the robotPackage instance to the DB
            
            await Application.Current.MainPage.DisplayAlert("Success!", "Robot saved", "OK");
            Application.Current.MainPage = new NavigationPage(new MenuPage());
            await Navigation.PopToRootAsync();
            
        }



        //TODO lav en generisk funktion til at hente components fra DB
        public async Task<T> GetItemFromDB<T>(string id) where T : System.ComponentModel.IComponent, new()
        {

            throw new NotImplementedException();
            return new T();
        }

        
        #endregion
    }
}
