using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class RoverPreregistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public string QRScannedData { get; set; }
        public string BarcodeScannedData { get; set; }


        public QRSticker RoverQR { get; set; }
        public SimCard RoverSimcard { get; set; }

        public Action<object, string> Callback { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public Command ScanRoverQR { get; }
        public Command ScanRoverSim { get;  }
        public Command ConfirmPreregistration { get;  }

        // UI Bools
        public bool CanScanSQ { get; set; } = true;
        public bool CanScanBarcode { get; set; } = false;
        public bool CanConfirm { get; set; } = false;
        public bool toggledQRMounted { get; set; } = false;
        public bool toggleSimcardMounted { get; set; } = false;

        // UI Color properties
        public Color ScanQRColor { get; set; }
        public Color ScanBarcodeColor { get; set; }

        public RoverPreregistrationViewModel(INavigation nav)
        {
            this.Navigation = nav;
            this.RoverQR = new QRSticker();
            CanScanSQ = true;
            Callback = new Action<object, string>(OnDataReceived);
            //ScanRoverQR = new Command(execute: async () => await DummyScanQR(), canExecute: () => CanScanSQ);
            ScanRoverQR = new Command(() => NavigateToScanPage("rover"));

            //ScanRoverSim = new Command(execute: async () => await DummyScanBarcode(), canExecute: () => CanScanBarcode);
            ScanRoverSim = new Command(() => NavigateToScanPage(""));

            ConfirmPreregistration = new Command(execute: async () => await DummyConfirm());
            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);
        }


        private async Task DummyScanQR()
        {

            await Task.Delay(2000);
            await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Rover QR-label " + RoverQR.ID, "OK");

            CanScanBarcode = true;
            OnPropertyChanged(nameof(CanScanBarcode));

            ScanQRColor = Color.DarkGreen;
            OnPropertyChanged(nameof(ScanQRColor));

        }

        private async Task DummyScanBarcode()
        {
            
            await Task.Delay(2000);
            await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the barcode", "Ok");

            CanConfirm = true;
            OnPropertyChanged(nameof(CanConfirm));

            ScanBarcodeColor = Color.DarkGreen;
            OnPropertyChanged(nameof(ScanBarcodeColor));

        }

        private async Task DummyConfirm()
        {
            await Task.Delay(2000);
            if (toggledQRMounted && toggleSimcardMounted)
            {
                await Application.Current.MainPage.DisplayAlert("Success!", "You succesfully preregistered the Rover", "Ok");
                await Navigation.PopAsync();
            } else
            {
                await Application.Current.MainPage.DisplayAlert("OOPS!", "Have you remembered mount the QR label and insert the simcard?", "Ok");
            }
            
        }

        public void toggleQRTapped()
        {
            toggledQRMounted = true;
            OnPropertyChanged(nameof(toggledQRMounted));
        }

        public void toggleSimcardTapped()
        {
            toggledQRMounted = true;
            OnPropertyChanged(nameof(toggleSimcardMounted));
        }








        public void NavigateToScanPage(string component)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + component;
            scanPage.QRMustContain = component;
            Navigation.PushAsync(scanPage);
        }

        private async void OnDataReceived(object sender, string data)
        {
            try
            {
                if (data.Contains("rover"))
                {
                    QRScannedData = data;
                    //RoverQR = new QRSticker(data);
                    await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Rover QR-label " + RoverQR.ID, "OK");
                    CanScanBarcode = true;
                    OnPropertyChanged(nameof(CanScanBarcode));
                    //RoverQR.ID = QRScannedData;
                    //OnPropertyChanged(nameof(QRScannedData));

                    ScanQRColor = Color.DarkGreen;
                    OnPropertyChanged(nameof(ScanQRColor));


                }
                else 
                {
                    await Application.Current.MainPage.DisplayAlert("OBS!", "Scan did not work as expected!", "Ok");
                }
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", e.Message, "Ok");
            }
        }

        private async Task Confirm()
        {
            await Application.Current.MainPage.DisplayAlert("Success!", "You succesfully preregistered the Rover", "Ok");
            await Navigation.PopAsync();
            
        }

        private async Task ScanSim()
        {
            await Application.Current.MainPage.DisplayAlert("Success!", "You succesfully preregistered the Rover", "Ok");
            CanConfirm = true;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
