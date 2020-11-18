using System;
namespace TurfTankRegistrationApplication.Model
{
     interface ISimCard
    {
        string ID { get; set; }
        QRSticker QrSticker { get; set; }
        BarcodeSticker BarcodeSticker { get; set; }
        bool Activated { get; set; }
    }

    public class SimCard : ISimCard
    {
        public string ID { get; set; }
        public QRSticker QrSticker { get; set; }
        public BarcodeSticker BarcodeSticker { get; set; }
        public bool Activated { get; set; }
    }

}
