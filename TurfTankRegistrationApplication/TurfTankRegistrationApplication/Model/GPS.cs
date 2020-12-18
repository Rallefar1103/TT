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
        public string SerialNumber { get; set; }

        public static IDBAPI<GPS> API { get; set; } = new DBAPI<GPS>();

        #endregion

        #region Constructor
        public void Initialize(GPSType type, SimCard simcard, string serialNumber)
        {
            ofType = type;
            ID = simcard.QR.ID;
            Simcard = simcard;
            SerialNumber = serialNumber;
            API = new DBAPI<GPS>();
        }
        public GPS()
        {
            Initialize(type: GPSType.NoType, simcard: new SimCard(), serialNumber: "");
        }
        public GPS(GPSType type, string serialNumber = "")
        {
            Initialize(type: type, simcard: new SimCard(), serialNumber: serialNumber);
        }
        public GPS(GPSType type, SimCard simcard, string serialNumber = "")
        {
            Initialize(type: type, simcard: simcard, serialNumber: serialNumber);
        }
        public GPS(SimCard simcard, string serialNumber = "")
        {
            GPSType type;

            if (simcard.QR.OfType == QRType.rover)
                type = GPSType.Rover;
            else if (simcard.QR.OfType == QRType.basestation)
                type = GPSType.Base;
            else
                type = GPSType.NoType;

            Initialize(type: type, simcard: simcard, serialNumber: serialNumber);
        }
        public GPS(RobotBasestationItemModel baseStationItemModel)
        {
            ofType = GPSType.Base;
            ID = baseStationItemModel.Id;
            //Simcard = baseStationItemModel.simcard;
            SerialNumber = baseStationItemModel.SerialNumber;
        }

        #endregion constructors

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

        public override void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.BothSerialAndQRId)
        {
            base.ValidateSelf(SerialOrQR.AnyId);

            if (idRestriction != SerialOrQR.OnlyQRId && idRestriction != SerialOrQR.BothSerialAndQRId)
                throw new ValidationException("The idRestriction should be either OnlyQrId or BothSerialAndQrId");

            if (ofType == GPSType.Rover && Simcard.QR.OfType != QRType.rover) 
                throw new ValidationException($"The QR sticker is of the wrong type! The component is type Rover while the QR is type {Simcard.QR.OfType}");
            if (ofType == GPSType.Base && Simcard.QR.OfType != QRType.basestation) 
                throw new ValidationException($"The QR sticker is of the wrong type! The component is type Base while the QR is type {Simcard.QR.OfType}");
            if (ofType == GPSType.NoType) 
                throw new ValidationException($"The GPS does not have a type!");

            try
            {
                Simcard.ValidateSelf(idRestriction: SerialOrQR.BothSerialAndQRId);
            }
            catch (ValidationException e)
            {
                throw new ValidationException("The simcard in the gps is not valid: " + e.Message);
            }
          
            if (idRestriction == SerialOrQR.BothSerialAndQRId)
            {
                if (SerialNumber == "" || SerialNumber == null)
                    throw new ValidationException("The GPS has no serial number");
            }
 
            if (ID != Simcard.QR.ID)
            {
                throw new ValidationException("The GPS ID does not match the simcard QR ID");
            }
        }

        #endregion
    }
}


