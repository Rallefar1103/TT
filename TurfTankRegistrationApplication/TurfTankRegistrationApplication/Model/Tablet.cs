using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface ITablet
    {
        SimCard Simcard { get; set; }
        void ActivateSimcard();
    }
    public class Tablet : Component, ITablet
    {
        public SimCard Simcard { get; set; }

        public void ActivateSimcard()
        {
            throw new NotImplementedException();
        }

        #region Constructor
        public void Initialize(SimCard simcard)
        {
            Simcard = simcard;
        }

        public Tablet()
        {
            Initialize(simcard: new SimCard());
        }

        public Tablet(SimCard simcard)
        {
            Initialize(simcard: simcard);

        }
        #endregion
    }
}
