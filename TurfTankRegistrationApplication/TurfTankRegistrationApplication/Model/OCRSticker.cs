using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IOCRSticker
    {

        string ID { get; set; }
        string EthMAC { get; set; }
        string WifiMAC { get; set; }

    }

    public class OCRSticker : ScanableSticker, IOCRSticker
    {

        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EthMAC { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WifiMAC { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
