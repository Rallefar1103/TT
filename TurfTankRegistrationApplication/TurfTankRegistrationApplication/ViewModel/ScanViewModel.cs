using System;
using System.Windows.Input;
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
            Ready_To_Scan,
            Scannable_discovered,
            Analyzing,
            ScanResultDoesNotMatchQRMustContainString,
            ScanResult_Ready,
            Saving_Data,
            Saved,
            mere
        }
        public bool ResultIsLocked = false;

        public string ScanResult
        {
            get => _result;
            set
            {
                if (!ResultIsLocked)
                {
                    _result = value;
                    OnPropertyChanged(nameof(ScanResult));
                }
            }
        }
        private string _result = "";


        public string ManualInputText
        {
            get => _manualInputText;
            set
            {
                _result = value;
                _manualInputText = value;
                OnPropertyChanged(nameof(ScanResult));
                OnPropertyChanged(nameof(ManualInputText));
            }
        }
        private string _manualInputText = "";

        public string ScannerStateString { get => $"{ScannerState}"; }
        public state ScannerState
        {
            get => _scannerState;
            set
            {
                _scannerState = value;
                OnPropertyChanged(nameof(ScannerStateString));
            }
        }
        private state _scannerState = state.Ready_To_Scan;


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

        public Color DimmColor
        {
            get => _dimmColor;
            set
            {
                _dimmColor = value;
                OnPropertyChanged(nameof(DimmColor));
            }
        }

        private Color _dimmColor = Color.Yellow;


        #region Constructor
        public ScanViewModel()
        {
            //ScannerState = state.No_Scanable_Recognized;
            ////recieveScan = new Command(doSomething);
            Title = "Scanner";
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
