using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class Controller : Component
    {
        public string SerialNumber { get; set; }
        public string ID { get; set; }
        public string SSID { get; set; }
        public string Password { get; set; }
        public Controller()
        {
            SerialNumber = "";
            ID = "";
            SSID = "";
            Password = "";
        }
    }
}
