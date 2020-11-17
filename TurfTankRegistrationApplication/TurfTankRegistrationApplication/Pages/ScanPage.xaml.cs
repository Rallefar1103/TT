using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;
using TurfTankRegistrationApplication.ViewModel;
using System.Threading.Tasks;
using ZXing.Net.Mobile.Forms;

/// <summary>
/// når en QR er genkendt skal der ske noget
/// når en qr bliver analyseret skal der ske noget visuelt
/// når qr data er tilgængeligt
/// når qr data gemmes-evt catch error
/// når qr er gemt
/// når scanner er klar til et nyt scan
/// </summary>
namespace TurfTankRegistrationApplication.Pages

{
    /// <summary>
    /// This page implements a ZXing scanner that recognizes
    /// Barcodes and QR codes.
    /// The scanned Data can be represented in the QR label.
    /// When the "use scanned data" button is pressed It can navigate to the view
    /// it was called from, and pass in the scanned data as a string.
    /// </summary>
    public partial class ScanPage : ContentPage
    {
        ScanViewModel vm;
        public RobotRegistrationViewModel RobotVM = new RobotRegistrationViewModel();

        //Create a ZXing Scanner for displaying and analyzins QR-codes
        ZXingScannerView Scanner = new ZXingScannerView();

        //Represents the possible states of the view
        enum State
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
            //Binding to the viewmodel
            BindingContext = vm = new ScanViewModel(Navigation);


            //Initializing the viewmodel

            //set the yellow overlay to invisible
            vm.DimmValue = 0;

            //set the scannerstate 
            vm.ScannerState = ScanViewModel.state.No_Scanable_Recognized;

            //clean the Result
            vm.Result = " ";


            //QRScanner

            //Callback function for the ZXing view , is called when it has data
            Scanner.OnScanResult += Handle_OnScanResult;

            //Add the QRscanner (ZXing)to the view 
            TheScanner.Content = Scanner;
            Scanner.IsVisible = true;

            //Activating the scan function on the QRScanner
            Scanner.IsScanning = true;

            //View Stuff
            //Show/hide the QR sticker
            //QR.IsVisible = true;

        }


        #endregion Constructor

        #region Button Actions
        /// <summary>
        /// Turn on the scanner function, and reset Result in viewmodel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void StartScanner(System.Object sender, System.EventArgs e)
        {
            //Toggle scanner 
            if (Scanner.IsScanning != true)
            {
                Scanner.IsScanning = true;

            }
            //Update viewmodel
            vm.Result = " ";
            vm.ScannerState = ScanViewModel.state.No_Scanable_Recognized;

            //Update view
            scanner_State.Text = vm.ScannerStateString;
        }


        /// <summary>
        /// Stores the the scanned data in viewmodel.Result
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Save_Scanned_Data(System.Object sender, System.EventArgs e)
        {
            RegisterBtn.IsEnabled = false;
            SavingData();
            Saved();
        }

        void SwitchToManual_Clicked(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("MANUAL INPUT ACTIVATED");
            vm.Result = "Robot;THIS IS THE MANUAL INPUT";
            vm.ResultIsLocked = !vm.ResultIsLocked;
            SwitchToManual.Text = vm.ResultIsLocked == true ? "SWITCH TO SCANNER INPUT" : "MANUAL INPUT";
            //after typing the data call
            ScanResultReady(vm.Result);
            //SavingData();
            //Saved();
        }


        #endregion Button Actions

        #region States
        void readyToScan()
        {
            QR.IsVisible = false;
            vm.DimmValue = 0;
            vm.ScannerState = ScanViewModel.state.No_Scanable_Recognized;
            scanner_State.Text = vm.ScannerStateString;// nameof(state.No_Scanable_Recognized);
        }
        /// <summary>
        /// Should be called when the view should reflect the Analyzing state
        /// </summary>
        void ScannableDiscovered()
        {
            vm.ScannerState = ScanViewModel.state.Scannable_discovered;
            vm.DimmValue = 0.3;

            //View  layout stuff
            //Update the scannerstate text in view
            scanner_State.Text = vm.ScannerStateString;
        }


        /// <summary>
        /// Should be called when the view should reflect the Analyzing state
        /// </summary>
        void AnalyzingScannable()
        {
            //Update the Viewmodel
            vm.ScannerState = ScanViewModel.state.Analyzing;
            vm.DimmValue = 0.5;

            //View  layout stuff
            //Update the scannerstate text in view
            scanner_State.Text = vm.ScannerStateString;
        }

        /// <summary>
        /// Should be called when the scannerpage has new Data, the new data (result) is passed
        /// in as a string.
        /// It updates the Viewmodel.
        /// It updates the View accordingly
        /// </summary>
        /// <param name="result"></param>
        void ScanResultReady(string result)
        {
            //Update the Viewmodel
            vm.Result = result;
            vm.ScannerState = ScanViewModel.state.ScanResult_Ready;
            vm.DimmValue = 0.6;

            //View  layout stuff
            //Update the scannerstate text in view
            scanner_State.Text = vm.ScannerStateString;


            //Position the data in view
            //Camera_Data.VerticalOptions = LayoutOptions.Center;
            //Camera_Data.HorizontalOptions = LayoutOptions.Center;

            //Show/hide the QR code
            QR.IsVisible = true;

            //set the text color of the scanned data
            Camera_Data.TextColor = Color.OrangeRed;

            //Activate the Registration Button
            RegisterBtn.BackgroundColor = Color.GreenYellow;
            RegisterBtn.IsVisible = true;
            RegisterBtn.IsEnabled = true;

        }

        /// <summary>
        /// Should be called when the view must reflect the savingprocess
        /// </summary>
        void SavingData()
        {
            //update the viewmodel
            vm.ScannerState = ScanViewModel.state.Saving_Data;

            //View  layout stuff
            //Update the scannerstate text in view
            scanner_State.Text = vm.ScannerStateString;
        }

        /// <summary>
        /// Should becalled when the view must reflect that data has been saved
        /// </summary>
        void Saved()
        {

            //update the viewmodel
            vm.ScannerState = ScanViewModel.state.Saved;
            vm.DimmValue = 0;

            //View  layout stuff
            //Update the scannerstate text in view
            scanner_State.Text = vm.ScannerStateString;

            //Position the data in view
            Camera_Data.VerticalOptions = LayoutOptions.Start;

            //Dis-activate the Registration Button
            RegisterBtn.BackgroundColor = Color.LightGray;
            RegisterBtn.IsEnabled = false;
            

        }

        #endregion States


        #region The primary function!!

        /// <summary>
        /// This function is called from the QR-Scanner when it
        /// has analyzed a QR-code and has data.
        /// 
        /// The Purpose is to activate the ScanResultReady state of the page
        /// if and only if there is new acceptable data in Result.
        ///
        /// the Result type is a ZXing Type, that contains the QR-Data as
        /// a string: Result.Text
        /// </summary>
        /// <param name="result"></param>
        public void Handle_OnScanResult(Result result)
        {
            if (string.IsNullOrWhiteSpace(result.Text)) { return; }


            Device.BeginInvokeOnMainThread((Action)(() =>
            {
                //Only update the vm.Result if the data has changed
                if (vm.Result != result.Text)
                {
                    ScanResultReady(result.Text);
                }

                Console.WriteLine($"QR-detected: {result.Text}");
            }));
            Scanner.IsScanning = true;
        }
        #endregion The primary function!!

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


        #endregion simulations

    }
}
