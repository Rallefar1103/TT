using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TurfTankRegistration.Models;
using TurfTankRegistration.Views;
using Xamarin.Forms;

namespace TurfTankRegistration.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        //Models
        public Robot CurrentRobot = new Robot();

        public RegistrationViewModel()
        {
            BaseCommand = new Command(execute: () => { RegisterBase(); });
            ControllerCommand = new Command(execute: () => { RegisterController(); });
            RoverCommand = new Command(execute: () => { RegisterRover(); });
            TabletCommand = new Command(execute: () => { RegisterTablet(); });
            RobotCommand = new Command(execute: () => { RegisterRobot(); });
            DoneCommand = new Command(execute: () => { Done(); }, canExecute: () => CurrentRobot.CheckForCompleteRobot());
        }
        public ICommand BaseCommand { get; }
        private string basenum;
        public string BaseNum { get=>basenum; set { SetProperty(ref basenum, value); } }
        public void RegisterBase()
        {
            Base NewBase = new Base();
            CurrentRobot.RegisteredBase = NewBase;
            CurrentRobot.RegisteredBase.SerialNumber = "ASFOAHP24";
            BaseNum = CurrentRobot.RegisteredBase.SerialNumber;
            if (CurrentRobot.CheckForCompleteRobot())
                (DoneCommand as Command).ChangeCanExecute();
        }
        public ICommand ControllerCommand { get; }
        private string controllernum;
        public string ControllerNum { get=>controllernum; set { SetProperty(ref controllernum, value); } }
        public void RegisterController()
        {
            Controller NewController = new Controller();
            CurrentRobot.RegisteredController = NewController;
            CurrentRobot.RegisteredController.SerialNumber = "HIASGLA4";
            ControllerNum = CurrentRobot.RegisteredController.SerialNumber;
            if (CurrentRobot.CheckForCompleteRobot())
                (DoneCommand as Command).ChangeCanExecute();
        }
        public ICommand RoverCommand { get; }
        private string rovernum;
        public string RoverNum { get=>rovernum; set { SetProperty(ref rovernum, value); } }
        public void RegisterRover()
        {
            Rover NewRover = new Rover();
            CurrentRobot.RegisteredRover = NewRover;
            CurrentRobot.RegisteredRover.SerialNumber = "HASG#1234";
            RoverNum = CurrentRobot.RegisteredRover.SerialNumber;
            if (CurrentRobot.CheckForCompleteRobot())
                (DoneCommand as Command).ChangeCanExecute();
        }
        public ICommand TabletCommand { get; }
        private string tabletnum;
        public string TabletNum { get=>tabletnum; set { SetProperty(ref tabletnum, value); } }
        public void RegisterTablet()
        {
            Tablet NewTablet = new Tablet();
            CurrentRobot.RegisteredTablet = NewTablet;
            CurrentRobot.RegisteredTablet.SerialNumber = "JGAFYFY12";
            TabletNum = CurrentRobot.RegisteredTablet.SerialNumber;
            if (CurrentRobot.CheckForCompleteRobot())
                (DoneCommand as Command).ChangeCanExecute();
        }
        public ICommand RobotCommand { get; }
        private string robotnum;
        public string RobotNum { get=>robotnum; set { SetProperty(ref robotnum, value); } }
        public void RegisterRobot()
        {
            CurrentRobot.SerialNumber = "FGHJKL3";
            RobotNum = CurrentRobot.SerialNumber;
            if (CurrentRobot.CheckForCompleteRobot())
                (DoneCommand as Command).ChangeCanExecute();
        }
        public ICommand DoneCommand { get; }
        public async void Done()
        {
            await App.Current.MainPage.Navigation.PushAsync(new DoneRegistrationPage());
        }
    }
}
