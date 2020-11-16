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
            Component OldComponent;
            
            if (component == Controller)
            {
                OldComponent = Controller;
                Console.WriteLine("Swapping Controller");
                Controller = (Controller)component;
              
            } else if (component == RoverGPS)
            {
                OldComponent = RoverGPS;
                Console.WriteLine("Swapping Rover");
                RoverGPS = (GPS)component;
            } else if (component == BaseGPS)
            {
                OldComponent = BaseGPS;
                Console.WriteLine("Swapping Base");
                BaseGPS = (GPS)component;
            } else if (component == Tablet)
            {
                OldComponent = Tablet;
                Console.WriteLine("Swapping Tablet");
                Tablet = (Tablet)component;
            } else
            {
                OldComponent = null;
                Console.WriteLine("Invalid input");
            }

            OldComponent.FlagAsBroken();
            return OldComponent;
        }

        #endregion Public functions

    }

}

