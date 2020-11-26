using System;

using TurfTankRegistrationApplication.Connection;
namespace TurfTankRegistrationApplication.Model
{
    public interface IRobotPackage
    {
        string SerialNumber { get; set; }
        QRSticker QR { get; set; }
        Controller Controller { get; set; }
        Tablet Tablet { get; set; }
        GPS RoverGPS { get; set; }
        GPS BaseGPS { get; set; }

        bool IsSynchronized { get; set; }
        bool IsSelected { get; set; }

        Component SwapBrokenComponent(Component component);
        void SetAsSelected();
    }

    public class RobotPackage : IRobotPackage
    {
        #region Public Attributes

        public string SerialNumber { get; set; }
        public QRSticker QR { get; set; }
        public Controller Controller { get; set; }
        public Tablet Tablet { get; set; }
        public GPS RoverGPS { get; set; }
        public GPS BaseGPS { get; set; }
        public bool IsSynchronized { get; set; }
        public bool IsSelected { get; set; }

        #endregion Public Attributes


        #region Public functions

        public void SetAsSelected()
        {
            IsSelected = true;
        }


        public Component SwapBrokenComponent(Component component)
        {

            if (component is Controller)
            {
                Controller oldController = Controller;
                oldController.FlagAsBroken();
                Console.WriteLine("Swapping Controller");
                Controller = (Controller)component;

                return oldController;

            }
            else if (component is GPS)
            {
                if (((GPS)component).ofType == GPS.GPSType.Rover)
                {
                    GPS oldRover = RoverGPS;
                    oldRover.FlagAsBroken();
                    Console.WriteLine("Swapping Rover");
                    RoverGPS = (GPS)component;

                    return oldRover;
                }
                else
                {
                    GPS oldBase = BaseGPS;
                    oldBase.FlagAsBroken();
                    Console.WriteLine("Swapping Base");
                    BaseGPS = (GPS)component;

                    return oldBase;
                }

            }
            else if (component is Tablet)
            {
                Tablet oldTablet = Tablet;
                oldTablet.FlagAsBroken();
                Console.WriteLine("Swapping Tablet");
                Tablet = (Tablet)component;

                return oldTablet;

            }
            else
            {
                Console.WriteLine("Invalid input");
                return null;
            }
        }

        #endregion Public functions


        #region Constructor
        public void Initialize(Controller controller, Tablet tablet, GPS roverGPS, GPS baseGPS, QRSticker qr)
        {
            Controller = controller;
            Tablet = tablet;
            RoverGPS = roverGPS;
            BaseGPS = baseGPS;
            QR = qr;
        }

        public RobotPackage()
        {
            Initialize(
                controller: new Controller(),
                tablet: new Tablet(),
                roverGPS: new GPS() { ofType = GPS.GPSType.Rover },
                baseGPS: new GPS() { ofType = GPS.GPSType.Base },
                qr: new QRSticker());
        }

        public RobotPackage(Controller controller, Tablet tablet, GPS roverGPS, GPS baseGPS, QRSticker qr)
        {
            Initialize(
                controller: controller,
                tablet: tablet,
                roverGPS: roverGPS,
                baseGPS: baseGPS,
                qr: qr);
        }

        public RobotPackage(RobotItem schema)
        {
            SerialNumber = schema.Id ?? "";
            Controller = new Controller();
            Controller.ID = schema.Controller.SerialNumber ?? "";
            Controller.ActiveSSID = schema.Controller.Ssid ?? "";
            Controller.ActivePassword = schema.Controller.SsidPassword ?? "";
        }



        #endregion




    }

}

