using Moq;
using Moq.Language;
using Moq.Protected;
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
        [TestCase(5, 5, "Testing running the method without parameters")]
        public void TestGetFiveFromData_NoParam_MustReturnFive(int ArrangedValue, int Expected, string TestDesc)
        {
            Mock<Connection1> mock = new Mock<Connection1>(); // This creates a "test double".
            mock.CallBase = true; // Makes it so it only partially mocks the object. Ie. it now is like the original, unless otherwise specified. However, all methods must be virtual (so they can be overwritten)!!!
            mock.Setup(r => r.ResponseFromDB(It.IsAny<string>())).Returns(ArrangedValue); // Setup so the RestAPICall should accept any int and return the ArrangedMock (5)
            
            var Actual = mock.Object.MakeAnAPICall(); // A test for the use of the data method (behaviour)

            mock.Verify(r => r.ResponseFromDB(It.IsAny<string>()), Times.Exactly(1), "The RestAPICall should only be called once");
            Assert.AreEqual(Expected, Actual,  TestDesc);
        }
    }
}
