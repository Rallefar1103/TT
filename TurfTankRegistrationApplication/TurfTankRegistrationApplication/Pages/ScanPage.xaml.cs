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
        #region ViewModel
        public ScanViewModel vm;
        #endregion

        #region Public Attributes

        //Hvis denne string er sat så kan QR kun godkendes hvis den indeholder denne string
        public string QRMustContain
        {
            get => _qrMustContain;
            set => _qrMustContain = value;
        }
        private string _qrMustContain = "";

        private string _returntypeKey= "Result";

        public string Separator { get; set; }
        


        #endregion

        #region Private Attributes

        //Create a ZXing Scanner for displaying and analyzins QR-codes
        ZXingScannerView _scanner = new ZXingScannerView();
        bool _manualInput = false;

        ZXingBarcodeImageView _qr = new ZXingBarcodeImageView();

        #endregion

        #region Constructor

        public ScanPage(string returntypeKey, string qrMustContain = "", string separator = "|")
        {
            InitializeComponent();
            _returntypeKey = returntypeKey;
            QRMustContain = qrMustContain;
            Separator = separator;

            //Binding to the viewmodel
            BindingContext = vm = new ScanViewModel();

            ///Initializing the viewmodel

            //set the yellow overlay to invisible
            vm.DimmValue = 0;

            //set the scannerstate 
            vm.ScannerState = ScanViewModel.state.Ready_To_Scan;

            //clean the Result
            vm.ScanResult = " ";


            //QRScanner

            //Callback function for the ZXing view , is called when it has data
            _scanner.OnScanResult += Handle_OnScanResult;

            //Add the QRscanner (ZXing)to the view 
            TheScanner.Content = _scanner;
            _scanner.IsVisible = true;

            //Activating the scan function on the QRScanner
            _scanner.IsScanning = true;

            //View Stuff
            //Show/hide the QR sticker
            QR.IsVisible = true;
            QR.BarcodeValue = "0";

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
            if (_scanner.IsScanning != true)
            {
                _scanner.IsScanning = true;
            }

            //Update ScanPageState
            SetScanPageStateToReadyToScan();
        }


        /// <summary>
        /// Stores the the scanned data in viewmodel.Result
        /// When Button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Save_Scanned_Data(System.Object sender, System.EventArgs e)
        {

            SetScanPageStateToSavingData();
            SetScanPageStateToDataSaved();
        }

        async void SwitchToManual_Clicked(System.Object sender, System.EventArgs e)
        {
            //TODO sammensæt QRMustcontain med manual input

            Console.WriteLine("MANUAL INPUT ACTIVATED");
            _manualInput = !_manualInput;
            ManualInputView.IsVisible = _manualInput;
            TheScanner.IsVisible = !_manualInput;
            Dimmer.IsVisible = !_manualInput;
            vm.ResultIsLocked = _manualInput;
            SwitchToManual.Text = vm.ResultIsLocked == true ? "SWITCH TO SCANNER INPUT" : "MANUAL INPUT";
            //after typing the data call
            ManualLabel.Text = $"Plaese type the {QRMustContain}{Separator}ID";
            vm.ManualInputText = QRMustContain + Separator;
            await Task.Run(() =>
            {
                ManualInputField.Focus();
            });
            SetScanPageStateToScanResultReady(vm.ScanResult);

        }


        #endregion Button Actions

        #region States
        void SetScanPageStateToReadyToScan()
        {
            vm.ScanResult = "";
            QR.IsVisible = false;

            Dimmer.IsVisible = !_manualInput;
            vm.DimmValue = 0;
            vm.ScannerState = ScanViewModel.state.Ready_To_Scan;
        }

        /// <summary>
        /// Should be called when the view should reflect the Analyzing state
        /// </summary>
        void SetScanPageStateToScannableDiscovered()
        {
            vm.ScannerState = ScanViewModel.state.Scannable_discovered;
            Dimmer.IsVisible = !_manualInput; ;
            vm.DimmValue = 0.3;
            vm.DimmColor = Color.Yellow;

            //View  layout stuff
            //Update the scannerstate text in view
        }


        /// <summary>
        /// Should be called when the view should reflect the Analyzing state
        /// </summary>
        void SetScanPageStateToAnalyzingScannable()
        {
            //Update the Viewmodel
            vm.ScannerState = ScanViewModel.state.Analyzing;
            Dimmer.IsVisible = !_manualInput; ;
            vm.DimmValue = 0.5;
            vm.DimmColor = Color.Yellow;

        }

        void SetScanPageStateToResultDoesNotMatchQRMustContainString()
        {
            vm.ScanResult = $"This is not a {QRMustContain} QR-Sticker";
            Dimmer.IsVisible = !_manualInput; ;
            vm.DimmValue = 0.2;
            vm.DimmColor = Color.Red;
            vm.ScannerState = ScanViewModel.state.Ready_To_Scan;

            //Set the view, Deactivate registration button
            QR.IsVisible = false;
            RegisterBtn.BackgroundColor = Color.LightGray;
            RegisterBtn.IsEnabled = false;

        }
        /// <summary>
        /// Should be called when the scannerpage has new Data, the new data (result) is passed
        /// in as a string.
        /// It updates the Viewmodel.
        /// It updates the View accordingly
        /// </summary>
        /// <param name="result"></param>
        void SetScanPageStateToScanResultReady(string result)
        {
            //Update the Viewmodel
            vm.ScanResult = result;
            vm.ScannerState = ScanViewModel.state.ScanResult_Ready;
            Dimmer.IsVisible = !_manualInput; ;
            vm.DimmValue = 0.6;
            vm.DimmColor = Color.YellowGreen;

            //Position the data in view
            //Camera_Data.VerticalOptions = LayoutOptions.Center;
            //Camera_Data.HorizontalOptions = LayoutOptions.Center;

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
        void SetScanPageStateToSavingData()
        {

            //update the viewmodel
            vm.ScannerState = ScanViewModel.state.Saving_Data;
            RegisterBtn.IsEnabled = false;

        }

        /// <summary>
        /// Should becalled when the view must reflect that data has been saved
        /// </summary>
        void SetScanPageStateToDataSaved()
        {

            //update the viewmodel
            vm.ScannerState = ScanViewModel.state.Saved;
            vm.DimmValue = 0;

            //View  layout stuff
            //Position the data in view
            Camera_Data.VerticalOptions = LayoutOptions.Start;

            //Dis-activate the Registration Button
            RegisterBtn.BackgroundColor = Color.LightGray;
            RegisterBtn.IsEnabled = false;

            SendResultBack();
            Navigation.PopAsync();
        }

        #endregion States



        #region The primary functions!!
        /// <summary>
        /// This function is called from the QR-Scanner when it
        /// has analyzed a QR-code and has data.
        /// /// </summary>
        /// <param name="ZXingresult"></param>
        public void Handle_OnScanResult(Result ZXingresult)
        {
            if (!_manualInput)
            {
                if (string.IsNullOrWhiteSpace(ZXingresult.Text))
                {
                    if (vm.ScannerState == ScanViewModel.state.Saved)
                        SetScanPageStateToReadyToScan();
                    return;
                }

                Device.BeginInvokeOnMainThread((Action)(() =>
                {
                    string zxinResultText = ZXingresult.Text;

                    Handle_OnScanResult(zxinResultText);
                }));
            }

        }

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
        /// <param name="ZXingresult"></param>
        public void Handle_OnScanResult(string zxinResultText)
        {

            if (string.IsNullOrWhiteSpace(zxinResultText))
            {
                if (vm.ScannerState == ScanViewModel.state.Saved)
                    SetScanPageStateToReadyToScan();
                return;
            }

            //Device.BeginInvokeOnMainThread((Action)(() =>
            //{
            if (QRMustContain != null && !zxinResultText.Contains(QRMustContain))
            {
                SetScanPageStateToResultDoesNotMatchQRMustContainString();
                return;
            }
            // else QRMustcontain == null || it is contained in the scanned string
            //Only update the vm.Result if the data has changed
            else if (vm.ScanResult != zxinResultText)
            {
                SetScanPageStateToScanResultReady(zxinResultText);
            }

            //}));

            //make sure the scanner is still activated and scanning.
            _scanner.IsScanning = true;
        }

        /// <summary>
        /// Returns the result via MessagingCenter on the key "Result"
        /// </summary>
        void SendResultBack()
        {
            
            _manualInput = false;
            MessagingCenter.Send(this, _returntypeKey, vm.ScanResult);
        }

        void ManualInputField_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            Handle_OnScanResult(e.ToString());
            //vm.ScanResult = e.ToString();
        }

        #endregion 


    }
}
