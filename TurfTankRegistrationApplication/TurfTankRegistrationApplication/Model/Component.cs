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
        bool IsBroken { get; set; }
        string GetSerialNumber();
        void FlagAsBroken();
    }

    public abstract class Component : IComponent
    {
        public string ID { get; set; }
        public bool ConnectedToRobot { get; set; }
        public bool IsBroken { get; set; }

        public void FlagAsBroken()
        {
            IsBroken = true;
        }

        public virtual string GetSerialNumber()
        {
            return ID ?? "null";
        }
    }
}