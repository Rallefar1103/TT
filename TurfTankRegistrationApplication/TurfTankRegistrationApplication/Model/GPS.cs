using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{
    interface IGPS
    {
        SimCard Simcard { get; set; }
    }

    public enum GPSType
    {
        Rover,
        Base,
        NoType
    }

    public class GPS : Component, IGPS, IValidateable
    {
        #region Public Attributes

        public GPSType ofType;

        public SimCard Simcard { get; set; }

        public static IRegistrationDBAPI<GPS> API { get; set; } = new RegistrationDBAPI<GPS>();

        #endregion

        #region Constructor
        public void Initialize(GPSType type, SimCard simcard)
        {
            ofType = type;
            Simcard = simcard;
        }
        public GPS()
        {
            Initialize(type: GPSType.NoType, simcard: new SimCard());
        }
        public GPS(GPSType type)
        {
            Initialize(type: type, simcard: new SimCard());
        }
        public GPS(GPSType type, SimCard simcard)
        {
            Initialize(type: type, simcard: simcard);
        }
        public GPS(SimCard simcard)
        {
            GPSType type = simcard.QR.ofType == QRType.ROVER ? GPSType.Rover : simcard.QR.ofType == QRType.BASE ? GPSType.Base : GPSType.NoType;
            Initialize(type: type, simcard: simcard);
        }

        #endregion

        #region Public Methods

        public override string GetSerialNumber()
        {
            if (ID == null)
            {
                if (ofType == GPSType.Rover)
                {
                    // Connect to Controller
                    ID = "SerialNumberRover";
                    return ID;

                }
                else
                {
                    // Connect to Base through Bluetooth
                    ID = "SerialNumberBase";
                    return ID;
                }
            }
            else
            {
                return ID;

            }
        }

        public override void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.AnyId)
        {
            base.ValidateSelf(idRestriction);
            if (ofType == GPSType.Rover && Simcard.QR.ofType != QRType.ROVER) throw new ValidationException($"The QR sticker is of the wrong type! The component is type Rover while the QR is type {Simcard.QR.ofType}");
            if (ofType == GPSType.Base && Simcard.QR.ofType != QRType.BASE) throw new ValidationException($"The QR sticker is of the wrong type! The component is type Base while the QR is type {Simcard.QR.ofType}");
            if (ofType == GPSType.NoType) throw new ValidationException($"The GPS does not have a type!");

            if (idRestriction == SerialOrQR.AnyId)
            {
                Simcard.ValidateSelf(idRestriction: SerialOrQR.BothSerialAndQRId);
                if(ID != "" || Simcard.QR.ID != "") throw new ValidationException("Neither GPS ID or Simcards QR Id is set!");
            }
            else if (idRestriction == SerialOrQR.OnlyQRId)
            {
                Simcard.ValidateSelf(idRestriction: SerialOrQR.BothSerialAndQRId);
            }
            else
            {
                throw new NotImplementedException("The requested SerialOrQr restriction havent been implemented on GPS.ValidateSelf");
            }
        }

        #endregion
    }
}


