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
using System.Net;
using System.Text;
using System.IO;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class RoverRegistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        
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
            ChangeRoverSN = new Command(() => GetRoverSerialNumber());
            
        }

        public RoverRegistrationViewModel()
        {

        }

        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = "Type:" + component;
            Navigation.PushAsync(scanPage);
        }

        /// <summary>
        /// GetRoverSerialNumber sends a postrequest to the rover endpoint once you're connected to the specific robot's wifi
        /// Before we can get any information from the rover we need to call the StopInmark method.
        /// We get the response back and parses it through the SOSVER class to retrieve the actual serial number
        /// When we're done we call the StartInmark method.
        /// </summary>
        public async void GetRoverSerialNumber()
        {
            HttpClient http = new HttpClient();
            string URL = "http://192.168.80.1:8888/rover";
            var values = new Dictionary<string, string>
            {
                { "command", "SOSVER,0" },
            };
            
            var content = new FormUrlEncodedContent(values);

            var successfullStop = await StopInmark();

            if (successfullStop)
            {
                try
                {
                    var response = await http.PostAsync(URL, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string StringContent = await response.Content.ReadAsStringAsync();

                        Console.WriteLine("!!!!! ------- This is what we got back: " + StringContent);

                        dynamic json = JsonConvert.DeserializeObject(StringContent);

                        Console.Write("!!!!! ------ response: " + json["response"]);
                        SOSVER RoverSOSVER = new SOSVER(json["response"]);

                        RoverResponse = RoverSOSVER.SerialNumber;

                        Console.WriteLine("Rover Serial Number: " + RoverResponse);
                        MessagingCenter.Send(this, "RoverSerialNumber", RoverResponse);

                        await Application.Current.MainPage.DisplayAlert("Success!", "Got the Rover Serial Number!", "OK");
                        
                    }

                    await StartInmark();

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("CATCH: " + e);
                }
            }
            else
            {
                Console.WriteLine("Something went wrong with the stopInmark!");
            }

        }



        /// <summary>
        // This method gives us the opportunity to communicate with the rover by switching its mode
        // from "operation" to "configuration"
        /// </summary>
        /// <returns></returns>
        private async Task<bool> StopInmark()
        {
            HttpClient http = new HttpClient();
            string URL = "http://192.168.80.1:8888/stopInmark";
            var values = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(values);
            try
            {
                var response = await http.PostAsync(URL, content);
                var responseSucces = response.IsSuccessStatusCode;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("!!!!! ------- Stopping!");
                    return true;
                    
                } else
                {
                    Console.WriteLine("!!!!! ------- Stopping failed!");
                    return false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("CATCH: " + e);
            }

            return false;
        }



        /// <summary>
        // This method restarts the robot such that the production employee can continue with the
        // testing and reassembling process
        /// </summary>
        /// <returns></returns>
        private async Task<bool> StartInmark()
        {
            HttpClient http = new HttpClient();
            string URL = "http://192.168.80.1:8888/startInmark";
            var values = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(values);
            try
            {
                var response = await http.PostAsync(URL, content);
                var responseSucces = response.IsSuccessStatusCode;

                if (response.IsSuccessStatusCode)
                {
                    
                    Console.WriteLine("!!!!! ------- Starting!");
                    return true;

                } else
                {
                    Console.WriteLine("!!!!! ------- Starting failed!");
                    return false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("CATCH: " + e);
            }

            return false;
        }

        
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



//Test
//var response = await http.GetAsync("https://jsonplaceholder.typicode.com/users");
//if (response.IsSuccessStatusCode)
//{
//    string StringContent = await response.Content.ReadAsStringAsync();
//    dynamic json = JsonConvert.DeserializeObject(StringContent);
//    Console.WriteLine("!!!!!!!! ------- THIS IS WHAT WE GOT: \n" + json);

//    RoverResponse = json[0]["name"];
//    Console.WriteLine("THIS IS ROVER RESPONSE: \n" + RoverResponse);
//    await Application.Current.MainPage.DisplayAlert("Success!", "Got Serial Number for rover: " + RoverResponse, "Add to Robot");
//    await Navigation.PopAsync();
//}
//else
//{
//    await Application.Current.MainPage.DisplayAlert("ERROR!", "Could not retrieve serial number from rover", "OK");
//    Console.WriteLine("BAD RESPONSE CODE!!!!!");
//}