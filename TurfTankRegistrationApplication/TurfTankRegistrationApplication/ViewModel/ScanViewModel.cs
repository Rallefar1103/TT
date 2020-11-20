using System;
using System.Windows.Input;
using TurfTankRegistrationApplication.Pages;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace TurfTankRegistrationApplication.ViewModel
{
    public class ScanViewModel : BaseViewModel
    {

        public enum state
        {
            No_Scanable_Recognized,
            Scannable_discovered,
            Analyzing,
            ScanResult_Ready,
            Saving_Data,
            Saved,
            mere
        }
        public bool ResultIsLocked = false;

        public string Result
        {
            get => _result;
            set
            {
                if (!ResultIsLocked)
                {
                    _result = value;
                    OnPropertyChanged(nameof(Result));
                }
            }
        }
        private string _result = "";


        public string ScannerStateString { get => $"{ScannerState}"; }
        public state ScannerState
        {
            get => _scannerState;
            set
            {
                _scannerState = value;
                OnPropertyChanged(nameof(ScannerState));
            }
        }
        private state _scannerState = state.No_Scanable_Recognized;


        public string Greeting
        {
            get => _greeting;
            set
            {
                _greeting = value;
                OnPropertyChanged(nameof(Greeting));

            }
        }
        private string _greeting = "Ready to Scan";

        public double DimmValue
        {
            get => _dimmValue;
            set
            {
                _dimmValue = value;
                OnPropertyChanged(nameof(DimmValue));
            }
        }

        private double _dimmValue = 0.0;

        #region Constructor
        public ScanViewModel()
        {
            //ScannerState = state.No_Scanable_Recognized;
            ////recieveScan = new Command(doSomething);
            Title = "Scanning ";
            //ScanData = "XXX-XX-XXXX";


        }




        #endregion

        #region Commands

        //public ICommand recieveScan { get; }

        #endregion

        //void doSomething()
        //{
        //    Result = "111,22,3333";
        //}
        //void aButton_Clicked(System.Object sender, System.EventArgs e)
        //{

        //    Console.WriteLine("Hej jax fra VM");
        //}
    }
}
