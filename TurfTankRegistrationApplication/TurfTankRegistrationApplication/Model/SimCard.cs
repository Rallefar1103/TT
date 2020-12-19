using System;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{
    interface ISimCard
    {
        string ID { get; set; }
        QRSticker QR { get; set; }
        IBarcodeSticker Barcode { get; set; }
        bool Activated { get; set; }
    }

    public class SimCard : ISimCard, IValidateable
    {
        #region Public Attributes

        public string ID { get; set; }
        public QRSticker QR { get; set; }
        public IBarcodeSticker Barcode { get; set; }
        public bool Activated { get; set; }

        public static IDBAPI<SimCard> API { get; set; } = new DBAPI<SimCard>();

        #endregion Public Attributes

        #region Constructors
        public void Initialize(string id, QRSticker qr, IBarcodeSticker barcode, bool activated)
        {
            ID = id;
            QR = qr;
            Barcode = barcode;
            Activated = activated;
        }

        public SimCard()
        {
            Initialize(id: "", qr: new QRSticker(), barcode: new BarcodeSticker(), activated: false);
        }
        public SimCard(IBarcodeSticker barsticker)
        {
            Initialize(id: barsticker.ICCID, qr: new QRSticker(), barcode: barsticker, activated: false);
        }

        public SimCard(IBarcodeSticker barsticker, QRSticker qrSticker)
        {
            Initialize(id: barsticker.ICCID, qr: qrSticker, barcode: barsticker, activated: false);
        }

        #endregion Constructors

        #region Public Methods

        public void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.BothSerialAndQRId)
        {
            if (idRestriction == SerialOrQR.BothSerialAndQRId)
            {
                if (ID == "" || ID == null) 
                    throw new ValidationException("Simcard doesn't have an ID!");
                if (QR.ID == "" || QR.ID == null) 
                    throw new ValidationException("Simcard doesn't have a QR with an ID!");
                if (QR.OfType != QRType.basestation && QR.OfType != QRType.rover && QR.OfType != QRType.tablet)
                    throw new ValidationException("The simcards QR is of an invalid type");
                if (Barcode.ICCID == "" || Barcode.ICCID == null) 
                    throw new ValidationException("Simcards barcode doesn't have an ICCID!");
                if (ID != Barcode.ICCID) 
                    throw new ValidationException("Simcards barcode ICCID isn't equal to Simcards ID!");
            }
        }

        #endregion Public Methods
    }

}