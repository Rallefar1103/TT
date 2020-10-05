using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;

namespace testForms1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BarcodeScanView.IsVisible = true;
            BarcodeScanView.IsScanning = true;
            BindingContext = new DynamicComponentList();
            Title = "Component List";
            
        }

        public void Generate_Barcode(object sender, EventArgs e)
        {
            BarcodeImageView.BarcodeValue = $"HEY TURFTANK {e}";
            BarcodeImageView.IsVisible = true;
        }

        public void Handle_OnScanResult(Result result)
        {
            if (string.IsNullOrWhiteSpace(result.Text)) { return; }

            //BarcodeImageView.IsVisible = !BarcodeImageView.IsVisible;
            Device.BeginInvokeOnMainThread(() =>
            {
                Serial.Text = result.Text;
                Console.WriteLine($"QR-detected: {result.Text}");
            });
            BarcodeScanView.IsScanning = true;
        }
        
    }
}
