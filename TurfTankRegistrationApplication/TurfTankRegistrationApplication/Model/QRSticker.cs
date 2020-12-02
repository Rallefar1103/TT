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
        ROVER,
        BASE,
        TABLET,
        ROBOTPACKAGE,
        CONTROLLER, // denne qrsticker er af typen ControllerQRSticker (SSID==null på resten,)
        NOTYPE
    }

    public class QRSticker : ScanableSticker, IQRSticker
    {
        #region Public Attributes

        public string ID { get; set; }

        public bool ConfirmedLabelled { get; set; }
        public QRType OfType { get; set; }

        #endregion Public Attributes

        #region Constants
        protected const string strType = "TYPE:";
        protected const string strQRID = "QRID:";
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
            Initialize(type: QRType.NOTYPE, id: "");
        }

        public QRSticker(string id, QRType type)
        {
            Initialize(type: type, id: id);
        }

        public QRSticker(string scanResult)
        {
            scanResult = scanResult.ToUpper();
            
            if (ValidateScannedQR(scanResult, out List<string>validatedResults, out QRType type) && type != QRType.CONTROLLER)
            {
                string id = validatedResults[1];
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
        protected virtual bool ValidateScannedQR(string scanResult, out List<string> results, out QRType outType)
        {
            results = scanResult.Split(' ').ToList();

            outType = QRType.NOTYPE;

            string type;
            string qrId;

            if (results[0].Contains(strType) &&
                results[1].Contains(strQRID))
            {
                type = Regex.Replace(results[0], strType, "");

                qrId = Regex.Replace(results[1], strQRID, "");
                results[1] = qrId;
            }
            else
                return false;

            if (Enum.TryParse(type, out outType) && IsID(qrId))
                return true;
            else
                return false;
        }
        protected bool IsID(string id)
        {
            if (Regex.IsMatch(id, @"^[0-9]*$") && id != "" && id != null)
                return true;
            else
                return false;
        }

        #endregion Private Methods

    }
}
