using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;
using System.Collections.Generic;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class RoverRegistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public Command DidChangeRoverSimcard { get; }
        public Command DidChangeRoverSN { get;  }

        public event PropertyChangedEventHandler PropertyChanged;

        public RoverRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            DidChangeRoverSimcard = new Command(() => NavigateToScanPage("Rover"));
            DidChangeRoverSN = new Command(() => TryConnecting());
        }


        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        public bool TryConnecting()
        {
            bool result = false;
            result = DependencyService.Get<IWifiConnector>().ConnectToWifi();
            return result;
        }

        public void Scanner()
        {
            List<string> results = DependencyService.Get<IWifiConnector>().GetAvailableNetworks();
            if (results != null)
            {
                Console.WriteLine("!!!!! ---------------------  RESULTS: \n");
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            } else
            {
                Console.WriteLine("Uhm.... something wrong went!");
            }
        }

        // We might need to move this logic to the Model
        public void GetRoverSerialNumber()
        {
            bool gotConnected = false;
            Console.WriteLine("Start connecting to wifi");
            gotConnected = TryConnecting();
            if (gotConnected)
            {
                Console.WriteLine("WE ARE CONNECTED!");
            } else
            {
                Console.WriteLine("OOPS WE END UP HERE!");
            }
            // Use Connectivity to check if we are online or not 
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
