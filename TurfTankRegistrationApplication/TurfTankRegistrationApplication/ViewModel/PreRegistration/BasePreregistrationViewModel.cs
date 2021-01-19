using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class BasePreregistrationViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public string QRScannedData { get; set; }

        public QRSticker BaseQR { get; set; }
        public SimCard BaseSimcard { get; set; }

        public Action<object, string> Callback { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public Command ScanBaseQR { get; }
        public Command ScanBaseSim { get; }
        public Command ConfirmPreregistration { get; }

        // UI Bools
        public bool CanScanSQ { get; set; } = true;
        public bool CanScanBarcode { get; set; } = false;
        public bool CanConfirm { get; set; } = false;
        public bool toggledQRMounted { get; set; } = false;
        public bool toggleSimcardMounted { get; set; } = false;

        // UI Color properties
        public Color ScanQRColor { get; set; } = Color.LightBlue;
        public Color ScanBarcodeColor { get; set; }

        public BasePreregistrationViewModel(INavigation nav)
        {
            this.Navigation = nav;
            this.BaseQR = new QRSticker();
            CanScanSQ = true;
            Callback = new Action<object, string>(OnDataReceived);
            ScanBaseQR = new Command(execute: async () => await DummyScanQR(), canExecute: () => CanScanSQ);
            ScanBaseSim = new Command(execute: async () => await DummyScanBarcode(), canExecute: () => CanScanBarcode);
            ConfirmPreregistration = new Command(execute: async () => await DummyConfirm());
            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);
        }


        private async Task DummyScanQR()
        {
            await Task.Delay(2000);
            await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Basestation QR-label ", "OK");

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
                await Application.Current.MainPage.DisplayAlert("Success!", "You succesfully preregistered the Basestation", "Ok");
                await Navigation.PopAsync();
            }
            else
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
            
            Navigation.PushAsync(scanPage);
        }

        private async void OnDataReceived(object sender, string data)
        {
            try
            {
                if (data.Contains("basestation"))
                {
                    QRScannedData = data;
                    BaseQR = new QRSticker(data);
                    await Application.Current.MainPage.DisplayAlert("Success!", "Scanned the Base QR-label " + BaseQR.ID, "OK");

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

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
