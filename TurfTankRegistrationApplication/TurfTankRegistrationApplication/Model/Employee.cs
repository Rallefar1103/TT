using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Model
{
    interface IEmployee
    {
        string ID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool Login();
    }
    class Employee : IEmployee
    {
        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Username { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Login()
        {
            throw new NotImplementedException();
        }
    }
}
