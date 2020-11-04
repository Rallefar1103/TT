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
        string GetSerialNumber();
        void FlagAsBroken();
    }

    public abstract class Component : IComponent
    {
        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ConnectedToRobot { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void FlagAsBroken()
        {
            throw new NotImplementedException();
        }

        public string GetSerialNumber()
        {
            throw new NotImplementedException();
        }
    }
}
