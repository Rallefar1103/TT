using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.ViewModel;
namespace TestUnit.ViewModel
{
    [TestFixture]
    public class RoverRegVMTest
    {
        [TestCase("Testing that the GetRoverSerialNumber actually returns a parsed string of the response")]
        public void GetRoverSerialNumber_NoParams_ShouldReturnString(string desc)
        {
            // Arrange
            RoverRegistrationViewModel testObject = new RoverRegistrationViewModel();
            string expected = "Leanne Graham";

            // Act
            testObject.GetRoverSerialNumber("cmd");
            string actual = testObject.RoverResponse;

            // Assert
            Assert.AreEqual(expected, actual, desc);
        }
    }
}
