using System;

using TurfTankRegistrationApplication.Model;
using System.Linq;
using TurfTankRegistrationApplication.Exceptions;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TurfTankRegistrationApplication.Model
{
    public interface IControllerQRSticker
    {
        string FinalSSID { get; set; }
        string FinalPASSWORD { get; set; }

    }

    public class ControllerQRSticker : QRSticker, IControllerQRSticker
    {
        public string FinalSSID { get; set; }
        public string FinalPASSWORD { get; set; }

        private void Initialize(QRType type, string id, string ssid, string password)
        {
            ofType = type;
            ID = id;

            //Should this be changed?
            FinalSSID = ssid;
            FinalPASSWORD = password;
        }

        protected override bool ValidateScannedQR(List<string> results, out QRType outType)
        {
            outType = QRType.NOTYPE;

            string strType = "TYPE:";
            string strQRID = "QRID:";
            string strSSID = "SSID:";
            string strPassword = "PASSWORD:";

            string type;
            string qrId;
            string ssid;
            string password;

            if (results[0].Contains(strType) &&
                results[1].Contains(strQRID) &&
                results[2].Contains(strSSID) &&
                results[3].Contains(strPassword))
            {
                type = Regex.Replace(results[0], strType, "");
                qrId = Regex.Replace(results[1], strQRID, "");
                ssid = Regex.Replace(results[2], strSSID, "");
                password = Regex.Replace(results[3], strPassword, "");
            }
            else
            {
                return false;
            }

            if  (Enum.TryParse(type, out outType) &&
                IsID(qrId) &&
                IsSSID(ssid) &&
                IsPassword(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsPassword(string password)
        {
            if (Regex.IsMatch(password, "TTIO[1-9]+$"))
                return true;
            else
                return false;
        }

        private bool IsSSID(string ssid)
        {
            if (Regex.IsMatch(ssid, "TTAP[1-9]+$"))
                return true;
            else
                return false;
        }

        public ControllerQRSticker()
        {

        }
        public ControllerQRSticker(QRType type, string id, string ssid, string password)
        {
            Initialize(type, id, ssid, password);
        }
        public ControllerQRSticker(string scannedData)
        {
            scannedData = scannedData.ToUpper();
            List<string> results = scannedData.Split(' ').ToList();

            if (ValidateScannedQR(results, out QRType type))
            {
                string id = results[1];
                string ssid = results[2];
                string password = results[3];
                Initialize(type, id, ssid, password);
                Console.WriteLine("------------------The QR sticker was successfully scanned------------------");
            }
            else
            {
                throw new ValidationException("The scanned QR code is not in a valid format");
            }
        }

        
    }

}