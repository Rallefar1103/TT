using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface IGPS
    {
    }

    public class GPS : Component
    {
        public enum Type
        {
            Rover,
            Base
        }

        public Type ofType;

        public GPS()
        {

        }

        public override string GetSerialNumber()
        {
            if (ID == null)
            {
                if (ofType == Type.Rover)
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
    }
}
