using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication.Connection
{
    interface IDBAPI<T>
    {
        T Get(T someObject);
    }
    abstract class DBAPI<T> : IDBAPI<T>
    {
        public virtual T Get(T someObject)
        {
            throw new NotImplementedException();
        }
    }
}
