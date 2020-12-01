using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using System.Text.Json;
using System.Net;
using System.Net.Http;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Model;

namespace TestUnit.Connection
{
    [TestFixture]
    class RegistrationDBAPITest
    {
        #region GetById

        #region Testing that it works

        // Maybe redo tests to more easily make testcases, ie. set som values as null
        // Then there is only a need for a single Test pr. Component

        [TestCase("Some Serial", "Some Controller Id", "Some SSID", "Some Password", "Testing the method returns a full Robot Package")]
        public async Task GetById_RobotPackage_ShouldReturnRobotPackage(string SN, string ContSN, string SSID, string PW, string Desc)
        {
            // Arrange Robot In Database and mock HttpClient
            RobotItem schema = Initializer.ValidRobotItem();
            schema.Id = SN;
            schema.Controller.SerialNumber = ContSN;
            schema.Controller.Ssid = SSID;
            schema.Controller.SsidPassword = PW;

            var statuscodeInResponse = HttpStatusCode.OK;
            string responseFromDB = JsonSerializer.Serialize<RobotItem>(schema);

            var mockMessageHandler = new MockHttpMessageHandler(responseFromDB, statuscodeInResponse);
            var mockHttpClient = new HttpClient(mockMessageHandler);
            RegistrationDBAPI<RobotPackage> testRegistrationDBAPI = new RegistrationDBAPI<RobotPackage>(new Client(mockHttpClient));

            // Act
            RobotPackage RobotTest = await testRegistrationDBAPI.GetById(SN);

            // Assert that HttpClient was called and that the Robots are identical
            Assert.AreEqual(1, mockMessageHandler.NumberOfCalls, "Testing that the HttpClient mock was called only ones");
            Assert.AreEqual(schema.Id, RobotTest.SerialNumber, Desc);
            Assert.AreEqual(schema.Controller.SerialNumber, RobotTest.Controller.ID, Desc);
            Assert.AreEqual(schema.Controller.Ssid, RobotTest.Controller.ActiveSSID, Desc);
            Assert.AreEqual(schema.Controller.SsidPassword, RobotTest.Controller.ActivePassword, Desc);
        }

        [TestCase("Some Serial", "Some Controller Id", "Testing the method returns a partial Robot Package with whatever data is on the database")]
        public async Task GetById_PartialRobotPackage_ShouldReturnPartialRobotPackage(string SN, string ContSN, string Desc)
        {
            // Arrange Robot In Database and mock HttpClient
            RobotItem schema = Initializer.ValidRobotItem();
            schema.Id = SN;
            schema.Controller.SerialNumber = ContSN;

            var statuscodeInResponse = HttpStatusCode.OK;
            string responseFromDB = JsonSerializer.Serialize<RobotItem>(schema);

            var mockMessageHandler = new MockHttpMessageHandler(responseFromDB, statuscodeInResponse);
            var mockHttpClient = new HttpClient(mockMessageHandler);
            RegistrationDBAPI<RobotPackage> testRegistrationDBAPI = new RegistrationDBAPI<RobotPackage>(new Client(mockHttpClient));

            // Act
            RobotPackage RobotTest = await testRegistrationDBAPI.GetById(SN);

            // Assert that HttpClient was called and that the Robots are identical
            Assert.AreEqual(1, mockMessageHandler.NumberOfCalls, "Testing that the HttpClient mock was called only ones");
            Assert.AreEqual(schema.Id, RobotTest.SerialNumber, Desc);
            Assert.AreEqual(schema.Controller.SerialNumber, RobotTest.Controller.ID, Desc);
            Assert.AreEqual("", RobotTest.Controller.ActiveSSID, Desc);
            Assert.AreEqual("", RobotTest.Controller.ActivePassword, Desc);
        }

        // Add Tests for Controller, GPS, Tablet and Simcard

        #endregion Testing that it works

        #region Testing that it fails correctly

        [TestCase("Testing that it catches unauthorised access")]
        public void GetById_NotAuthorized_ShouldRaiseException(string Desc)
        {
            // Arrange Robot In Database and mock HttpClient
            RobotItem schema = Initializer.ValidRobotItem();
            schema.Id = "SerialNumber";

            var statuscodeInResponse = HttpStatusCode.Unauthorized;
            string responseFromDB = JsonSerializer.Serialize<RobotItem>(schema);

            var mockMessageHandler = new MockHttpMessageHandler(responseFromDB, statuscodeInResponse);
            var mockHttpClient = new HttpClient(mockMessageHandler);
            RegistrationDBAPI<RobotPackage> testRegistrationDBAPI = new RegistrationDBAPI<RobotPackage>(new Client(mockHttpClient));

            // Act & Assert (since throws needs to take the acting object)
            Assert.ThrowsAsync<ApiException>(async () => await testRegistrationDBAPI.GetById(schema.Id));
        }

        [TestCase("Testing that it catches forbidden access")]
        public void GetById_Forbidden_ShouldRaiseException(string Desc)
        {
            // Arrange Robot In Database and mock HttpClient
            RobotItem schema = Initializer.ValidRobotItem();
            schema.Id = "SerialNumber";

            var statuscodeInResponse = HttpStatusCode.Forbidden;
            string responseFromDB = JsonSerializer.Serialize<RobotItem>(schema);

            var mockMessageHandler = new MockHttpMessageHandler(responseFromDB, statuscodeInResponse);
            var mockHttpClient = new HttpClient(mockMessageHandler);
            RegistrationDBAPI<RobotPackage> testRegistrationDBAPI = new RegistrationDBAPI<RobotPackage>(new Client(mockHttpClient));

            // Act & Assert (since throws needs to take the acting object)
            Assert.ThrowsAsync<ApiException>(async () => await testRegistrationDBAPI.GetById(schema.Id));
        }

        // Test that it catches a lack of connection correctly maybe? Which error code is that? 404? 503?

        #endregion Testing that it fails correctly

        #endregion GetById

        #region GetListOfObjects

        // To later, since we don't yet have any use of this functionality

        #endregion GetListOfObjects

        #region Save

        #region Testing that it works

        #endregion Testing that it works

        #region Testing that it fails correctly

        #endregion Testing that it fails correctly

        #endregion Save
    }
}