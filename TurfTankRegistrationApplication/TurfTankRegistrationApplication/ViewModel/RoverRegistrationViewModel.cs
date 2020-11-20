using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;
namespace TurfTankRegistrationApplication.ViewModel
{
    public class RoverRegistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public Command DidChangeRoverSimcard { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RoverRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            DidChangeRoverSimcard = new Command(() => NavigateToScanPage("Rover"));
        }


        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        public void GetRoverSerialNumber()
        {
            Console.WriteLine("Start connecting to wifi");
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
