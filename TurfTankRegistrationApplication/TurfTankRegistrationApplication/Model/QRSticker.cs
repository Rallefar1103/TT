using System;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TurfTankRegistrationApplication.Model
{
    public interface IQRSticker : IScanableSticker
    {

        string ID { get; set; }
        QRType OfType { get; set; }
        bool ConfirmedLabelled { get; set; }
        Task<bool> Preregister(IValidateable component);
    }

    public enum QRType
    {
        robotpackage,
        rover,
        basestation,
        tablet,
        robot,
        controller, // denne qrsticker er af typen ControllerQRSticker (SSID==null på resten,)
        notype
    }

    public class QRSticker : ScanableSticker, IQRSticker
    {
        #region Public Attributes

        public string ID { get; set; }
        
        public bool ConfirmedLabelled { get; set; }
        public QRType OfType { get; set; }

        #endregion Public Attributes

        #region Constants
        //protected const string strType = "TYPE:";
        //protected const string strQRID = "QRID:";
        #endregion

        #region Constructors

        private void Initialize(QRType type, string id)
        {
            OfType = type;
            ID = id;
            ConfirmedLabelled = false;

        }
        public QRSticker()
        {
            Initialize(type: QRType.notype, id: "");
        }

        public QRSticker(string id, QRType type)
        {
            Initialize(type: type, id: id);
        }

        public QRSticker(string scanResult)
        {
            string id = scanResult;
            
            if (ValidateScannedQR(scanResult, out QRType type) && type != QRType.controller)
            {
                //string id = validatedResults[1];
                Initialize(type, id);
                Console.WriteLine("------------------The QR sticker was successfully scanned------------------");
            }
            else
            {
                throw new ValidationException("The scanned QR code is not in a valid format");
            }
        }
        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Preregisters a given object, creating the object for preregistration and attaching the QR and object to it.
        /// </summary>
        /// <param name="obj">A Controller or Simcard used in the preregistration process</param>
        /// <returns>Confirms that the given object matches the QR code, then saves it and returns true, otherwise false</returns>
        public async Task<bool> Preregister(IValidateable obj)
        {
            if (!ConfirmedLabelled) throw new Exception("The QR code hasn't been confirmed labelled to the component set for preregistration!");

            if (obj is SimCard)
            {
                SimCard sim = obj as SimCard;
                sim.QR = this;
                GPS gps = new GPS(sim);

                gps.ValidateSelf(idRestriction: SerialOrQR.OnlyQRId);
                await GPS.API.Save(gps);
                return true;
            }
            else
            {
                // Should return false when all is implemented
                throw new NotImplementedException("The given component type hasn't been implemented yet in ConfirmAssemblyAndLabelling.");
            }
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// This is a protected method used to validate the strings scanned with the QR scanner.
        /// </summary>
        /// <param name="results"></param>
        /// <param name="outType"></param>
        /// <returns></returns>
        protected bool ValidateScannedQR(string scanResult, out QRType outType)
        {
            List<string> results = scanResult.Split('|').ToList();

            string type = results[0].ToLower();
            string GUID = results[1];

            if (Enum.TryParse(type, out outType) && IsGUID(GUID))
                return true;
            else
                return false;
        }
        protected bool IsGUID(string id)
        {
            if (Regex.IsMatch(id, @"[a-zA-Z0-9-]*$") && id != "" && id != null)
                return true;
            else
                return false;
        }

        #endregion Private Methods

    }
}
