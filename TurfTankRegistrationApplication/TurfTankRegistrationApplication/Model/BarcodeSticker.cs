using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IBarcodeSticker
    {

        string ID { get; set; }
        string ICCID { get; set; }

    }

    public class BarcodeSticker : ScanableSticker, IBarcodeSticker
    {

        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ICCID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
