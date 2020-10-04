using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Rover : GPS
    {
        public string SerialNumber { get; set; }
        protected SimCard RegisteredSimCard { get; set; }
        public Rover()
        {
            SerialNumber = "";
            RegisteredSimCard = new SimCard();
        }
        public override void GetSerialNumber()
        {
            
        }
    }
}
