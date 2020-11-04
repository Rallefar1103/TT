using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IControllerQRSticker
    {
        string FinalSSID { get; set; }
        string FinalPASSWORD { get; set; }

    }

    public class ControllerQRSticker : ScanableSticker, IControllerQRSticker
    {

        public string FinalSSID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FinalPASSWORD { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

}
