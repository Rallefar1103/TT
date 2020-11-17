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
            if (this.ID != null || this.ID.Length != 0)
            {
                return this.ID;

            }
            else
            {
                if (this.ofType == Type.Rover)
                {
                    // Connect to Controller
                    this.ID = "SerialNumberRover";
                    return this.ID;

                }
                else
                {
                    // Connect to Base through Bluetooth
                    this.ID = "SerialNumberBase";
                    return this.ID;
                }
            }
        }
    }
}
