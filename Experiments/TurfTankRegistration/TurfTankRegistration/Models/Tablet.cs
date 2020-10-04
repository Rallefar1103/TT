using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Tablet
    {
        public string SerialNumber { get; set; }
        protected SimCard RegisteredSimCard { get; set; }
        public Tablet()
        {
            SerialNumber = "";
            RegisteredSimCard = new SimCard();
            RegisteredSimCard.Active = true;
        }
        public void Login()
        {

        }
        public void ScanQR()
        {

        }
        public void ScanBarCode()
        {

        }
    }
}
