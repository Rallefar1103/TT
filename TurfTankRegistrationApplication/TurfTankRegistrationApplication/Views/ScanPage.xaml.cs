using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Views;
using TurfTankRegistrationApplication.ViewModel;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;
/// <summary>
/// TODO Lav funktioner som skal håndtere: statsene i enumen state
/// når en QR er genkendt skal der ske noget
/// når en qr bliver analyseret skal der ske noget visuelt
/// når qr data er tilgængeligt
/// når qr date gemmes-evt catch error
/// når qr er gemt
/// når scsnner er klar til et nyt scan
/// </summary>
namespace TurfTankRegistrationApplication.Views
{
    public partial class ScanPage : ContentPage
    {
        ScanViewModel vm;

        ZXingScannerView Scanner = new ZXingScannerView();
        enum state
        {
            No_Scanable_Recognized,
            Scannable_discovered,
            Analyzing,
            ScanResult_Ready,
            Saving_Data,
            Saved
        }

        
    
        #region Constructor

        public ScanPage()
        {
            InitializeComponent();

            BindingContext = vm = new ScanViewModel();
            
            Scanner.OnScanResult += Handle_OnScanResult;
            //Add the scanner cameraview
            TheScanner.Content = Scanner;
            Scanner.IsVisible = true;
            //Scanner.IsScanning = true;
            //set the yellow overlay to invisible
            vm.DimmValue = 0;
            //set the info about scannerstate 
            vm.ScannerState = ScanViewModel.state.No_Scanable_Recognized;
            //clean the Result
            vm.Result = " ";

            //QR.Source = "QRSticker.png";
            //QR.IsVisible = true;




        }

        
        #endregion

        #region Button Actions

        //Activate the scanner
        void StartScanner(System.Object sender, System.EventArgs e)
        {
            if (Scanner.IsScanning != true)
            {
                Scanner.IsScanning = true;
                StartScannerButton.Text = "CLEAR SCANNED DATA";
            }
            vm.Result = " ";
            scanner_State.Text = vm.ScannerStateString;
        }



        void Save_Scanned_Data(System.Object sender, System.EventArgs e)
        {
            RegisterBtn.IsEnabled = false;
            SavingData();
        }
        #endregion

        #region States
        void readyToScan()
        {
            QR.IsVisible = false;
            vm.DimmValue = 0;
            vm.ScannerState = ScanViewModel.state.No_Scanable_Recognized;
            scanner_State.Text = vm.ScannerStateString;// nameof(state.No_Scanable_Recognized);
        }

        void ScannableDiscovered()
        {
            vm.ScannerState = ScanViewModel.state.Scannable_discovered;
            scanner_State.Text = vm.ScannerStateString;//
        }

        void AnalyzingScannable()
        {
            vm.ScannerState = ScanViewModel.state.Analyzing;
            scanner_State.Text = vm.ScannerStateString;//
        }

        void ScanResultReady(string result)
        {
            Camera_Data.VerticalOptions = LayoutOptions.Center;
            Camera_Data.HorizontalOptions = LayoutOptions.Center;
            QR.IsVisible = true;
            vm.ScannerState = ScanViewModel.state.ScanResult_Ready;
            scanner_State.Text = vm.ScannerStateString;

            vm.Result = result;
            Camera_Data.TextColor = Color.OrangeRed;
            vm.DimmValue = 0.8;
            RegisterBtn.BackgroundColor = Color.GreenYellow;
            RegisterBtn.IsVisible = true;
            RegisterBtn.IsEnabled = true;

        }

        void SavingData()
        {
            vm.ScannerState = ScanViewModel.state.Saving_Data;
            //vm.Result = $"{vm.Result} er blevet tilknyttet Komponent";
            scanner_State.Text = vm.ScannerStateString;
            RegisterBtn.BackgroundColor = Color.LightGray;

            SimulateSavingScanresult();
        }

        void Saved()
        {
            
            Camera_Data.VerticalOptions = LayoutOptions.Start;
            vm.ScannerState = ScanViewModel.state.Saved;

            scanner_State.Text = vm.ScannerStateString;
            RegisterBtn.BackgroundColor = Color.LightGray;
            RegisterBtn.IsEnabled = false;
            //Camera_Data.Text = vm.Result;
            //Scanner.IsScanning = true;

        }

        #endregion


        public void Handle_OnScanResult(Result result)
        {
            if (string.IsNullOrWhiteSpace(result.Text)) { return; }


            Device.BeginInvokeOnMainThread((Action)(() =>
            {
                if (vm.Result != result.Text)
                {
                    vm.Result = result.Text;
                    SimulateTheScannerStatesInAScan();
                }

                Console.WriteLine($"QR-detected: {result.Text}");
            }));
            Scanner.IsScanning = true;
        }

        #region simulations
        //Simulate a scan
        async void SimulateTheScannerStatesInAScan()
        {
            //ScannableDiscovered();
            //vm.DimmValue = 0.2;
            //QR.IsVisible = true;
            //await Task.Delay(2000);
            //AnalyzingScannable();
            //await Task.Delay(2000);
            //ScanResultReady(result:Scanner.Result?.Text ?? "1234-567-89");
            //await Task.Delay(2000);   
        }

        //simulate Saving data
        async void SimulateSavingScanresult()
        {
            //await Task.Delay(4000);
            //Saved();

            //await Task.Delay(1000);
            readyToScan();
        }
        #endregion

    }
}
