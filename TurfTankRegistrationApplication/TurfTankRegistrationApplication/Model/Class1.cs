using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;

namespace TurfTankRegistrationApplication.Model
{
    public class Class1
    {
        private Connection1 API { get; set; }
        public int FiveVar { get; set; }
        public Class1()
        {
            API = new Connection1();
            FiveVar = GetValFromDB(API);
        }
        public Class1(Connection1 api) // Constructor made specifically for testing
        {
            API = api;
            FiveVar = GetValFromDB(API);
        }
        public int GetValFromDB(Connection1 api) // Requests the Connection Layer to get some data
        {
            return api.MakeAnAPICall();
        }
    }
}
