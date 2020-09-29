using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Project;

namespace Unittests
{
    [TestFixture]
    class MenuTest
    {
        [TestCase("Testing for adding one MenuItem")]
        public void Add_SingleValue_AddsMenuItemToMenu(string TestName)
        {
            Menu StubMenu = new Menu("Test Title");
            MenuItem ArrangedMenuItem = new MenuItem("Test Title", "Test Content");
            MenuItem ExpectedMenuItem = ArrangedMenuItem;

            StubMenu.AddItem(ArrangedMenuItem);

            Assert.AreEqual(
            ExpectedMenuItem, StubMenu.MenuList[0],
            TestName
            );
        }
        [TestCase()]
        public void Start_SingleCommand_MustBeAbleToNavigateConsole(string TestName)
        {

        }
    }
}
