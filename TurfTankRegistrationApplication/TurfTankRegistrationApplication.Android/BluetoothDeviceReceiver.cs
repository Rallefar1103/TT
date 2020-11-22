using System;
using Android.Bluetooth;
using Android.Content;

namespace TurfTankRegistrationApplication.Droid
{
    public class BluetoothDeviceReceiver : BroadcastReceiver
    {
        public BluetoothDeviceReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;

            if (action == BluetoothDevice.ActionFound)
            {
                BluetoothDevice newDevice = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);
            }
        }
    }
}
