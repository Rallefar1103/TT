using System;
namespace TurfTankRegistrationApplication.Model
{
     public interface IRobotPackage
    {
         string SerialnumberFromChassi { get; set; }
         Controller Controller { get; set; }
         Tablet Tablet { get; set; }
         GPS RoverGps { get; set; }
         GPS BaseGps { get; set; }
         QRSticker QrSticker { get; set; }
         bool IsSynchronized { get; set; }
         bool IsSelected { get; set; }

         void SwapComponent(Component component);
         void SetAsSelected();
    }

    public class RobotPackage : IRobotPackage
    {
        #region Public Attributes

        public string SerialnumberFromChassi { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Controller Controller { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Tablet Tablet { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GPS RoverGps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public GPS BaseGps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public QRSticker QrSticker { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSynchronized { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        #endregion Public Attributes


        #region Public functions

        public void SetAsSelected()
        {
            throw new NotImplementedException();
        }

        public void SwapComponent(Component component)
        {
            throw new NotImplementedException();
        }

        #endregion Public functions


        #region Constructor

        public RobotPackage()
        {
           
        }


        #endregion Constructor




    }

}

