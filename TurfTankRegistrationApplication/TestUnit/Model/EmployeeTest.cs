using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Model;

namespace TestUnit.Model
{
    [TestFixture]
    public class EmployeeTest
    {
        #region Login

        [TestCase("NameWithoutSpaces", "PasswordWithoutSpaces", "Testing that an employee which is on the database actually validates.")]
        public void Login_ValidEmployee_ShouldValidateCorrectly(string validName, string validPassword, string Desc)
        {
            // Mock
            var mockEmployeeDBAPI = Substitute.For<IEmployeeDBAPI>();
            Employee employeeInDatabase = new Employee(validName, validPassword);
            mockEmployeeDBAPI.Authorize(Arg.Any<Employee>()).Returns(true);

            // Arrange
            Employee testObject = new Employee(validName, validPassword, mockEmployeeDBAPI);

            // Act
            bool Actual = testObject.Login();

            // Assert
            mockEmployeeDBAPI.Received().Authorize(testObject);
            Assert.AreEqual(true, Actual, Desc);
        }

        [TestCase("SomeUser", "SomePassword", "Testing that a User wont be validated if not on the DB")]
        public void Login_EmployeeNotInDB_ShouldValidateToFalse(string invalidName, string invalidPassword, string Desc)
        {
            // Mock
            var mockEmployeeDBAPI = Substitute.For<IEmployeeDBAPI>();
            mockEmployeeDBAPI.Authorize(Arg.Any<Employee>()).Returns(false);

            // Arrange
            Employee testObject = new Employee(invalidName, invalidPassword, mockEmployeeDBAPI);

            // Act
            bool Actual = testObject.Login();

            // Assert
            mockEmployeeDBAPI.Received().Authorize(testObject);
            Assert.AreEqual(false, Actual, Desc);
        }

        #endregion Login
    }
}
