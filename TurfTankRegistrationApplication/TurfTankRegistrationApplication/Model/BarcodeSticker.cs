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

        private void Initialize(string iccid)
        {
            ICCID = iccid;
        }

        public BarcodeSticker()
        {
            Initialize("");
        }

        public BarcodeSticker(string scannedData)
        {
            Initialize(scannedData);
        }
    }
}
