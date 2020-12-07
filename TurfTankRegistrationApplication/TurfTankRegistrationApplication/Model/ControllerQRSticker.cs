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

        private const string strSSID = "SSID:";
        private const string strPassword = "PASSWORD:";


        

        

        #region Constructors
        private void Initialize(QRType type, string id, string ssid, string password)
        {
            OfType = type;
            ID = id;
            FinalSSID = ssid;
            FinalPASSWORD = password;
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

            if (ValidateScannedQR(scannedData, out List<string> validatedResults, out QRType type))
            {
                string id = validatedResults[1];
                string ssid = validatedResults[2];
                string password = validatedResults[3];
                Initialize(type, id, ssid, password);
                Console.WriteLine("------------------The QR sticker was successfully scanned------------------");
            }
            else
            {
                throw new ValidationException("The scanned QR code is not in a valid format");
            }
        }

        #endregion Constructors

        #region Private methods
        /// <summary>
        /// This is a protected method used to validate the strings scanned with the QR scanner.
        /// </summary>
        /// <param name="results"></param>
        /// <param name="outType"></param>
        /// <returns></returns>
        protected override bool ValidateScannedQR(string scanResult, out List<string> results, out QRType outType)
        {
            outType = QRType.NOTYPE;

            results = scanResult.Split(' ').ToList();

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
                results[1] = qrId;

                ssid = Regex.Replace(results[2], strSSID, "");
                results[2] = ssid;

                password = Regex.Replace(results[3], strPassword, "");
                results[3] = password;
            }
            else
            {
                return false;
            }

            if (Enum.TryParse(type, out outType) &&
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
        #endregion Private methods

    }

}