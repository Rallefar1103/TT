using System;

using TurfTankRegistrationApplication.Model;

namespace TurfTankRegistrationApplication.Model
{
    public interface IControllerQRSticker
    {
        string FinalSSID { get; set; }
        string FinalPASSWORD { get; set; }

    }

    public class ControllerQRSticker : QRSticker, IControllerQRSticker
    {
        public string FinalSSID { get; set; }
        public string FinalPASSWORD { get; set; }

        public ControllerQRSticker()
        {
            Data = "";
        }
        public ControllerQRSticker(string scannedData)
        {
            Data = scannedData;
        }
    }

}