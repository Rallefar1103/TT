
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TurfTankRegistrationApplication.Views
{
    [Xamarin.Forms.ContentProperty("TheScanner")]
    public partial class Scanner : ContentView
    {
        //[XamlCompilation(XamlCompilationOptions.Compile)]
        public Scanner()
        {
            InitializeComponent();
        }

        public View content
        {
            get { return Content ; }
            set { Content = value; }
        }

        public string ResultString;

        //private void ZXingScannerView_OnScanResult(ZXing.Result result)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        //scanResultText.Text = result.Text + " (type:" + result.BarcodeFormat.ToString() + ")";
        //    });
        //}
    }
}
