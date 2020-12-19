using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{
    interface ITablet : IComponent
    {
        SimCard Simcard { get; set; }
        void ActivateSimcard();
    }
    public class Tablet : Component, ITablet, IValidateable
    {
        public SimCard Simcard { get; set; }

        public static IDBAPI<Tablet> API { get; set; } = new DBAPI<Tablet>();

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

        public Tablet(RobotTabletItemModel schema)
        {
            SimCard sim = new SimCard();
            sim.QR = new QRSticker();
            sim.QR.ID = schema.Id;
            sim.Barcode = new BarcodeSticker();
            sim.Barcode.ICCID = schema.Imei;
            Initialize(simcard: sim);
        }
        #endregion Constructors

        #region Public Methods

        public void ActivateSimcard()
        {
            throw new NotImplementedException();
        }

        public override void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.NoId)
        {
            base.ValidateSelf(idRestriction);
            if (Simcard != null)
                try
                {
                    Simcard.ValidateSelf();
                }
                catch (Exception e)
                {
                    throw new ValidationException("There was an issue with the tablet simcard: " + e.Message);
                }
            else
                throw new ValidationException("There is no simcard registered to the tablet");
            
            if (Simcard.QR.OfType != QRType.tablet)
            {
                throw new ValidationException("The simcard QR is not a tablet type");
            }
        }

        #endregion Public Methods
    }
}
