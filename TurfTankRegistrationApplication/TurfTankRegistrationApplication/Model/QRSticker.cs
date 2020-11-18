using System;
namespace TurfTankRegistrationApplication.Model
{
    public interface IQRSticker
    {

        string ID { get; set; }
        bool ConfirmAssemblyAndLabeling();
    }

    public class QRSticker : ScanableSticker, IQRSticker
    {

        public string ID { get; set; }
        public enum QRType
        {
            Rover,
            Base,
            Tablet,
            RobotPackage,
            Controller, // denne qrsticker er af typen ControllerQRSticker (SSID==null på resten,)
            NoType
        }

        public QRType ofType;



        public bool ConfirmAssemblyAndLabeling()
        {
            throw new NotImplementedException();
        }

        #region Constructor

        public void Initialize(QRType type, string id)
        {
            ofType = type;
            ID = id;

        }
        public QRSticker()
        {
            Initialize(type: QRType.NoType, id: "");
        }

        public QRSticker(string id, QRType type)
        {
            Initialize(type: type, id: id);
        }
        #endregion
    }
}
