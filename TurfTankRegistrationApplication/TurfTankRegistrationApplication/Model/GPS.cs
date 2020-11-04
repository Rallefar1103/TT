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
    }
}
