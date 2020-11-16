using System;
namespace TurfTankRegistrationApplication.Model
{
     public interface IRobotPackage
    {
         string SerialNumber { get; set; }
         Controller Controller { get; set; }
         Tablet Tablet { get; set; }
         GPS RoverGPS { get; set; }
         GPS BaseGPS { get; set; }
         QRSticker QRSticker { get; set; }
         bool IsSynchronized { get; set; }
         bool IsSelected { get; set; }

         Component SwapBrokenComponent(Component component);
         void SetAsSelected();
           
    }

    public class RobotPackage : IRobotPackage
    {
        #region Public Attributes

        public string SerialNumber { get; set; }
        public Controller Controller { get; set; }
        public Tablet Tablet { get; set; }
        public GPS RoverGPS { get; set; }
        public GPS BaseGPS { get; set; }
        public QRSticker QRSticker { get; set; }
        public bool IsSynchronized { get; set; }
        public bool IsSelected { get; set; }

        #endregion Public Attributes

        #region Constructor

        public RobotPackage()
        {
            Controller = new Controller();
            Tablet = new Tablet();
            RoverGPS = new GPS();
            BaseGPS = new GPS();
            QRSticker = new QRSticker();
        }


        #endregion Constructor

        #region Public functions

        public void SetAsSelected()
        {
            IsSelected = true;
        }

        // Method overwrites existing and broken component with a new instance of the specific component type
        // Method returns the old component with the isBroken attribute.
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
                if (((GPS)component).ofType == GPS.Type.Rover)
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

    }

}

