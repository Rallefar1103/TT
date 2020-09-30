using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Base : GPS
    {
        public string SerialNumber { get; set; }
        protected SimCard RegisteredSimCard { get; set; }
        public override void GetSerialNumber()
        {
            
        }
    }
}
