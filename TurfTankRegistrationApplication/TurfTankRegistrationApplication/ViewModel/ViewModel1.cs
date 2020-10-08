using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TurfTankRegistrationApplication.ViewModel;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class ViewModel1 : BaseViewModel
    {
        public ViewModel1()
        {
            PressForFiveCommand = new Command(execute: () => PressForFive());
        }
        public ICommand PressForFiveCommand { get; }
        private string showfive = "No Five";
        public string ShowFive { get => showfive; set { SetProperty(ref showfive, value); } }
        public int PressForFive()
        {
            Class1 FiveObject = new Class1();
            ShowFive = $"{FiveObject.FiveVar}";
            return FiveObject.FiveVar;
        }
    }
}
