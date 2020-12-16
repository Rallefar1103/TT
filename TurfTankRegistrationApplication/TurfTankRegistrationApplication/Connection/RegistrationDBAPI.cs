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
    public interface IRegistrationDBAPI<T> : IDBAPI<T> where T : new()
    {
        Task<bool> Save(IValidateable someObject);
    }

    public class RegistrationDBAPI<T> : DBAPI<T>, IRegistrationDBAPI<T> 
        where T : new()
    {
        #region Public Attributes

        public string GetByIdURI { get; set;}
        public string GetListOfObjectsURI { get; set; }
        public string SaveURI { get; set; }
        public Client ApiClientRef { get; set; }

        #endregion Public Attributes

        #region Constructors

        public void Initialize(Client client) 
        {
            ApiClientRef = client;
        }
        public RegistrationDBAPI() 
        {
            Initialize(App.ApiClient);
        }
        public RegistrationDBAPI(Client testClient)
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

        #endregion Public Methods

        #region Overwritten Methods
        
        public async override Task<T> GetById(string id)
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

        public async override Task<List<T>> GetListOfObjects(List<T> someListWithIds)
        {
            await Task.Delay(1);
            throw new NotImplementedException();
        }

        #endregion Overwritten Methods

        #region private attribute

        #endregion private attributes

        #region Private Methods



        #endregion Private Methods
    }
}
