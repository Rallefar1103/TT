using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;

namespace TurfTankRegistrationApplication.Model
{
    public class Class1
    {
        protected Connection1 API = new Connection1();
        public int FiveVar { get; set; }
        public Class1()
        {
            FiveVar = API.GetFiveFromData();
        }
    }
}
