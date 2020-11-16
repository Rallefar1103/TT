using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TurfTankRegistrationApplication.Model
{

    interface IComponent
    {
        string ID { get; set; } 
        bool ConnectedToRobot { get; set; }
        bool isBroken { get; set; }
        string GetSerialNumber();
        void FlagAsBroken();
    }

    public abstract class Component : IComponent
    {
        public string ID { get; set; }
        public bool ConnectedToRobot { get; set; }
        public bool isBroken { get; set; } = false;

        public void FlagAsBroken()
        {
            isBroken = true;
        }

        // Method returns the serial number of the specific component
        // if serial number is null it returns a string saying "null" else the actual serial number
        public string GetSerialNumber()
        {
            return ID ?? "null";      
        }
    }
}
