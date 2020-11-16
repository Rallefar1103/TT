using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Security.Cryptography.X509Certificates;

using TurfTankRegistrationApplication.Model;
using System.Diagnostics;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class ViewModel1 
    {
        public IEmployee User { get; set; }
        //public Class1 AFiveObject { get; set; }
        public ViewModel1() // "Normal" constructor
        {
            User = new Employee("5", "SomePassword");
            PressForFiveCommand = new Command(execute: () => PressForFive()); // Delegates the handling of the command to a method
        }
        public ViewModel1(IEmployee user) // "Normal" constructor
        {
            User = user;
            PressForFiveCommand = new Command(execute: () => PressForFive()); // Delegates the handling of the command to a method
        }
        public ICommand PressForFiveCommand { get; }
        private string showfive = "No Five";
        //public string ShowFive { get => showfive; set { SetProperty(ref showfive, value); } }
        public void PressForFive() // Handles the logic of what should happen when a button is pressed
        {
            
            //ShowFive = User.Username;
        }
    }
}
