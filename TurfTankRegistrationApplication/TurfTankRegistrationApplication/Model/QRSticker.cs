using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IQRSticker
    {

        int ID { get; set; }
        string ICCID { get; set; }
        bool ConfirmAssemblyAndLabeling();

    }

    public class QRSticker : ScanableSticker, IQRSticker
    {

        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ICCID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool ConfirmAssemblyAndLabeling()
        {
            throw new NotImplementedException();
        }
    }
}
