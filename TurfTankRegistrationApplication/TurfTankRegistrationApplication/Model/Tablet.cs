using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{
    interface ITablet
    {
        SimCard Simcard { get; set; }
        void ActivateSimcard();
    }
    public class Tablet : Component, ITablet, IValidateable
    {
        public SimCard Simcard { get; set; }

        public static IRegistrationDBAPI<Tablet> API { get; set; }
        public IRegistrationDBAPI<Tablet> SelfAPI { get => API; }

        #region Constructors
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
        #endregion Constructors

        #region Public Methods

        public void ActivateSimcard()
        {
            throw new NotImplementedException();
        }

        public override void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.AnyId)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods
    }
}
