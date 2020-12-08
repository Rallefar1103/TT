using System;
using System.Collections.Generic;
using Android.Content;
using Android.Net.Wifi;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Droid
{
    class WifiReceiver : BroadcastReceiver
    {
        public WifiManager wifiManager;
        public IList<ScanResult> results { get; set; }
        public List<string> wifis { get; set; }

        public WifiReceiver(WifiManager wifiMg)
        {
            wifiManager = wifiMg;
            wifis = new List<string>();
        }

        [Obsolete]
        public List<string> Scan()
        {
            wifiManager.StartScan();
            return wifis;
        }

        // Standardized onReceive method for the BroadcastReceiver class. This methods handles
        // how the scan results are being processed upon discovery.
        // What we do is that we add them to the wifi list and return them through the Scan method.
        public override void OnReceive(Context context, Intent intent)
        {
            IList<ScanResult> scanWifiNetworks = wifiManager.ScanResults;
            foreach (ScanResult wifiNetwork in scanWifiNetworks)
            {
                Console.WriteLine("Found something - " + wifiNetwork.Ssid);
                wifis.Add(wifiNetwork.Ssid);
            }
            

        }
    }
}
