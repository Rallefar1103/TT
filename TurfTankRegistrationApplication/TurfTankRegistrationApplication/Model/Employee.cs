using System;
using System.Collections.Generic;
using System.Text;

using TurfTankRegistrationApplication.Connection;

namespace TurfTankRegistrationApplication.Model
{
    public interface IEmployee
    {
        string ID { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool Login();
    }
    public class Employee : IEmployee
    {
        #region Public Attributes

        public string ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Username { get; set; }
        public string Password { get; set; }

        #endregion Public Attributes

        #region Constructors

        public void Initialize(string username, string password, IEmployeeDBAPI dbapi)
        {
            Username = username;
            Password = password;
            _DBAPI = dbapi;
        }
        public Employee()
        {
            Initialize("", "", new EmployeeDBAPI());
        }
        public Employee(string username, string password)
        {
            Initialize(username, password, new EmployeeDBAPI());
        }

        public Employee(string username, string password, IEmployeeDBAPI dbapi)
        {
            Initialize(username, password, dbapi);
        }

        #endregion Constructors

        #region Public Methods

        public bool Login()
        {
            if(_DBAPI.Authorize(this))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Public Methods

        #region Private Attributes

        private IEmployeeDBAPI _DBAPI { get; set; }

        #endregion Private Attributes

        #region Private Methods

        #endregion Private Methods
    }
}
