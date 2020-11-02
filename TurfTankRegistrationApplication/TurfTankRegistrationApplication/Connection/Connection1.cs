using System;
using System.Collections.Generic;
using System.Text;

namespace TurfTankRegistrationApplication.Connection
{
    public interface IConnection1
    {
        int MakeAnAPICall();
    }
    public class Connection1 : IConnection1
    {
        public Connection1()
        {
            
        }
        protected virtual void Close() // Simulates closing the connection
        {
            // Me Am Close
        }

        public virtual int MakeAnAPICall() // Handles the logic regarding HOW to access the database
        {
            string options = "Some options needed to satisfy the database access";
            int data = ResponseFromDB(options);
            this.Close();
            return data;
        }
        public virtual int ResponseFromDB(string options) // Simulates the actual response from the database. Would probably be an http call or something
        {
            return 3; // Something wrong with the DB! Returns wrong value! 
            // This is just made to show how all the mocked tests pass, while integration fails ;)
        }
    }
}
