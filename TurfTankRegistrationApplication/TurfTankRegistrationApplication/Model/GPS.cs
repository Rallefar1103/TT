using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface IGPS
    {
        SimCard Simcard { get; set; }
    }

    public class GPS : Component, IGPS
    {
        #region Public Attributes
        public enum GPSType
        {
            Rover,
            Base,
            NoType
        }

        public GPSType ofType;

        public SimCard Simcard { get; set; }

        #endregion

        #region Public Methods

        public override string GetSerialNumber()
        {
            if (ID == null)
            {
                if (ofType == GPSType.Rover)
                {
                    // Connect to Controller
                    ID = "SerialNumberRover";
                    return ID;

                }
                else
                {
                    // Connect to Base through Bluetooth
                    ID = "SerialNumberBase";
                    return ID;
                }
            }
            else
            {
                return ID;

            }
        }

        #endregion

        #region Constructor
        public void Initialize(GPSType type, SimCard simcard)
        {
            ofType = type;
            Simcard = simcard;

        }
        public GPS()
        {
            Initialize(type: GPSType.NoType, simcard: new SimCard());
        }
        public GPS(GPSType type)
        {
            Initialize(type: type, simcard: new SimCard());

        }
        public GPS(GPSType type, SimCard simcard)
        {
            Initialize(type: type, simcard: simcard);

        }

        #endregion
    }
}


