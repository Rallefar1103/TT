using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Text.Json;
using System.Net;
using System.Net.Http;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Connection;

namespace TestIntegration.Connection
{
    [TestFixture]
    class RegistrationDBAPISwaggerTest
    {
        DBAPI<RobotPackage> RobotAPI { get; set; }


        [SetUp]
        public void SetupAPIs()
        {
            RobotAPI = new DBAPI<RobotPackage>();

            RobotAPI.ApiClientRef = new Client(new HttpClient());
        }

        [TestCase("Should return a valid RobotPackage")]
        public async Task GetById_IdOnDatabase_ShouldGiveRobotPackage(string Desc)
        {
            string IdWhichExistsOnDatabase = "13f627a7-d6e8-46a8-898a-36853a1e1481";

            RobotPackage robot = await RobotAPI.GetById(IdWhichExistsOnDatabase);

            Assert.AreEqual(IdWhichExistsOnDatabase, robot.SerialNumber, Desc);
        }
    }
}
