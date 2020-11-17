using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface IController
    {
        string ControllerQRSticker { get; set; }
        string OCRSticker { get; set; }
        string ActiveSSID { get; set; }
        string ActivePassword { get; set; }
        void SetupWifi();
    }
    public class Controller : Component, IController
    {
        public string ControllerQRSticker { get; set; }
        public string OCRSticker { get; set; }
        public string ActiveSSID { get; set; }
        public string ActivePassword { get; set; }

        public void SetupWifi()
        {
            Console.WriteLine("Connecting to Robot");
            // Connect to Robot SSID
        }

        // Method returns serial number for controller.
        // If the serial number (ID) is null it starts the process of retrieving the serial number
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
