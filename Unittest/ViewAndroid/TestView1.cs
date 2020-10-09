using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

using TurfTankRegistrationApplication.ViewModel;

namespace Unittest.ViewAndroid
{
    [TestFixture(Platform.Android)]
    public class TestView1
    {
        IApp app;
        Platform platform;

        public TestView1(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest() // Makes it so that an emulator is initialised at the beginning of each test.
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestPressForFiveButton_PressButton_ShouldCreateAFiveString()
        {
            // Arrange
            app.Screenshot("Before Tap");
            AppResult[] before_results = app.Query(c => c.Marked("ShowFiveLabel"));
            // In some way, if we even want/need it, the ViewModel Command should be mocked out to return the value we want.

            // Act
            app.Tap(c => c.Button("PressForFive"));
            AppResult[] after_results = app.Query(c => c.Marked("ShowFiveLabel"));
            app.Screenshot("After Tap");

            //Assert
            Assert.IsTrue(before_results != after_results);
            Assert.AreEqual("3", after_results[0].Text); // Here I am cheating and providing the right, wrong value ;-)
        }
    }
}
