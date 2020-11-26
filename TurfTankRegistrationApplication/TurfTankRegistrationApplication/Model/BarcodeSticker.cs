using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IBarcodeSticker
    {

        string ICCID { get; set; }

    }

    public class BarcodeSticker : ScanableSticker, IBarcodeSticker
    {

        public string ICCID { get; set; }
    }
}
