using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public interface IRegistrateRobot
    {
        void RegistrateChassis();
        void RegistrateControllerAsync();
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

        public RobotRegistrationViewModel()
        {
            DidChangeChassisSN = new Command(RegistrateChassis);
            DidChangeControllerSN = new Command(RegistrateControllerAsync);
            DidChangeRoverSN = new Command(RegistrateRover);
            DidChangeBaseSN = new Command(RegistrateBase);
            DidChangeTabletSN = new Command(RegistrateTablet);    
        }

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

        public async void RegistrateChassis()
        {
            ChassisSN = "123456";
            //robotItem.SerialnumberFromChassi = ChassisSN;
            if (ChassisSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Chassis Scanned", "Ok");
                OnPropertyChanged(nameof(ChassisSN));
            }
        }

        public async void RegistrateControllerAsync()
        {
            ControllerSN = "123456";
            //robotItem.Controller.ID = ControllerSN;
            if (ControllerSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Controller Scanned", "Ok");
                OnPropertyChanged(nameof(ControllerSN));
            }
        }

        public async void RegistrateRover()
        {
            RoverSN = "123456";
            //robotItem.RoverGps.ID = RoverSN;
            if (RoverSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Rover Scanned", "Ok");
                OnPropertyChanged(nameof(RoverSN));
            }
        }

        public async void RegistrateBase()
        {
            BaseSN = "123456";
            //robotItem.BaseGps.ID = BaseSN;
            if (BaseSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Base Scanned", "Ok");
                OnPropertyChanged(nameof(BaseSN));
            }
        }

        public async void RegistrateTablet()
        {
            TabletSN = "123456";
            //robotItem.Tablet.ID = TabletSN;
            if (TabletSN.Length > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "Tablet Scanned", "Ok");
                OnPropertyChanged(nameof(TabletSN));
            }
        }

        public void SaveRobot()
        {
            // Not implemented
        }
    }
}
