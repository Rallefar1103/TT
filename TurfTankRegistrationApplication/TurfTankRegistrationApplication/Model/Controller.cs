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
        public string ControllerQRSticker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string OCRSticker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ActiveSSID { get; set; }
        public string ActivePassword { get; set; }

        public void SetupWifi()
        {
            // Connect to Robot SSID
        }

        public override string GetSerialNumber()
        {
            if (this.ID != null || this.ID.Length != 0)
            {
                return this.ID;

            }
            else
            {
                SetupWifi();
                this.ID = "ControllerSerialNumber";
                return this.ID;
            }
        }
    }
}
