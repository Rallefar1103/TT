using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using TurfTankRegistrationApplication.Model;

namespace TurfTankRegistrationApplication.Connection
{
    public interface IDBAPI<T>
    {
        Task<T> GetById(string id);
        Task<List<T>> GetListOfObjects(List<T> someListWithIds);
    }

    public abstract class DBAPI<T> : IDBAPI<T>
    {
        #region public attributes
        

        #endregion public attributes
        public async virtual Task<T> GetById(string id)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        public async virtual Task<List<T>> GetListOfObjects(List<T> someListWithIds)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        #region protected attributes

        protected static HttpClient ApiClient { get; set; }

        #endregion protected attributes
    }
}
