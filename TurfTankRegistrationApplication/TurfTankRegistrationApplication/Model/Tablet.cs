using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface ITablet
    {
        void ActivateSimcard();
    }
    public class Tablet : Component, ITablet
    {
        public void ActivateSimcard()
        {
            throw new NotImplementedException();
        }
    }
}
