using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TestIntegration.AndroidTest
{
    [TestFixture(Platform.Android)]
    public class LoginTest
    {
        IApp app;
        Platform platform;

        public LoginTest(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest() // Makes it so that an emulator is initialised at the beginning of each test.
        {
            app = AppInitializer.StartApp(platform);
        }

        [TestCase()]
        public void TestPressForFiveButton_PressButton_ShouldCreateAFiveString()
        {
            // Arrange
            AppResult[] before_results = app.Query(c => c.Marked("???"));
            app.Screenshot("Before Tap");

            // Act
            app.Tap(c => c.Button("PressForFive"));
            AppResult[] after_results = app.Query(c => c.Marked("ShowFiveLabel"));
            app.Screenshot("After Tap");

            //Assert
            Assert.IsTrue(before_results != after_results);
            Assert.AreEqual("5", after_results[0].Text);
        }
    }
}