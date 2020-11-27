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
        public List<string> wifiResults { get; set; }
        public Command DidChangeRoverSimcard { get; }
        public Command DidChangeRoverSN { get;  }
        public bool IsDoneLoading { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public Action<IWifiConnector, List<string>> Callback { get; set; }

        public RoverRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            DidChangeRoverSimcard = new Command(() => NavigateToScanPage("Rover"));
            DidChangeRoverSN = new Command(() => Scanner());
            
            
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

        // Needs to wait for the result to finish
        public async void Scanner()
        {
            Task<List<string>> wifiTask = DependencyService.Get<IWifiConnector>().GetAvailableNetworks();
            await Task.Delay(8000);
            if (wifiTask.Status == TaskStatus.RanToCompletion)
            {
                Console.WriteLine("DONE");
                IsDoneLoading = true;
                wifiResults = wifiTask.Result;
                foreach (var network in wifiResults)
                {
                    Console.WriteLine("Result: " + network);
                }
                //MessagingCenter.Send(this, "wifiLoaded", IsDoneLoading);

            } else if (wifiTask.Status == TaskStatus.Running)
            {

                Console.WriteLine("RUNNING");

            } else if (wifiTask.Status == TaskStatus.WaitingToRun)
            {
                Console.WriteLine("WAITING");
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
