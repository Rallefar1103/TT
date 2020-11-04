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
        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public QRSticker QrSticker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public BarcodeSticker BarcodeSticker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Activated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}
