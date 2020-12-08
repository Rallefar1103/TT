using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;
using TestUnit.Connection;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace TestUnit.ViewModel
{
    [TestFixture]
    public class RoverRegVMTest
    {
        [TestCase("Testing that the GetRoverSerialNumber actually returns a parsed string of the response")]
        public async Task GetRoverSerialNumber_NoParams_ShouldReturnString(string desc)
        {
            // Arrange
            Dictionary<string, string> json = new Dictionary<string, string>
            {
                { "command", "SOSVER,0" },
                { "response", "$SOSVER,0,SMARTOS 1.3.5 EN,0917W - SRTK0128 * 0B\n" }

            };

            string response = JsonSerializer.Serialize(json);

            var mockNavigation = Substitute.For<INavigation>();
            var mockMessageHandler = new MockHttpMessageHandler(response, HttpStatusCode.OK);
            var mockHttpClient = new HttpClient(mockMessageHandler);

            string expected = "0917W - SRTK0128";


            RoverRegistrationViewModel testObject = new RoverRegistrationViewModel(mockNavigation, mockHttpClient);

            // Act
            await testObject.GetRoverSerialNumber();
            string actual = testObject.RoverResponse;

            // Assert
            Assert.AreEqual(expected, actual, desc);
            await mockNavigation.Received().PopAsync();
        }
    }
}
