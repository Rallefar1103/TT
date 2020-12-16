using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class WifiPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public List<string> wifiResults { get; set; }
        public string SelectedNetwork { get; set; }

        public Command ScanForWifi { get; }
        public Command ConnectToSelectedWifi { get; }

        // UI bools
        public bool HasNotStartedWifiLoading { get; set; } = true;
        public bool ShowLoadingLabel { get; set; }
        public bool WifiListIsReady { get; set; } = false;
        public bool StartedConnecting { get; set; } = false;

        public WifiPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            ScanForWifi = new Command( async () => await Scanner());
            ConnectToSelectedWifi = new Command(() => ConnectToRobot());
        }

        public async Task Scanner()
        {
            var status = await Permissions.RequestAsync<Permissions.LocationAlways>();

            if (status == PermissionStatus.Granted)
            {
                Task<List<string>> wifiTask = DependencyService.Get<IWifiConnector>().GetAvailableNetworks();
                HasNotStartedWifiLoading = false;
                OnPropertyChanged(nameof(HasNotStartedWifiLoading));

                ShowLoadingLabel = true;
                OnPropertyChanged(nameof(ShowLoadingLabel));

                // Needs to wait for the result to finish
                await Task.Delay(8000);
                if (wifiTask.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("DONE");
                    ShowLoadingLabel = false;
                    OnPropertyChanged(nameof(ShowLoadingLabel));

                    wifiResults = wifiTask.Result.Where(element => !string.IsNullOrEmpty(element)).ToList();
                    OnPropertyChanged(nameof(wifiResults));

                    if (wifiResults.Count != 0)
                    {
                        foreach (var network in wifiResults)
                        {
                            Console.WriteLine("Result: " + network);
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("OOPS!", "Could not find any wifi networks", "OK");
                    }


                    WifiListIsReady = true;
                    OnPropertyChanged(nameof(WifiListIsReady));

                }
                else if (wifiTask.Status == TaskStatus.Running)
                {
                    Console.WriteLine("RUNNING");

                }
                else if (wifiTask.Status == TaskStatus.WaitingToRun)
                {
                    Console.WriteLine("WAITING");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("OOPS!", "You need to allow location", "OK");
            }
            

        }

        public string GetCorrectWifi(string ssid)
        {
            ssid.Trim();
            string foundNetwork = wifiResults.Find(wifi => wifi == ssid);
            return foundNetwork;
        }

        // We might need to move this logic to the Model
        public async void ConnectToRobot()
        {
            string ConnectedSSID = "";
            string ssid = GetCorrectWifi(SelectedNetwork);

            // Connect to wifi
            DependencyService.Get<IWifiConnector>().ConnectToWifi(SelectedNetwork);

            WifiListIsReady = false;
            OnPropertyChanged(nameof(WifiListIsReady));

            StartedConnecting = true;
            OnPropertyChanged(nameof(StartedConnecting));

            // Wait for the connection to be established
            await Task.Delay(5000);

            StartedConnecting = false;
            OnPropertyChanged(nameof(StartedConnecting));

            if (DependencyService.Get<IWifiConnector>().CheckWifiStatus())
            {
                ConnectedSSID = DependencyService.Get<IWifiConnector>().GetSSID();
            }


            if ($"\"{SelectedNetwork}\"" == ConnectedSSID && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                Console.WriteLine("WE ARE CONNECTED!");
                await Application.Current.MainPage.DisplayAlert("Success!", "You are connected to: " + ssid, "Start Registration");
                await Navigation.PushAsync(new RobotRegistrationPage(ssid));

            }
            else
            {
                Console.WriteLine("OOPS WE END UP HERE!");
                await Application.Current.MainPage.DisplayAlert("OOPS!", "You did not connect to: " + ssid, "OK");
                await Navigation.PopAsync();
                await Navigation.PopAsync();
            }
 
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
