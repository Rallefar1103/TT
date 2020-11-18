using System;
namespace TurfTankRegistrationApplication.Model
{
    interface ISimCard
    {
        string ID { get; set; }
        QRSticker QR { get; set; }
        BarcodeSticker Barcode { get; set; }
        bool Activated { get; set; }
    }

    public class SimCard : ISimCard
    {
        #region Public Attributes

        public string ID { get; set; }
        public QRSticker QR { get; set; }
        public BarcodeSticker Barcode { get; set; }
        public bool Activated { get; set; }

        #endregion

        #region Constructor
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

        #endregion
    }

}
