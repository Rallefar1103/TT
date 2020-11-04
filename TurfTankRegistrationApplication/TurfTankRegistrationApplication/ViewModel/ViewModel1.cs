using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using System.Security.Cryptography.X509Certificates;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class ViewModel1 : BaseViewModel
    {
        //public Class1 AFiveObject { get; set; }
        public ViewModel1() // "Normal" constructor
        {
            //AFiveObject = new Class1();
            PressForFiveCommand = new Command(execute: () => PressForFive()); // Delegates the handling of the command to a method
        }
        public ICommand PressForFiveCommand { get; }
        private string showfive = "No Five";
        public string ShowFive { get => showfive; set { SetProperty(ref showfive, value); } }
        public void PressForFive() // Handles the logic of what should happen when a button is pressed
        {
            ShowFive = $"5";
        }
    }
}
