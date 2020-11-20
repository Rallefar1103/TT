using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.Content;
using Android.Net.Wifi;
using TurfTankRegistrationApplication;
using TurfTankRegistrationApplication.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Wifi))]
namespace TurfTankRegistrationApplication.Droid
{
    public class Wifi : IWifiConnector
    {
        public string SSID = "Interwebs";
        public string PASS = "blaapostkasse";

        public Wifi()
        {
        }

        public void ConnectToWifi()
        {
            var formattedSSID = $"\"{SSID}\"";
            var formattedPassword = $"\"{PASS}\"";

            WifiManager wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));

            var wifiConfig = new WifiConfiguration
            {
                Ssid = formattedSSID,
                PreSharedKey = formattedPassword,

            };

            Console.WriteLine("!!!!!! ------ WifiConfig: " + wifiConfig.ToString());

            wifiManager.AddNetwork(wifiConfig);
            IList<WifiConfiguration> myWifi = wifiManager.ConfiguredNetworks;
            //myWifi.FirstOrDefault(n => n.Ssid == SSID);
            //int networkID = wifiConfig.NetworkId;

            wifiManager.Disconnect();
            wifiManager.EnableNetwork(myWifi.FirstOrDefault(x => x.Ssid.Contains(SSID)).NetworkId, true);
            Console.WriteLine($"!!!!!! -----------------------   Connecting to {SSID} ------------------------- !!!!!!!!!");
            wifiManager.Reconnect();
                
        }
    }
}
