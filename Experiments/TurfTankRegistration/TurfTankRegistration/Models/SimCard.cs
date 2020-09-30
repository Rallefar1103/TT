using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class SimCard
    {
        public string SerialNumber { get; set; }
        public string ICCID { get; set; }
        public Component ComponentType { get; set; }
        public bool Active { get; set; }
        public void RegisterTo(Component TabletOrGPS)
        {
            
        }
    }
}
