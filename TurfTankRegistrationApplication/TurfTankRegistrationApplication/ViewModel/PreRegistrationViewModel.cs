using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;

using TurfTankRegistrationApplication.Model;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class PreRegistrationViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }

        public QRType chosenComponent { get; set; }
        /// <summary>
        /// TODO: Should create a QR Sticker when it has been scanned
        /// </summary>
        public string QRScanData { get; set; }
        public QRSticker qr { get; set; }
        // TODO: Should create a Barcode when it has been scanned
        public string BarcodeScanData { get; set; }
        public BarcodeSticker Barcode { get; set; }

        public Command ChooseToPreregisterRoverCommand { get; set; }
        public Command ChooseToPreregisterBaseCommand { get; set; }
        public Command ChooseToPreregisterTabletCommand { get; set; }
        public Command ScanQRCommand { get; }
        public Command ScanBarcodeCommand { get; }
        public Command ConfirmAssemblyAndLabellingCommand { get; }
        public Command PreregisterComponentCommand { get; }

        public string UserMessage { get => _userMessage; set => SetProperty(ref _userMessage, value); }
        private string _userMessage;

        public PreRegistrationViewModel(INavigation navigation)
        {
            qr = new QRSticker();
            Barcode = new BarcodeSticker();

            ChooseToPreregisterRoverCommand = new Command(execute: () => chosenComponent = QRType.Rover, canExecute: () => true);
            ChooseToPreregisterBaseCommand = new Command(execute: () => chosenComponent = QRType.Base, canExecute: () => true);
            ChooseToPreregisterTabletCommand = new Command(execute: () => chosenComponent = QRType.Tablet, canExecute: () => true);
            ScanQRCommand = new Command(execute: () => Console.WriteLine("scanQR"), canExecute: () => true);
            ScanBarcodeCommand = new Command(execute: () => Console.WriteLine("scanBarcode"), canExecute: () => chosenComponent != QRType.Controller);
            ConfirmAssemblyAndLabellingCommand = new Command(execute: () => qr.ConfirmedLabelled=true, canExecute: () => true);
            PreregisterComponentCommand = new Command(execute: () => PreregisterComponent(),canExecute: () => qr.ConfirmedLabelled);
            this.Navigation = navigation;
        }

        async void PreregisterComponent()
        {
            if(chosenComponent == QRType.Rover || chosenComponent == QRType.Base)
            {
                SimCard sim = new SimCard(Barcode);
                await qr.Preregister(sim);
            }
            else
            {
                throw new NotImplementedException("Other Components haven't been made yet for preregistration");
            }
        }
    }
}
