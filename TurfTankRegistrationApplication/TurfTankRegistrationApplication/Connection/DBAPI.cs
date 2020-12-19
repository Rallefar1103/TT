using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Net;
using System.Net.Http;

using TurfTankRegistrationApplication.Model;

namespace TurfTankRegistrationApplication.Connection
{
    /// <summary>
    /// An interface which handles the needed interactions with the RegistrationDBAPI
    /// This interface also inherits from IDBAPI, to give general Database interaction functionality.
    /// </summary>
    /// <typeparam name="T">The Type which will be retrieved/saved</typeparam>
    /// <typeparam name="S">The Schema which matches the Type on the Database side</typeparam>
    public interface IDBAPI<T> where T : new()
    {
        Client ApiClientRef { get; set; }
        Task<bool> Save(IValidateable someObject);
        Task<T> GetById(string id);
        Task<List<T>> GetListOfObjects(List<T> someListWithIds);
    }

    public class DBAPI<T> : IDBAPI<T> 
        where T : new()
    {
        #region Public Attributes

        public Client ApiClientRef { get; set; }

        #endregion Public Attributes

        #region Constructors

        public void Initialize(Client client) 
        {
            ApiClientRef = client;
        }
        public DBAPI() 
        {
            Initialize(App.ApiClient);
        }
        public DBAPI(Client testClient)
        {
            Initialize(testClient);
        }

        #endregion Constructors

        #region Public Methods

        public async Task<bool> Save(IValidateable someObject)
        {
            await Task.Delay(1);
            Console.WriteLine("Save run, but not implemented yet!");
            return false;
        }
        
        public async Task<T> GetById(string id)
        {
            T type = new T();
            if(type is RobotPackage)
            {
                RobotItemModel robotSchema = await ApiClientRef.GetRobotByIdAsync(id);

                RobotPackage newObject = new RobotPackage(robotSchema);
                return (T)Convert.ChangeType(newObject, typeof(RobotPackage));
            }
            else if (type is GPS)
            {
                if (id.Contains("basestation"))
                {
                    RobotBasestationItemModel BaseSchema = await ApiClientRef.GetBasestationByIdAsync(id);

                    GPS newObject = new GPS(BaseSchema);
                    return (T)Convert.ChangeType(newObject, typeof(GPS));

                }
                else
                {
                    throw new Exception("Type GPS did not contain the correct GPS QR ID");
                }

            }
            else
            {
                throw new Exception($"The type {typeof(T)} is not yet added to the RegistrationDBAPI.GetById method, or might not be added to swagger yet.");
            }
            
        }

        public async Task<List<T>> GetListOfObjects(List<T> someListWithIds)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        #endregion Public Methods

        #region private attribute

        #endregion private attributes

        #region Private Methods



        #endregion Private Methods
    }
}
