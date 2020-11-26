using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public List<string> availableNetworks = new List<string>();
        public WifiManager wifiMgr;

        private Context context = null;

        public Wifi()
        {
            this.context = Android.App.Application.Context;
        }

        public bool ConnectToWifi()
        {
            var formattedSSID = $"\"{SSID}\"";
            var formattedPassword = $"\"{PASS}\"";

            wifiMgr = (WifiManager)(context.GetSystemService(Context.WifiService));

            var wifiConfig = new WifiConfiguration
            {
                Ssid = formattedSSID,
                PreSharedKey = formattedPassword,

            };

            wifiMgr.AddNetwork(wifiConfig);
            IList<WifiConfiguration> myWifi = wifiMgr.ConfiguredNetworks;

            wifiMgr.Disconnect();
            wifiMgr.EnableNetwork(myWifi.FirstOrDefault(x => x.Ssid.Contains(SSID)).NetworkId, true);

            Console.WriteLine($"!!!!!! -----------------------   Connecting to {SSID} ------------------------- !!!!!!!!!");

            if (wifiMgr.Reconnect())
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        [Obsolete]
        public void ScanWifi()
        {
            wifiMgr = (WifiManager)(context.GetSystemService(Context.WifiService));
            WifiReceiver wifiReceiver = new WifiReceiver(wifiMgr);
            context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
            wifiMgr.StartScan();

            availableNetworks = wifiReceiver.networks;
        }

        [Obsolete]
        public List<string> GetAvailableNetworks()
        {
            ScanWifi();
            return availableNetworks;
        }

        
    }





    class WifiReceiver : BroadcastReceiver
    {
        private WifiManager wifiManager;
        public List<string> networks = new List<string>();

        public WifiReceiver(WifiManager mgr)
        {
            wifiManager = mgr;
        }

        public override void OnReceive(Context context, Intent intent)
        {
            var results = wifiManager.ScanResults;
            if (results != null)
            {
                foreach (var result in results)
                {
                    networks.Add(result.Ssid);
                }
            } else
            {
                Console.WriteLine("Error in OnReceive");
            }
        }
    }
}




//    [Obsolete]
//    public async Task<IEnumerable<string>> GetAvailableNetworksAsync()
//    {
//        IEnumerable<string> availableNetworks = null;

//        var wifiMgr = (WifiManager)context.GetSystemService(Context.WifiService);
//        var wifiReceiver = new WifiReceiver(wifiMgr);

//        await Task.Run(() =>
//        {
//            context.RegisterReceiver(wifiReceiver, new IntentFilter(WifiManager.ScanResultsAvailableAction));
//            availableNetworks = wifiReceiver.Scan();
//        });

//        return availableNetworks;
//    }

//    class WifiReceiver : BroadcastReceiver
//    {
//        private WifiManager wifi;
//        private List<string> wifiNetworks;
//        private AutoResetEvent receiverARE;
//        private Timer tmr;
//        private const int TIMEOUT_MILLIS = 20000;
//        private readonly Action _callback;

//        public WifiReceiver(WifiManager wifiM)
//        {
//            wifi = wifiM;
//            wifiNetworks = new List<string>();
//            receiverARE = new AutoResetEvent(false);
//        }

//        [Obsolete]
//        public IEnumerable<string> Scan()
//        {
//            tmr = new Timer(Timeout, null, TIMEOUT_MILLIS, System.Threading.Timeout.Infinite);
//            wifi.StartScan();
//            receiverARE.WaitOne();
//            return wifiNetworks;
//        }

//        public override void OnReceive(Context context, Intent intent)
//        {
//            IList<ScanResult> scanWifiNetworks = wifi.ScanResults;
//            foreach (ScanResult wifiNetwork in scanWifiNetworks)
//            {
//                wifiNetworks.Add(wifiNetwork.Ssid);
//            }

//            receiverARE.Set();
//        }

//        private void Timeout(object sender)
//        {
//            receiverARE.Set();
//        }

//    }