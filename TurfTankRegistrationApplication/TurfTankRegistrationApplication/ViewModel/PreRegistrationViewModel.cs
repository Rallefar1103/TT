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
        /// <summary>
        /// TODO: Should create a QR Sticker when it has been scanned
        /// </summary>
        public string QRScanData { get => qrScanData; set => SetProperty(ref qrScanData, value); }
        private string qrScanData;
        public QRSticker QR { get => qr; set => SetProperty(ref qr, value); }
        private QRSticker qr;
        // TODO: Should create a Barcode when it has been scanned
        public string BarcodeScanData { get => barcodeScanData; set => SetProperty(ref barcodeScanData, value); }
        private string barcodeScanData;
        public BarcodeSticker Barcode { get => barcode; set => SetProperty(ref barcode, value); }
        private BarcodeSticker barcode;

        public Command ChooseToPreregisterRoverCommand { get; }
        public Command ChooseToPreregisterBaseCommand { get; }
        public Command ChooseToPreregisterTabletCommand { get; }
        public Command ScanQRCommand { get; }
        public Command ScanBarcodeCommand { get; }
        public Command ConfirmAssemblyAndLabellingCommand { get; }
        public Command PreregisterComponentCommand { get; }
        public Action<object, string> Callback { get; set; }

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
                execute: () => { ChosenComponent = QRType.Rover; ScanQRCommand.ChangeCanExecute(); ScanBarcodeCommand.ChangeCanExecute(); },
                canExecute: () => true);
            ChooseToPreregisterBaseCommand = new Command(
                execute: () => { ChosenComponent = QRType.Base; ScanQRCommand.ChangeCanExecute(); ScanBarcodeCommand.ChangeCanExecute(); }, 
                canExecute: () => true);
            ChooseToPreregisterTabletCommand = new Command(
                execute: () => { ChosenComponent = QRType.Tablet; ScanQRCommand.ChangeCanExecute(); ScanBarcodeCommand.ChangeCanExecute(); }, 
                canExecute: () => true);
            ScanQRCommand = new Command(
                execute: () => NavigateToScanPage("QR"),
                canExecute: () => ChosenComponent != QRType.NoType);
            ScanBarcodeCommand = new Command(
                execute: () => NavigateToScanPage("Barcode"),
                canExecute: () => ChosenComponent != QRType.NoType && ChosenComponent != QRType.Controller);
            ConfirmAssemblyAndLabellingCommand = new Command(
                execute: () => { QR.ConfirmedLabelled = true; PreregisterComponentCommand.ChangeCanExecute(); },
                canExecute: () => QRScanData != "" && BarcodeScanData != ""); // Should be able to press when all scans are done.
            PreregisterComponentCommand = new Command(execute: () => PreregisterComponent(), canExecute: () => QR.ConfirmedLabelled);
            Callback = new Action<object, string>(OnDataReceived);

            MessagingCenter.Subscribe<ScanPage, string>(this, "Result", Callback);
            this.Navigation = navigation;
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
                await QR.Preregister(sim);
            }
            else
            {
                throw new NotImplementedException("Other Components haven't been made yet for preregistration");
            }
        }

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
                scanPage.QRMustContain = ChosenComponent.ToString("g");
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
            if (data.Contains(ChosenComponent.ToString("g")))
            {
                QRScanData = data;
                QR = new QRSticker(data);
            }
            else if(data != "") // Should have better validation such as data.Length == 10 or something depending on the standards of the Simcard
            {
                BarcodeScanData = "";
                Barcode = new BarcodeSticker(data);
            }
            else
            {
                // await Application.Current.MainPage.DisplayAlert("OBS!", "Scan did not work as expected!", "Ok");
                throw new NotImplementedException("The other QR types isnt implemented yet in PreRegistrationViewModel.OnDataReceived");
            }

            ConfirmAssemblyAndLabellingCommand.ChangeCanExecute();
        }
    }
}
