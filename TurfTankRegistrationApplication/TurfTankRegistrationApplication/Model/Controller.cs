using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

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
    public class Controller : Component, IController, IValidateable
    {
        public string SerialNumber { get; set; }
        public string ActiveSSID { get; set; }
        public string ActivePassword { get; set; }
        public ControllerQRSticker QR { get; set; }
        public string EtherMac { get; set; }
        public string WifiMac { get; set; }

        public static IRegistrationDBAPI<Controller> API { get; set; }

        public void Initialize(string serial, string ssid, string pw, ControllerQRSticker qr, string ether, string wifi, RegistrationDBAPI<Controller> api)
        {
            SerialNumber = serial;
            ActiveSSID = ssid;
            ActivePassword = pw;
            QR = qr;
            EtherMac = ether;
            WifiMac = wifi;
            API = api;
        }
        public Controller()
        {
            Initialize(
                serial: $"",
                ssid: $"",
                pw: $"",
                qr: new ControllerQRSticker(),
                ether: $"",
                wifi: $"",
                api: new RegistrationDBAPI<Controller>()
            );
        }
        public Controller(RobotController schema)
        {
            Initialize(
                serial: $"{schema.SerialNumber}",
                ssid: $"{schema.Ssid}",
                pw: $"{schema.SsidPassword}",
                qr: new ControllerQRSticker(),
                ether: $"{schema.MacEth}",
                wifi: $"{schema.MacWifi}",
                api: new RegistrationDBAPI<Controller>()
            );
        }

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

        public override void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.AnyId)
        {
            throw new NotImplementedException();
        }
    }
}