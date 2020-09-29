using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Project.Model
{
    class Employee
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Employee() { }
        public Employee(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
