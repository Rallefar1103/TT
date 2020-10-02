using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    class StorageEmployee : Employee
    {
        /*
         A ProductionEmployee uses the program to register SimCards with GPS's and Tablets
         */
        // Initializers
        public StorageEmployee(string username, string password) : base(username, password) { }
    }
}
