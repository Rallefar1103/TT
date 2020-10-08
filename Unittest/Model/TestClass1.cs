using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Connection;
using NUnit.Framework.Constraints;

namespace Unittest.Model
{
    [TestFixture]
    public class TestClass1
    {
        [TestCase(5, "Testing for running the test without a parameter")]
        public void TestInitializationOfClass_NoParams_MustCreateDefaultFiveValue(int Expected, string TestDesc)
        {
            // Mock out call to Connection
            Class1 TestObj = new Class1();

            int Actual = TestObj.FiveVar;

            Assert.AreEqual(Expected, Actual, TestDesc);
        }
    }
}
