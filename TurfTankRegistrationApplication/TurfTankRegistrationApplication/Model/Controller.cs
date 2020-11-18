using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface IController
    {
        ControllerQRSticker QR { get; set; }
        string ActiveSSID { get; set; }
        string ActivePassword { get; set; }
        string EtherMac { get; set; }
        string WifiMac { get; set; }
        void SetupWifi();

    }
    public class Controller : Component, IController
    {
        public string ControllerQRSticker { get; set; }
        public string OCRSticker { get; set; }
        public string ActiveSSID { get; set; }
        public string ActivePassword { get; set; }
        public ControllerQRSticker QR { get; set; }
        public string EtherMac { get; set; }
        public string WifiMac { get; set; }

        public void SetupWifi()
        {
            Console.WriteLine("Connecting to Robot");
        }

        public override string GetSerialNumber()
        {
            if (ID == null)
            {
                SetupWifi();
                ID = "ControllerSerialNumber";
                return ID;
            }
            else
            {
                return ID;
            }

        }

    }
}
