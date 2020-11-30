using System;
using System.Collections.Generic;
using System.Linq;

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

        public QRSticker(string scanResult)
        {
            List<string> results = scanResult.Split(';').ToList();

            if (results.Count == 2)
            {            
                string id = results[1];
                QRType type;

                if (Enum.TryParse(results[0], out type))
                {
                    Initialize(type, id);
                    Console.WriteLine("------------------The QR sticker was successfully scanned------------------");
                }
                else
                    throw new FormatException("------------------The scan result could not be parsed------------------");
            }

        }
        #endregion
    }
}
