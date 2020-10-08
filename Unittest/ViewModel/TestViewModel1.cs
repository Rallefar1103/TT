using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.ViewModel;

namespace Unittest.ViewModel
{
    [TestFixture]
    public class TestViewModel1
    {
        [TestCase(5, "Testing that the command is properly executed.")]
        public void TestMethod(int Expected, string TestDesc)
        {
            Assert.AreEqual(1, 0, "Not implemented yet");
        }
    }
}
