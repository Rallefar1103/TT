using System;
using System.Threading.Tasks;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{
    public interface IQRSticker : IScanableSticker
    {

        string ID { get; set; }
        QRType ofType { get; set; }
        bool ConfirmedLabelled { get; set; }
        Task<bool> Preregister(IValidateable component);
    }

    public enum QRType
    {
        Rover,
        Base,
        Tablet,
        RobotPackage,
        Controller, // denne qrsticker er af typen ControllerQRSticker (SSID==null på resten,)
        NoType
    }

    public class QRSticker : ScanableSticker, IQRSticker
    {
        #region Public Attributes

        public string ID { get; set; }

        public bool ConfirmedLabelled { get; set; }
        public QRType ofType { get; set; }

        #endregion Public Attributes

        #region Constructors

        public void Initialize(QRType type, string id)
        {
            ofType = type;
            ID = id;
            ConfirmedLabelled = false;

        }
        public QRSticker()
        {
            Initialize(type: QRType.NoType, id: "");
        }
        public QRSticker(string scannedData) //WRONG METHOD! Just used to make the PreRegistration not fail. Marco is working on making it correct :)
        {
            Initialize(type: QRType.NoType, id: scannedData);
        }

        public QRSticker(string id, QRType type)
        {
            Initialize(type: type, id: id);
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


        #endregion Private Methods

    }
}
