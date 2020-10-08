using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public abstract class Component
    {
        public bool ConnectedToRobot = false;
        public Robot RegisterTo(Robot robot)
        {
            return robot;
        }
    }
}
