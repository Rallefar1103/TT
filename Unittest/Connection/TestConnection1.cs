using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Connection;

namespace Unittest.Connection
{
    [TestFixture]
    public class TestConnection1
    {
        [TestCase(5, "Testing running the method without parameters")]
        public void TestGetFiveFromData_NoParam_MustReturnFive(int Expected, string TestDesc)
        {
            // Mock out the actual database call
            Connection1 TestObj = new Connection1();

            var Actual = TestObj.GetFiveFromData();

            Assert.AreEqual(Expected, Actual,  TestDesc);
        }
    }
}
