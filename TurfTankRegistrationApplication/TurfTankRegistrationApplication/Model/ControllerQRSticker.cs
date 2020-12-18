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

            if (ValidateScannedQR(scannedData, out List<string> validatedResults, out QRType type))
            {
                string id = scannedData;
                string ssid = validatedResults[2];
                string password = validatedResults[3];
                Initialize(type, id, ssid, password);
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
        private  bool ValidateScannedQR(string scanResult, out List<string> results, out QRType outType)
        {
            results = scanResult.Split('|').ToList();

            string type = results[0].ToLower().Trim();
            string GUID = results[1].Trim();
            string ssid = results[2].Trim();
            string password = results[3].Trim();

            if (Enum.TryParse(type, out outType) &&
                IsGUID(GUID) &&
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