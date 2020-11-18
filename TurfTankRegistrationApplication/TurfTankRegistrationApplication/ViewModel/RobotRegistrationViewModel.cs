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
        void RegistrateChassis();
        void RegistrateController();
        void RegistrateRover();
        void RegistrateBase();
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
            robotItem.RoverGPS.ID = "13141516";
            robotItem.BaseGPS.ID = "17181920";
            robotItem.Tablet.ID = "212223345";

            this.Navigation = navigation;
            robotItem.SetAsSelected();

            DidChangeChassisSN = new Command(RegistrateChassis);
            DidChangeControllerSN = new Command(RegistrateController);
            DidChangeRoverSN = new Command(RegistrateRover);
            DidChangeBaseSN = new Command(RegistrateBase);
            DidChangeTabletSN = new Command(RegistrateTablet);
            Callback = new Action<object, string>(OnDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);

        }

        private void OnDataReceived(object sender, string data)
        {
            if (data.Contains("Robot"))
            {
                robotItem.SerialNumber = data;
                ChassisSN = robotItem.SerialNumber;
                OnPropertyChanged(nameof(ChassisSN));

            }
            else if (data.Contains("Controller"))
            {
                robotItem.Controller.ID = data;
                ControllerSN = robotItem.Controller.ID;
                OnPropertyChanged(nameof(ControllerSN));
            }
            else if (data.Contains("Rover"))
            {
                robotItem.RoverGPS.ofType = GPS.GPSType.Rover;
                robotItem.RoverGPS.ID = data;
                RoverSN = robotItem.RoverGPS.ID;
                OnPropertyChanged(nameof(RoverSN));
            }
            else if (data.Contains("Base"))
            {
                robotItem.BaseGPS.ofType = GPS.GPSType.Base;
                robotItem.BaseGPS.ID = data;
                BaseSN = robotItem.BaseGPS.ID;
                OnPropertyChanged(nameof(BaseSN));
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
        public void NavigateToScanPage()
        {
            Navigation.PushAsync(new ScanPage());
        }

        #endregion

        #region public methods
        public void RegistrateChassis()
        {
            NavigateToScanPage();
        }

        public void RegistrateController()
        {
            NavigateToScanPage();
        }
        

        public void RegistrateRover()
        {
            // Scan label for SimCard information
            NavigateToScanPage();

            // Retrieve SimCard SN from DB

            // Connect to Controller WiFi
            robotItem.Controller.SetupWifi();

            // Retrieve Rover SN from Controller
        }

        public async void RegistrateBase()
        {
            BaseSN = robotItem.BaseGPS.GetSerialNumber();
            if (BaseSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Base Scanned", "Ok");
                OnPropertyChanged(nameof(BaseSN));
            }
            else
            {
                Console.WriteLine("Error retrieving Base SN");
            }
        }

        public async void RegistrateTablet()
        {
            TabletSN = robotItem.Tablet.GetSerialNumber();
            if (TabletSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Tablet Scanned", "Ok");
                OnPropertyChanged(nameof(TabletSN));
            }
            else
            {
                Console.WriteLine("Error retrieving Tablet SN");
            }
        }

        public void SaveRobot()
        {
            // Not implemented, but will save every component on the robotPackage instance to the DB
        }
        #endregion
    }
}
