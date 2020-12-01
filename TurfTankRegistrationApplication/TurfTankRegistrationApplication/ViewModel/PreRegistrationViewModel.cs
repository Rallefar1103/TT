using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Pages;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class PreRegistrationViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public QRType ChosenComponent { get => chosenComponent; set { SetProperty(ref chosenComponent, value); ChosenComponentString = chosenComponent.ToString("g"); } }
        private QRType chosenComponent;
        public string ChosenComponentString { get => chosenComponentString; set => SetProperty(ref chosenComponentString, value); }
        private string chosenComponentString;
        public string QRScanData { get => qrScanData; set => SetProperty(ref qrScanData, value); }
        private string qrScanData;
        public IQRSticker QR { get => qr; set => SetProperty(ref qr, value); }
        private IQRSticker qr;
        public string BarcodeScanData { get => barcodeScanData; set => SetProperty(ref barcodeScanData, value); }
        private string barcodeScanData;
        public IBarcodeSticker Barcode { get => barcode; set => SetProperty(ref barcode, value); }
        private IBarcodeSticker barcode;

        public Command ChooseToPreregisterRoverCommand { get; }
        public Command ChooseToPreregisterBaseCommand { get; }
        public Command ChooseToPreregisterTabletCommand { get; }
        public Command ScanQRCommand { get; }
        public Command ScanBarcodeCommand { get; }
        public Command ConfirmAssemblyAndLabellingCommand { get; }
        public Command PreregisterComponentCommand { get; }
        public Action<object, string> Callback { get; set; }

        public Color ChooseRoverColor { get => _chooseRoverColor; set => SetProperty(ref _chooseRoverColor, value); }
        private Color _chooseRoverColor;
        public Color ChooseBaseColor { get => _chooseBaseColor; set => SetProperty(ref _chooseBaseColor, value); }
        private Color _chooseBaseColor;
        public Color ChooseTabletColor { get => _chooseTabletColor; set => SetProperty(ref _chooseTabletColor, value); }
        private Color _chooseTabletColor;
        public Color ScanQRColor { get => _scanQRColor; set => SetProperty(ref _scanQRColor, value); }
        private Color _scanQRColor;
        public Color ScanBarcodeColor { get => _scanBarcodeColor; set => SetProperty(ref _scanBarcodeColor, value); }
        private Color _scanBarcodeColor;
        public Color ConfirmLabellingColor { get => _confirmLabellingColor; set => SetProperty(ref _confirmLabellingColor, value); }
        private Color _confirmLabellingColor;

        private Color _neutralColor = Color.White;
        private Color _successOrChooseColor = Color.Green;
        private Color _failureOrNotChooseColor = Color.Red;

        public string UserMessage { get => _userMessage; set => SetProperty(ref _userMessage, value); }
        private string _userMessage;

        public PreRegistrationViewModel(INavigation navigation)
        {
            ChosenComponent = QRType.NoType;
            QR = new QRSticker();
            QRScanData = "";
            Barcode = new BarcodeSticker();
            BarcodeScanData = "";

            // The following commands follow a linear flow, which is that first you choose a component, then you scan, then you confirm, then you save.
            // ChangeCanScan is just a way to force the given command to reevaluate and rerender the options to the view. There is probably a smarter way, but this works.
            ChooseToPreregisterRoverCommand = new Command(
                execute: () => ChooseComponent(QRType.Rover),
                canExecute: () => true);
            ChooseToPreregisterBaseCommand = new Command(
                execute: () => ChooseComponent(QRType.Base),
                canExecute: () => true);
            ChooseToPreregisterTabletCommand = new Command(
                execute: () => ChooseComponent(QRType.Tablet),
                canExecute: () => true);
            ScanQRCommand = new Command(
                execute: () => NavigateToScanPage("QR"),
                canExecute: () => ChosenComponent != QRType.NoType);
            ScanBarcodeCommand = new Command(
                execute: () => NavigateToScanPage("Barcode"),
                canExecute: () => ChosenComponent != QRType.NoType && ChosenComponent != QRType.Controller);
            ConfirmAssemblyAndLabellingCommand = new Command(
                execute: () => ConfirmAssemblyAndLabelling(),
                canExecute: () => QRScanData != "" && BarcodeScanData != ""); // Should be able to press when all scans are done.
            PreregisterComponentCommand = new Command(execute: () => PreregisterComponent(), canExecute: () => QR.ConfirmedLabelled);
            Callback = new Action<object, string>(OnDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);
            this.Navigation = navigation;

            // Pure Graphics
            ChooseRoverColor = _neutralColor;
            ChooseBaseColor = _neutralColor;
            ChooseTabletColor = _neutralColor;
            ScanQRColor = _neutralColor;
            ScanBarcodeColor = _neutralColor;
            ConfirmLabellingColor = _neutralColor;
        }

        /// <summary>
        /// The QR code creates and Preregisters the chosen component.
        /// The Preregistration makes sure all validation is taken care of.
        /// </summary>
        public async void PreregisterComponent()
        {
            if(ChosenComponent == QRType.Rover || ChosenComponent == QRType.Base)
            {
                SimCard sim = new SimCard(Barcode);
                //await QR.Preregister(sim); TODO: This has been outcommented due to making a working ProtoType
                await Navigation.PopAsync();
            }
            else
            {
                throw new NotImplementedException("Other Components haven't been made yet for preregistration");
            }
        }

        #region Scanner
        /// <summary>
        /// Goes to the scan page, and makes sure that the scanned item contains the chosen component.
        /// </summary>
        /// <param name="scanChoice">The choice of scan. Just to verify to the user that they indeed chose QR or Barcode</param>
        public void NavigateToScanPage(string scanChoice)
        {
            ScanPage scanPage = new ScanPage();
            scanPage.vm.Title = "Scanning " + scanChoice;
            if(scanChoice == "QR")
            {
                scanPage.QRMustContain = ChosenComponentString;
            }
            Navigation.PushAsync(scanPage);
        }

        /// <summary>
        /// The method being called from the Callback which uses the scan function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data">The data received from the ScanPage</param>
        private async void OnDataReceived(object sender, string data)
        {
            try
            {
                if (data.Contains(ChosenComponentString))
                {
                    QRScanData = data;
                    if (data.Contains("Controller"))
                    {
                        QR = new ControllerQRSticker(data);
                    }
                    else
                    {
                        QR = new QRSticker(data);
                    }
                    ScanQRColor = _successOrChooseColor;
                }
                else if (data != "")
                {
                    BarcodeScanData = data;
                    Barcode = new BarcodeSticker(data);
                    ScanBarcodeColor = _successOrChooseColor;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("OBS!", "Scan did not work as expected!", "Ok");
                }
                ConfirmAssemblyAndLabellingCommand.ChangeCanExecute();
            }
            catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("OBS!", e.Message, "Ok");
            }
        }
        #endregion Scanner

        /// <summary>
        /// Sets the component as the one currently chosen, and updates the View
        /// </summary>
        /// <param name="chosen"></param>
        private void ChooseComponent(QRType chosen)
        {
            ChosenComponent = chosen;

            ChooseRoverColor = chosen == QRType.Rover ? _successOrChooseColor : _failureOrNotChooseColor;
            ChooseBaseColor = chosen == QRType.Base ? _successOrChooseColor : _failureOrNotChooseColor;
            ChooseTabletColor = chosen == QRType.Tablet ? _successOrChooseColor : _failureOrNotChooseColor;

            ScanQRCommand.ChangeCanExecute(); 
            ScanBarcodeCommand.ChangeCanExecute();
        }

        private void ConfirmAssemblyAndLabelling()
        {
            QR.ConfirmedLabelled = true;
            ConfirmLabellingColor = _successOrChooseColor;

            PreregisterComponentCommand.ChangeCanExecute();
        }
    }
}
