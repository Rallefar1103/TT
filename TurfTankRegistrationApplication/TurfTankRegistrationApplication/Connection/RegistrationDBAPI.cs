using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication.Connection
{
    interface IRegistrationDBAPI<T>
    {
        T Save(T someObject);
    }
    class RegistrationDBAPI<T> : DBAPI<T>, IRegistrationDBAPI<T>
    {
        public RegistrationDBAPI()
        {
            // pass
        }
        public override T Get(T someObject)
        {
            throw new NotImplementedException();
        }

        public T Save(T someObject)
        {
            throw new NotImplementedException();
        }
    }
}
