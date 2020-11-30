using System;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{
    interface ISimCard
    {
        string ID { get; set; }
        QRSticker QR { get; set; }
        BarcodeSticker Barcode { get; set; }
        bool Activated { get; set; }
    }

    public class SimCard : ISimCard, IValidateable
    {
        #region Public Attributes

        public string ID { get; set; }
        public QRSticker QR { get; set; }
        public BarcodeSticker Barcode { get; set; }
        public bool Activated { get; set; }

        public static IRegistrationDBAPI<SimCard> API { get; set; } = new RegistrationDBAPI<SimCard>();

        #endregion Public Attributes

        #region Constructors
        public void Initialize(string id, QRSticker qr, BarcodeSticker barcode, bool activated)
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
        public SimCard(BarcodeSticker barsticker)
        {
            Initialize(id: barsticker.ICCID, qr: new QRSticker(), barcode: barsticker, activated: false);
        }
        public SimCard(string serial, BarcodeSticker barsticker)
        {
            Initialize(id: serial, qr: new QRSticker(), barcode: barsticker, activated: false);
        }

        #endregion Constructors

        #region Public Methods

        public void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.AnyId)
        {            
            if(idRestriction == SerialOrQR.BothSerialAndQRId)
            {
                if(ID == "")            throw new ValidationException("Simcard doesn't have an ID!");
                if(QR.ID == "")         throw new ValidationException("Simcard doesn't have a QR with an ID!");
                if(Barcode.ICCID == "") throw new ValidationException("Simcards barcode doesn't have an ICCID!");
                if(ID != Barcode.ICCID) throw new ValidationException("Simcards barcode ICCID isn't equal to Simcards ID!");
            }
            else
            {
                throw new NotImplementedException("The requested SerialOrQr restriction havent been implemented on Simcard.ValidateSelf");
            }
        }

        #endregion Public Methods
    }

}
