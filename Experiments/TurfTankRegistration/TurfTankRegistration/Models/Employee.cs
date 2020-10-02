using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public abstract class Employee
    {
        /*
         An Employee is the abstract class that all Users of the program should derive from.
         */
        // Properties
        public string Username { get; set; }
        protected string Password 
        { 
            get 
            {
                // Some Robustness and De-Salting
                return Password;
            }
            set
            {
                string PreSaltPW = value;
                // Some Salt Robustness
                Password = PreSaltPW;
            }
        }
        protected string Salt { get; set; }
        // Initializers
        protected Employee(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
