using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Model;

namespace TurfTankRegistrationApplication.Connection
{
    public interface IEmployeeDBAPI : IDBAPI<Employee>
    {
        bool Authorize(Employee user);
    }
    public class EmployeeDBAPI : DBAPI<Employee>, IEmployeeDBAPI
    {
        public EmployeeDBAPI()
        {
            // pass
        }

        public bool Authorize(Employee user)
        {
            throw new NotImplementedException();
        }
    }
}
