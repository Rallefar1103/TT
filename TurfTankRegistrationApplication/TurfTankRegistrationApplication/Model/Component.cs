using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Exceptions;

namespace TurfTankRegistrationApplication.Model
{

    public interface IComponent
    {
        string ID { get; set; }
        bool ConnectedToRobot { get; set; }
        bool IsBroken { get; set; }
        string GetSerialNumber();
        void FlagAsBroken();
    }

    public abstract class Component : IComponent, IValidateable
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

        public virtual void ValidateSelf(SerialOrQR idRestriction = SerialOrQR.AnyId)
        {
            if (IsBroken) throw new ValidationException("The Component has previously been registered as broken");
            
            if(idRestriction == SerialOrQR.AnyId || idRestriction == SerialOrQR.OnlySerialId || idRestriction == SerialOrQR.BothSerialAndQRId)
            {
                if (ID == "" || ID == null) throw new ValidationException("The Component doesn't have its ID set");
            }
            else if(idRestriction == SerialOrQR.OnlyQRId || idRestriction == SerialOrQR.NoId)
            {
                if (ID != "" && ID != null) throw new ValidationException("The Component has its ID set, which it shouldn't have");
            }
        }
    }
}