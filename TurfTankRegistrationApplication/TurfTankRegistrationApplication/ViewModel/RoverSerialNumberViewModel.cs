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
    public class RoverSerialNumberViewModel : INotifyPropertyChanged
    {
        public INavigation navigation { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public HttpClient http { get; set; }
        public bool testing { get; set; }
        public GPS Rover { get; set; }

        public bool isOperation { get; set; }
        public bool isConfiguration { get; set; }

        // UI bools
        public bool showStopInmark { get; set; } = true;
        public bool showGetRoverSerial { get; set; } = false;
        public bool showStartInmark { get; set; } = false;

        public bool showStopInmarkLabel { get; set; } = false;
        public bool showStartInmarkLabel { get; set; } = false;
        public bool showRoverRetrievalLabel { get; set; } = false;

        public Command stoppingInmark { get; }
        public Command startingInmark { get; }
        public Command RetrieveSerialNumber { get; }

        public RoverSerialNumberViewModel(INavigation nav)
        {
            this.navigation = nav;
            this.testing = false;
            this.http = App.WifiClient;
            stoppingInmark = new Command(async () => await StopInmark());
            startingInmark = new Command(async () => await StartInmark());
            RetrieveSerialNumber = new Command(async () => await GetRoverSerialNumber());
        }

        /// <summary>
        /// GetRoverSerialNumber sends a postrequest to the rover endpoint once you're connected to the specific robot's wifi
        /// Before we can get any information from the rover we need to call the StopInmark method.
        /// We get the response back and parses it through the SOSVER class to retrieve the actual serial number
        /// When we're done we call the StartInmark method.
        /// </summary>
        public async Task GetRoverSerialNumber()
        {
            Console.WriteLine("!!!!!!! ---------------   Retrieving rover serial number! -------------- !!!!!!!!!");
            string URL = "http://192.168.80.1:8888/rover";
            var values = new Dictionary<string, string>
            {
                { "command", "SOSVER,0" },
            };

            var content = new FormUrlEncodedContent(values);

            showGetRoverSerial = false;
            OnPropertyChanged(nameof(showGetRoverSerial));

            showRoverRetrievalLabel = true;
            OnPropertyChanged(nameof(showRoverRetrievalLabel));

            if (isConfiguration)
            {
                try
                {
                    var response = await http.PostAsync(URL, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string StringContent = await response.Content.ReadAsStringAsync();

                        dynamic json = JsonConvert.DeserializeObject(StringContent);

                        SOSVER RoverSOSVER = new SOSVER(json["response"].ToString());

                        Rover.SerialNumber = RoverSOSVER.SerialNumber;

                        Console.WriteLine("Rover Serial Number: " + Rover.SerialNumber);

                        if (!testing)
                        {
                            MessagingCenter.Send(this, "RoverSerialNumber", Rover.SerialNumber);
                            await Application.Current.MainPage.DisplayAlert("Success!", "Got Rover Serial Number: " + Rover.SerialNumber, "OK");

                            showRoverRetrievalLabel = false;
                            OnPropertyChanged(nameof(showRoverRetrievalLabel));

                            showStartInmark = true;
                            OnPropertyChanged(nameof(showStartInmark));

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("OOPS!", "Something went wrong retrieving the serial number", "OK");
                            await navigation.PopAsync();
                        }

                    }

                }
                catch (HttpRequestException e)
                {
                    await Application.Current.MainPage.DisplayAlert("OOPS!", "Did catch an exception" + e, "OK");
                    Console.WriteLine("CATCH: " + e);
                }
            }
               
            
            

        }

        /// <summary>
        // This method gives us the opportunity to communicate with the rover by switching its mode
        // from "operation" to "configuration"
        /// </summary>
        /// <returns></returns>
        private async Task StopInmark()
        {
            string URL = "http://192.168.80.1:8888/stopInmark";
            var values = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(values);

            Console.WriteLine("Started StopInmark");
            showStopInmark = false;
            OnPropertyChanged(nameof(showStopInmark));

            showStopInmarkLabel = true;
            OnPropertyChanged(nameof(showStopInmarkLabel));

            try
            {
                http.Timeout = TimeSpan.FromMilliseconds(8000);
                var response = await http.PostAsync(URL, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("!!!!! ------- Stopping!");
                    isConfiguration = true;

                    showStopInmarkLabel = false;
                    OnPropertyChanged(nameof(showStopInmarkLabel));

                    await Application.Current.MainPage.DisplayAlert("Success!", "Completed Stop Inmark", "OK");

                    showGetRoverSerial = true;
                    OnPropertyChanged(nameof(showGetRoverSerial));

                }
                else 
                {
                    Console.WriteLine("!!!!! ------- Stopping failed!");
                    await Application.Current.MainPage.DisplayAlert("OOPS!", "Could not complete Stop Inmark", "OK");
                    isConfiguration = false;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("CATCH: " + e);
            }

        }



        /// <summary>
        // This method restarts the robot such that the production employee can continue with the
        // testing and reassembling process
        /// </summary>
        /// <returns></returns>
        private async Task<bool> StartInmark()
        {
            string URL = "http://192.168.80.1:8888/startInmark";
            var values = new Dictionary<string, string>();

            var content = new FormUrlEncodedContent(values);

            showStartInmark = false;
            OnPropertyChanged(nameof(showStartInmark));

            showStartInmarkLabel = true;
            OnPropertyChanged(nameof(showStartInmarkLabel));

            try
            {
                http.Timeout = TimeSpan.FromMilliseconds(8000);
                var response = await http.PostAsync(URL, content);

                if (response.IsSuccessStatusCode)
                {

                    Console.WriteLine("!!!!! ------- Starting!");
                    isOperation = true;

                    await Application.Current.MainPage.DisplayAlert("Success!", "Completed Start Inmark", "OK");

                    showStartInmarkLabel = false;
                    OnPropertyChanged(nameof(showStartInmarkLabel));

                    await navigation.PopAsync();

                }
                else
                {
                    Console.WriteLine("!!!!! ------- Starting failed!");
                    isOperation = false;
                    await Application.Current.MainPage.DisplayAlert("OOPS!", "Could not complete Start Inmark", "OK");
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

        #region testing
        public async Task DummyStopInmark()
        {
            Console.WriteLine("Started StopInmark");
            showStopInmark = false;
            OnPropertyChanged(nameof(showStopInmark));

            showStopInmarkLabel = true;
            OnPropertyChanged(nameof(showStopInmarkLabel));

            await Task.Delay(2000);

            showStopInmarkLabel = false;
            OnPropertyChanged(nameof(showStopInmarkLabel));

            await Application.Current.MainPage.DisplayAlert("Success!", "Completed Stop Inmark", "OK");

            showGetRoverSerial = true;
            OnPropertyChanged(nameof(showGetRoverSerial));

        }

        public async Task DummyStartInmark()
        {
            Console.WriteLine("Started StartInmark");

            showStartInmark = false;
            OnPropertyChanged(nameof(showStartInmark));

            showStartInmarkLabel = true;
            OnPropertyChanged(nameof(showStartInmarkLabel));

            await Task.Delay(2000);

            await Application.Current.MainPage.DisplayAlert("Success!", "Completed Start Inmark", "OK");

            showStartInmarkLabel = false;
            OnPropertyChanged(nameof(showStartInmarkLabel));

            await navigation.PopAsync();
        }

        public async Task DummyRoverSerial()
        {
            Console.WriteLine("Started RoverSerial Retrieval");
            showGetRoverSerial = false;
            OnPropertyChanged(nameof(showGetRoverSerial));

            showRoverRetrievalLabel = true;
            OnPropertyChanged(nameof(showRoverRetrievalLabel));

            await Task.Delay(2000);

            showRoverRetrievalLabel = false;
            OnPropertyChanged(nameof(showRoverRetrievalLabel));

            await Application.Current.MainPage.DisplayAlert("Success!", "Got Rover Serial Number", "OK");


            showStartInmark = true;
            OnPropertyChanged(nameof(showStartInmark));
        }
        #endregion
    }
}
