using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Robot
    {
        public string SerialNumber { get; set; }
        public Base RegisteredBase { get; set; }
        public Controller RegisteredController { get; set; }
        public Rover RegisteredRover { get; set; }
        public Tablet RegisteredTablet { get; set; }
        public Robot()
        {
            SerialNumber = "";
            RegisteredBase = new Base();
            RegisteredController = new Controller();
            RegisteredRover = new Rover();
            RegisteredTablet = new Tablet();
        }
        public string SaveRobot()
        {
            return SerialNumber;
        }
        public bool CheckForCompleteRobot()
        {
            if (RegisteredBase.SerialNumber != "" &&
                RegisteredController.SerialNumber != "" &&
                RegisteredRover.SerialNumber != "" &&
                RegisteredTablet.SerialNumber != "" &&
                SerialNumber != "")
            {
                return true;
            }
            else
                return false;
        }
    }
}
