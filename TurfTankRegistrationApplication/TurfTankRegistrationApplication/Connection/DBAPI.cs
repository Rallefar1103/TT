using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurfTankRegistrationApplication.Connection
{
    public interface IDBAPI<T>
    {
        T GetById(T someObject);
        List<T> GetListOfObjects(List<T> someListWithIds);
    }
    public abstract class DBAPI<T> : IDBAPI<T>
    {
        public T GetById(T someObject)
        {
            throw new NotImplementedException();
        }

        public List<T> GetListOfObjects(List<T> someListWithIds)
        {
            throw new NotImplementedException();
        }
    }
}
