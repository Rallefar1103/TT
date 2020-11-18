using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface ITablet
    {
        SimCard SimCard { get; set; }
        void ActivateSimcard();
    }
    public class Tablet : Component, ITablet
    {
        public SimCard SimCard { get; set; }

        public void ActivateSimcard()
        {
            throw new NotImplementedException();
        }
    }
}
