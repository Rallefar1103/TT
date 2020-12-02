using System;
using System.Net.Http;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using TurfTankRegistrationApplication.Model;
using Xamarin.Forms;
using TurfTankRegistrationApplication.Pages;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using TurfTankRegistrationApplication.Helpers;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class RoverRegistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        

        static string RobotIP = "192.168.80.1";
        static string RoverCMD = "SOSVER,0";
        public string RoverResponse { get; set; }

        public Command ChangeRoverSimcard { get; }
        public Command ChangeRoverSN { get; }
        public Command ScanForWifi { get; }
        public Command ConnectToSelectedWifi { get; }

        public string RoverSerialNumber { get; set; }

        public List<string> wifiResults { get; set; }
        public string SelectedNetwork { get; set; }

        // UI bools
        public bool HasNotStartedWifiLoading { get; set; } = true;
        public bool ShowLoadingLabel { get; set; }
        public bool WifiListIsReady { get; set; } = false;
        public bool StartedConnecting { get; set; } = false;
        

        public RoverRegistrationViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            ChangeRoverSimcard = new Command(() => NavigateToScanPage("Rover"));
            ChangeRoverSN = new Command(() => GetRoverSerialNumber(RoverCMD));
            
        }

        public RoverRegistrationViewModel()
        {

        }

        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        public async void GetRoverSerialNumber(string cmd)
        {
            HttpClient http = new HttpClient();
            string URL = $"http://{RobotIP}:8888/rover";
            var values = new Dictionary<string, string>
            {
                { "Command", cmd },
            };

            var content = new FormUrlEncodedContent(values);

            try
            {
                //var response = await http.PostAsync(URL, content);
                //if (response.IsSuccessStatusCode)
                //{
                //    string StringContent = await response.Content.ReadAsStringAsync();

                //    Console.WriteLine("!!!!! ------- This is what we got back: " + StringContent);
                //    dynamic json = JsonConvert.DeserializeObject(StringContent);

                //    SOSVER RoverSOSVER = new SOSVER(json["response"]);
                //    roverResponse = RoverSOSVER.SerialNumber;

                //    Console.WriteLine(roverResponse);
                //}

                //Test
               var response = await http.GetAsync("https://jsonplaceholder.typicode.com/users");
                if (response.IsSuccessStatusCode)
                {
                    string StringContent = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(StringContent);
                    Console.WriteLine("!!!!!!!! ------- THIS IS WHAT WE GOT: \n" + json);

                    RoverResponse = json[0]["name"];
                    Console.WriteLine("THIS IS ROVER RESPONSE: \n" + RoverResponse);
                    await Application.Current.MainPage.DisplayAlert("Success!", "Got Serial Number for rover: " + RoverResponse, "Add to Robot");
                    await Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("ERROR!", "Could not retrieve serial number from rover", "OK");
                    Console.WriteLine("BAD RESPONSE CODE!!!!!");
                }
            } catch
            {
                throw new HttpRequestException();
            }


            MessagingCenter.Send(this, "RoverSerialNumber", RoverResponse);
        }

        
        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
