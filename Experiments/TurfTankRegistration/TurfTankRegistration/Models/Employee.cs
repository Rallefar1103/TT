using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TurfTankRegistration.Models
{
    public abstract class Employee
    {
        public string Username { get; set; }
        public string Password { get; set; }
        protected Employee(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public bool LoginVerified()
        {
            if (Username != "" && Password != "")
                return true;
            else
                return false;
        }
    }
}
