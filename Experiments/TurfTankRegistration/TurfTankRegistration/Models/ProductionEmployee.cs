using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistration.Models
{
    public class ProductionEmployee : Employee
    {
        /*
         A ProductionEmployee uses the program to register Robots and Components together
         */
        // Initializers
        public ProductionEmployee() : base() { }
        public ProductionEmployee(string username, string password) : base(username, password) { }
    }
}
