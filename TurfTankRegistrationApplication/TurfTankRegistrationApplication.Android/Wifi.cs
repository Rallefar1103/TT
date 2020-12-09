using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android;
using Android.Content;
using Android.Net.Wifi;
using Android.Support.V4.App;
using TurfTankRegistrationApplication;
using TurfTankRegistrationApplication.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(Wifi))]
namespace TurfTankRegistrationApplication.Droid
{
    public class Wifi : IWifiConnector
    {
        public List<string> availableNetworks { get; set; }
        public WifiManager wifiMgr { get; set; }
        

        private Context context = null;

        public Wifi()
        {
            this.context = Android.App.Application.Context;
            this.availableNetworks = new List<string>();
        }




        /// <summary>
        /// Method takes in the desired SSID you'd want to connect to. Creates a WifiManager, adds a new wifi configuration
        /// to that manager, adds it to the "pool" of associated networks.It then locates the desired network in the "pool"
        ///  and reconnects to that network.
        /// </summary>
        /// <param name="ssid"></param>
        public void ConnectToWifi(string ssid)
        {
            string password = "IMIO64eb";
            var formattedSSID = $"\"{ssid}\"";
            var formattedPassword = $"\"{password}\"";

            wifiMgr = (WifiManager)(context.GetSystemService(Context.WifiService));

            var wifiConfig = new WifiConfiguration
            {
                Ssid = formattedSSID,
                PreSharedKey = formattedPassword,

            };

            wifiMgr.AddNetwork(wifiConfig);
            IList<WifiConfiguration> myWifi = wifiMgr.ConfiguredNetworks;

            wifiMgr.Disconnect();
            wifiMgr.EnableNetwork(myWifi.FirstOrDefault(x => x.Ssid.Contains(ssid)).NetworkId, true);

            Console.WriteLine($"!!!!!! -----------------------   Connecting to {ssid} ------------------------- !!!!!!!!!");

            wifiMgr.Reconnect();
           
        }

        public bool CheckWifiStatus()
        {
            WifiManager wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));
            if (wifiManager.ConnectionInfo.SupplicantState == SupplicantState.Completed)
            {
                return true;

            } else
            {
                return false;
            }
        }

        public string GetSSID()
        {
            WifiManager wifiManager = (WifiManager)(Android.App.Application.Context.GetSystemService(Context.WifiService));

            if (wifiManager != null && !string.IsNullOrEmpty(wifiManager.ConnectionInfo.SSID))
            {
                return wifiManager.ConnectionInfo.SSID;
            }
            else
            {
                return "WiFiManager is NULL";
            }
        }

        /// <summary>
        /// Method creates a WifiManager and a WifiReceiver. The WifiReceiver handles the retrieval of nearby networks.
        /// We register the receiver with the WifiReceiver as well as the ScanResultsAvailableAction.
        /// Lastly we return the located wifi networks.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public async Task<List<string>> GetAvailableNetworks()
        {

            var wifiMgr = (WifiManager)context.GetSystemService(Context.WifiService);
            var wifiReceiver = new WifiReceiver(wifiMgr);

            await Task.Run(() =>
            {
                context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
                availableNetworks = wifiReceiver.Scan();
            });
                
            return availableNetworks;
        }


        
    }

}


