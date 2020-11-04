using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Model;

namespace TurfTankRegistrationApplication.Connection
{
    interface IEmployeeDBAPI
    {
        // pass
    }
    class EmployeeDBAPI : DBAPI<Employee>, IEmployeeDBAPI
    {
        public EmployeeDBAPI()
        {
            // pass
        }
        public override Employee Get(Employee employee)
        {
            // Call to base
            throw new NotImplementedException();
        }
    }
}
