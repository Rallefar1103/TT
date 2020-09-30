using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Robot
    {
        public string SerialNumber { get; set; }
        protected Base RegisteredBase { get; set; }
        protected Controller RegisteredController { get; set; }
        protected Rover RegisteredRover { get; set; }
        protected Tablet RegisteredTablet { get; set; }
        public Robot SaveRobot()
        {
            return this;
        }
        private bool CheckForCompleteRobot()
        {
            return false;
        }
    }
}
