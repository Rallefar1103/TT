using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
        [TestCase(8, "8", "Testing that the command is properly executed.")]
        public void TestPressForFiveCommand_FiveVarAttribute_ShouldBeSetToShowFiveAsString(int Arranged, string Expected, string TestDesc)
        {
            Mock<Class1> mock = new Mock<Class1>();
            mock.Object.FiveVar = Arranged;
            ViewModel1 vm = new ViewModel1();
            vm.AFiveObject = mock.Object;

            vm.PressForFiveCommand.Execute(mock.Object);
            string Actual = vm.ShowFive;

            Assert.AreEqual(Expected, Actual, TestDesc);
        }
    }
}
