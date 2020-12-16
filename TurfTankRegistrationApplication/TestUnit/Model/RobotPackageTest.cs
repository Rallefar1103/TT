using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Exceptions;

namespace TestUnit.Model
{
    [TestFixture]
    class RobotPackageTest
    {
        public enum ComponentType
        {
            robot,
            roverGPS,
            baseGPS,
            tablet,
            controller,
            qr,
            none
        }

        #region ValidateSelf
        [TestCase("serialNumber", "finalSSID", "finalPassword", SerialOrQR.BothSerialAndQRId, "Should validate when none of its components are null and all of them are valid with both a serial and qr")]
        [TestCase("", "TTAPWifi", "ChangeMe", SerialOrQR.OnlyQRId, "Should validate when none of its components are null and all of them are valid with only a qr")]
        public void RobotPackageValidateSelfTest_ValidRobotPackage_ShouldNotThrow(string serialNumber, string newSSID, string newPassword, SerialOrQR idRestriction, string Desc)
        {
            //Arrange
            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string finalSSID = "finalSSID";
            string finalPassword = "finalPassword";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, finalSSID, finalPassword);
            
            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);
            
            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = newPassword;
            robot.Controller.ActiveSSID = newSSID;

            //Act
            //Assert
            Assert.DoesNotThrow(() => robot.ValidateSelf(idRestriction), Desc);

        }
        
        [TestCase("Should throw an exception when the rover is invalid")]
        public void RobotPackageValidateSelfTest_InvalidRover_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            roverGPS.ID = "";
             
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);

        }

        [TestCase("Should throw an exception when the base is invalid")]
        public void RobotPackageValidateSelfTest_InvalidBase_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            baseGPS.ID = "";
        
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);

        }

        [TestCase("Should throw an exception when the tablet is invalid")]
        public void RobotPackageValidateSelfTest_InvalidTablet_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            tablet.Simcard.ID = "";

            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);

        }

        [TestCase("Should throw an exception when the controller is invalid")]
        public void RobotPackageValidateSelfTest_InvalidController_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            controller.ID = "";
            
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);

        }

        [TestCase("Should throw an exception when the base is missing")]
        public void RobotPackageValidateSelfTest_MissingBase_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            baseGPS = null;
            
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);
        }
        [TestCase("Should throw an exception when the rover is missing")]
        public void RobotPackageValidateSelfTest_MissingRover_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            roverGPS = null;
            
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);
        }

        [TestCase("Should throw an exception when the tablet is missing")]
        public void RobotPackageValidateSelfTest_MissingTablet_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            tablet = null;
            
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);
        }

        [TestCase("Should throw an exception when the controller is missing")]
        public void RobotPackageValidateSelfTest_MissingController_ShouldThrow(string Desc)
        {
            //Arrange

            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            //Will make a components validateSelf throw an exception
            controller = null;
                    
            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);
        }


        [TestCase("" ,"Should throw an exception when the serialnumber is empty")]
        [TestCase(null, "Should throw an exception when the serialnumber is null")]
        [TestCase("different QR ID", "Should throw an exception when the serialnumber is different that the robots QR ID")]
        public void RobotPackageValidateSelfTest_InvalidSerialnumber_ShouldThrow(string newSerial, string Desc)
        {
            //Arrange
            string qrid = "qrid";
            string macEthernet = "macEthernet";
            string macWifi = "macWifi";
            string ssid = "ssid";
            string password = "password";
            string serialNumber = "serialNumber";

            QRSticker roverQR = new QRSticker(qrid, QRType.rover);
            QRSticker baseQR = new QRSticker(qrid, QRType.basestation);
            QRSticker tabletQR = new QRSticker(qrid, QRType.tablet);
            QRSticker robotQR = new QRSticker(qrid, QRType.robot);
            ControllerQRSticker controllerQR = new ControllerQRSticker(QRType.controller, qrid, ssid, password);

            BarcodeSticker barcode = new BarcodeSticker("1234");

            SimCard roverSimcard = new SimCard(barcode, roverQR);
            SimCard tabletSimCard = new SimCard(barcode, tabletQR);
            SimCard baseSimcard = new SimCard(barcode, baseQR);

            Tablet tablet = new Tablet(tabletSimCard);
            GPS roverGPS = new GPS(roverSimcard, serialNumber);
            GPS baseGPS = new GPS(baseSimcard, serialNumber);
            Controller controller = new Controller(serialNumber, controllerQR, macEthernet, macWifi);

            RobotPackage robot = new RobotPackage(controller, tablet, roverGPS, baseGPS, robotQR);
            robot.Controller.ActivePassword = controller.QR.FinalPASSWORD;
            robot.Controller.ActiveSSID = controller.QR.FinalSSID;

            robot.SerialNumber = newSerial;
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => robot.ValidateSelf(), Desc);

        }


        #endregion ValidateSelf
    }
}
