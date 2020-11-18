using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IQRSticker
    {

        string ID { get; set; }
        string ICCID { get; set; }
        bool ConfirmAssemblyAndLabeling();

    }

    public class QRSticker : ScanableSticker, IQRSticker
    {

        public string ID { get; set; }
        public string ICCID { get; set; }

        public bool ConfirmAssemblyAndLabeling()
        {
            throw new NotImplementedException();
        }
    }
}
