using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Base : GPS
    {
        public string SerialNumber { get; set; }
        protected SimCard RegisteredSimCard { get; set; }
        public Base()
        {
            SerialNumber = "";
            RegisteredSimCard = new SimCard();
        }
        public override void GetSerialNumber()
        {
            
        }
    }
}
