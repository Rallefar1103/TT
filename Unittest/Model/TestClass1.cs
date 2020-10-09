using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using Moq;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Connection;

namespace Unittest.Model
{
    [TestFixture]
    public class TestClass1
    {
        [TestCase(5, "Testing for running the test without a parameter")]
        public void TestInitializationOfClass_NoParams_MustCreateDefaultFiveValue(int ExpectedValue, string TestDesc)
        {
            var mock = new Mock<Connection1>(); // During this test, the Connection1 class is replaced with a "Mock" object. An empty "test double".
            mock.Setup(c => c.MakeAnAPICall()).Returns(5);  // The Mock is setup to have a method "MakeAnAPICall" and make it return 5.
            Class1 fake = new Class1(mock.Object); // Creates a fake injected with the mock object

            int Actual = fake.FiveVar; // A Test for the creation of the model itself is correct. (structure)

            mock.Verify(c => c.MakeAnAPICall(), Times.Once()); // Verify that the GetFiveFromData method was called with the expected value
            Assert.AreEqual(ExpectedValue, Actual, TestDesc);
        }
    }
}
