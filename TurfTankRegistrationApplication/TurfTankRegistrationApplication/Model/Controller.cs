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
        #region Public Attributes
        public string SerialNumber { get; set; }
        public string ActiveSSID { get; set; }
        public string ActivePassword { get; set; }
        public ControllerQRSticker QR { get; set; }
        public string EtherMac { get; set; }
        public string WifiMac { get; set; }

        public static IDBAPI<Controller> API { get; set; } = new DBAPI<Controller>();

        #endregion Public Attributes

        private const string initialPassword = "ChangeMe";

        #region Private methods


        //TTAP + Sidste 4 I macWifi 
        private string MacWifiToSSID(string macWifi)
        {
            if (string.IsNullOrEmpty(macWifi))
                return "";

            string ssid = "TTAP";

            int index = macWifi.Length;

            if (macWifi.Length >= 4)
                ssid += macWifi.Substring(index - 4);
            else
                ssid = "";

            return ssid;
        }

        #endregion Private methods

        #region Constructors
        public void Initialize(string serial, string ssid, string pw, ControllerQRSticker qr, string ether, string wifi)
        {
            ID = qr.ID;
            SerialNumber = serial;
            ActiveSSID = ssid;
            ActivePassword = pw;
            QR = qr;
            EtherMac = ether;
            WifiMac = wifi;
        }
        public Controller()
        {
            Initialize(
                serial: $"",
                ssid: $"",
                pw: $"",
                qr: new ControllerQRSticker(),
                ether: $"",
                wifi: $""
            );
        }

        public Controller(ControllerQRSticker qr, string macEthernet, string macWifi)
        {
            Initialize(
                serial: "",
                ssid: MacWifiToSSID(macWifi),
                pw: initialPassword,
                qr: qr,
                ether: macEthernet,
                wifi: macWifi
           );
        }

        public Controller(string serialNumber, ControllerQRSticker qr, string macEthernet, string macWifi)
        {
            Initialize(
                serial: serialNumber,
                ssid: MacWifiToSSID(macWifi),
                pw: initialPassword,
                qr: qr,
                ether: macEthernet,
                wifi: macWifi
           );
        }

        

        public Controller(RobotControllerItemModel schema)
        {
            Initialize(
                serial: $"{schema.SerialNumber}",
                ssid: $"{schema.Ssid}",
                pw: $"{schema.SsidPassword}",
                qr: new ControllerQRSticker(),
                ether: $"{schema.MacEth}",
                wifi: $"{schema.MacWifi}"
            );
        }

        #endregion Constructors

        #region Public methods
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

        public override void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.BothSerialAndQRId)
        {

            if (string.IsNullOrEmpty(QR.ID))
                throw new ValidationException("Controller doesn't have a QR with an ID");
            if (QR.OfType != QRType.controller)
                throw new ValidationException("The QR type is not a Controller");
            if (string.IsNullOrEmpty(ID))
                throw new ValidationException("The controller doesn't have an ID");
            if (QR.ID != ID)
                throw new ValidationException("Controllers ID isn't equal to that of the QR");
            if (string.IsNullOrEmpty(EtherMac))
                throw new ValidationException("Controller does not have ethermac");
            if (string.IsNullOrEmpty(WifiMac))
                throw new ValidationException("Controller does not have wifimac");
            if (string.IsNullOrEmpty(ActiveSSID))
                throw new ValidationException("Controller doesn't have a SSID");
            if (string.IsNullOrEmpty(ActivePassword))
                throw new ValidationException("Controller doesnt have an active password");


            if (idRestriction == SerialOrQR.BothSerialAndQRId)
            {
                if (string.IsNullOrEmpty(SerialNumber)) 
                    throw new ValidationException("Controller doesnt have a serial number");
                if (ActivePassword != QR.FinalPASSWORD)
                    throw new ValidationException("The Controller password is not equal to QR password");
                if (ActiveSSID != QR.FinalSSID)
                    throw new ValidationException("The Controller SSID is not equal to QR SSID");
            }
            else if (idRestriction == SerialOrQR.OnlyQRId)
            {
                if (ActiveSSID != MacWifiToSSID(WifiMac))
                    throw new ValidationException("The controller SSID is the wrong initial SSID");
                if (ActivePassword != initialPassword)
                    throw new ValidationException("The controller password is the wrong initial password");
                if (!string.IsNullOrEmpty(SerialNumber))
                    throw new ValidationException("The controller has a serial number, when i shouldn't");
            }
        }
        #endregion Public methods
    }
}