using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class PreRegistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public string Rover { get; set; } = "Rover";
        public string Base { get; set; } = "Base";

        public Command RegistrateRover { get; }
        public Command RegistrateBase { get; }

        public PreRegistrationViewModel(INavigation navigation)
        {
            RegistrateRover = new Command(PrintWorks);
            this.Navigation = navigation;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void PrintWorks()
        {
            Console.WriteLine("It works!");
        }
    }
}
