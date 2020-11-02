// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using NSubstitute;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Connection;

namespace TestUnit
{
    [TestFixture]
    public class TestClass1
    {
        [TestCase(5, "Testing for running the test without a parameter")]
        public void TestInitializationOfClass_NoParams_MustCreateDefaultFiveValue(int ExpectedValue, string TestDesc)
        {
            var mock = Substitute.For<IConnection1>(); // During this test, the Connection1 class is replaced with a "Mock" object. An empty "test double".
            mock.MakeAnAPICall().Returns(5);  // The Mock is setup to have a method "MakeAnAPICall" and make it return 5.
            Class1 fake = new Class1(mock); // Creates a fake injected with the mock object

            int Actual = fake.FiveVar; // A Test for the creation of the model itself is correct. (structure)

            mock.Received().MakeAnAPICall(); // Verify that the GetFiveFromData method was called with the expected value
            Assert.AreEqual(ExpectedValue, Actual, TestDesc);
        }
    }
}
