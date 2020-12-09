    using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using TurfTankRegistrationApplication.Connection;
using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Exceptions;

namespace TestUnit.Model
{
    [TestFixture]
    public class ControllerTest
    {

        #region ValidateSelf
        [TestCase(
            QRType.CONTROLLER, "SomeQRID", "SomeActiveSSID", "SomeActivePassword",
             "SomeMacEthernet", "SomeMacWifi", 
            "Should validate when when the controller only has a qr, macEthernet and macWifi when validate self is told it only has qr")]
        public void ValidateSelf_ValidControllerOnlyQR_ShouldValidate(
            QRType qrType, string QRID, string activeSSID, string activePassword,
            string macEthernet, string macWifi, string Desc)
        {
            // Arrange
            ControllerQRSticker scannedQR = new ControllerQRSticker(qrType, QRID, activeSSID, activePassword);
            Controller validController = new Controller(scannedQR, macEthernet, macWifi);

            // Act & Assert
            Assert.DoesNotThrow(() => validController.ValidateSelf(SerialOrQR.OnlyQRId), Desc);
        }
        [TestCase(
            QRType.CONTROLLER, "SomeQRID", "SomeActiveSSID", "SomeActivePassword",
             "SomeSerialNumber", "SomeMacEthernet", "SomeMacWifi", 
            "Should validate when all attributes have been entered, and the password and ssid mathces that on the QR, when it has both serial and QR")]
        public void ValidateSelf_ValidControllerQRAndSerial_ShouldValidate(
            QRType qrType, string QRID, string finalSSID, string finalPassword,
            string serialNumber, string macEthernet, string macWifi, string Desc)
        {
            // Arrange
            ControllerQRSticker scannedQR = new ControllerQRSticker(qrType, QRID, finalSSID, finalPassword);
            Controller validController = new Controller(serialNumber ,scannedQR, macEthernet, macWifi);
            validController.ActivePassword = finalPassword;
            validController.ActiveSSID = finalSSID;

            // Act & Assert
            Assert.DoesNotThrow(() => validController.ValidateSelf(SerialOrQR.BothSerialAndQRId), Desc);
        }

        [TestCase(QRType.BASE, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR type is not controller")]
        [TestCase(QRType.CONTROLLER, "", "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR ID is empty")]
        [TestCase(QRType.CONTROLLER, null, "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR ID is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when serial number is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", null, "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when serial number is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when active SSID is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", null, "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when active SSID is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", "", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when active password is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", null, "SomeMacEthernet", "SomeMacWifi", "Should throw exception when active password is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", "", "SomeMacWifi", "Should throw exception when mac ethernet is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", null, "SomeMacWifi", "Should throw exception when mac ethernet is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "", "Should throw exception when mac wifi is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "SomeSerialNumber", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", null, "Should throw exception when mac wifi is null")]
        public void ValidateSelf_InvalidControllerQRAndSerial_ShouldThrowException(QRType qrType, string qrId, string serialNumber, string finalSSID, string finalPassword, string macEthernet, string macWifi, string Desc)
        {
            // Arrange
            ControllerQRSticker scannedQR = new ControllerQRSticker(qrType, qrId, finalSSID, finalPassword);
            Controller validController = new Controller(serialNumber, scannedQR, macEthernet, macWifi);
            validController.ActivePassword = finalPassword;
            validController.ActiveSSID = finalSSID;

            // Act & Assert
            Assert.Throws<ValidationException>(() => validController.ValidateSelf(SerialOrQR.BothSerialAndQRId), Desc);
        }

        [TestCase(QRType.BASE, "SomeQRID", "", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR type is not controller")]
        [TestCase(QRType.CONTROLLER, "", "", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR ID is empty")]
        [TestCase(QRType.CONTROLLER, null, "", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR ID is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "someSerialNumber", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "SomeMacWifi", "Should throw exception when QR ID is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "", "SomeActiveSSID", "SomeActivePassword", "", "SomeMacWifi", "Should throw exception when mac ethernet is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "", "SomeActiveSSID", "SomeActivePassword", null, "SomeMacWifi", "Should throw exception when mac ethernet is null")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", "", "Should throw exception when mac wifi is empty")]
        [TestCase(QRType.CONTROLLER, "SomeQRID", "", "SomeActiveSSID", "SomeActivePassword", "SomeMacEthernet", null, "Should throw exception when mac wifi is null")]
        public void ValidateSelf_InvalidControllerQROnly_ShouldThrowException(QRType qrType, string qrId, string serialNumber, string activeSSID, string activePassword, string macEthernet, string macWifi, string Desc)
        {
            // Arrange
            ControllerQRSticker scannedQR = new ControllerQRSticker(qrType, qrId, activeSSID, activePassword);
            Controller validController = new Controller(serialNumber, scannedQR, macEthernet, macWifi);

            // Act & Assert
            Assert.Throws<ValidationException>(() => validController.ValidateSelf(SerialOrQR.OnlyQRId), Desc);
        }


        [TestCase("someActiveSSID", "someActivePassword", "Should throw exception when active SSID is empty")]
        [TestCase("someActiveSSID", "someActivePassword", "Should throw exception when active SSID is null")]
        [TestCase("someActiveSSID", "someActivePassword", "Should throw exception when active password is empty")]
        [TestCase("someActiveSSID", "someActivePassword", "Should throw exception when active password is null")]
        public void ValidateSelf_noSSIDOrPasswordQROnly_ShouldThrowException(string newSSID, string newPassword, string Desc)
        {
            // Arrange
            QRType qrType = QRType.CONTROLLER;
            string qrId = "someQRID";
            string activeSSID = "someActiveSSID";
            string activePassword = "someActivePassword";
            string macEthernet = "someMacEthernet";
            string macWifi = "someMacWifi";

            ControllerQRSticker scannedQR = new ControllerQRSticker(qrType, qrId, activeSSID, activePassword);
            Controller Controller = new Controller(scannedQR, macEthernet, macWifi);
            Controller.ActiveSSID = newSSID;
            Controller.ActivePassword = newPassword;

            // Act & Assert
            Assert.Throws<ValidationException>(() => Controller.ValidateSelf(SerialOrQR.OnlyQRId), Desc);
        }
        [TestCase(
            QRType.CONTROLLER, "SomeQRID","SomeSSID", "SomePassword", 
            "SomeNewControllerID",  "SomeSerialNumber", "SomeMacEthernet", 
            "SomeMacWifi", "Should throw and exception if the  QR ID does not match controller ID")]
        public void ValidateSelf_InvalidControllerIDAndQRID_ShouldThrowException(
            QRType qrType, string qrId, string ActiveSSID, string ActivePassword, 
            string newControllerID, string serialNumber, string macEthernet, string macWifi, string Desc)
        {
            // Arrange
            ControllerQRSticker qr = new ControllerQRSticker(qrType, qrId, ActiveSSID, ActivePassword);
            Controller validController = new Controller(serialNumber, qr, macEthernet, macWifi);
            validController.ID = newControllerID;

            // Act & Assert
            Assert.Throws<ValidationException>(() => validController.ValidateSelf(SerialOrQR.BothSerialAndQRId), Desc);
        }

        [TestCase(
            QRType.CONTROLLER, "SomeQRID", "SomeSSID", "SomePassword", 
            "SomeNewControllerPassword", "SomeSerialNumber", "SomeMacEthernet", "SomeMacWifi", 
            "Should throw and exception if the  QR ID does not match controller ID")]
        public void ValidateSelf_InvalidControllerPasswordAndQRPassword_ShouldThrowException(
            QRType qrType, string qrId, string ActiveSSID, string ActivePassword, 
            string newControllerPassword, string serialNumber, string macEthernet, string macWifi, string Desc)
        {
            // Arrange
            ControllerQRSticker qr = new ControllerQRSticker(qrType, qrId, ActiveSSID, ActivePassword);
            Controller validController = new Controller(serialNumber, qr, macEthernet, macWifi);
            validController.ActivePassword = newControllerPassword;

            // Act & Assert
            Assert.Throws<ValidationException>(() => validController.ValidateSelf(SerialOrQR.BothSerialAndQRId), Desc);
        }

        [TestCase("SomeNewSSID", "ChangeMe", "", "Should throw and exception if the ssid is not equal to TTAP followed by the last for digits of the macWifi")]
        [TestCase("TTAP2345", "SomeotherPassword", "", "Should throw and exception if the password is not the inital password")]
        [TestCase("TTAP2345", "ChangeMe", "SomeSerialNumber", "Should throw and exception when there is a non empty serial number")]
        public void ValidateSelf_InvalidControllerOnlyQR_ShouldThrowException(
            string newControllerSSID, string newControllerPassword, string serialNumber, string Desc)
        {
            // Arrange
            QRType qrType = QRType.CONTROLLER;
            string qrId = "SomeQRID";
            string activeSSID = "SomeActiveSSID";
            string activePassword = "SomePassword";
            string macEthernet = "someMacEthernet";
            string macWifi = "12345";

            ControllerQRSticker qr = new ControllerQRSticker(qrType, qrId, activeSSID, activePassword);
            Controller validController = new Controller(qr, macEthernet, macWifi);
            validController.ActiveSSID = newControllerSSID;
            validController.ActivePassword = newControllerPassword;
            validController.SerialNumber = serialNumber;

            // Act & Assert
            Assert.Throws<ValidationException>(() => validController.ValidateSelf(SerialOrQR.OnlyQRId), Desc);



        }

        [TestCase("", "QRSSID", "QRPassword", "Should throw and exception if the serialnumber is empty")]
        [TestCase("SomeSerialNumber", "someOtherSSID", "QRPassword", "Should throw and exception if the ssid is different from the qr ssid")]
        [TestCase("SomeSerialNumber", "QRSSID", "SomeOtherPassword", "Should throw and exception if the Password is different from the qr password")]
        public void ValidateSelf_InvalidControllerSerialAndQR_ShouldThrowException(
            string serialNumber, string newControllerSSID, string newControllerPassword , string Desc)
        {
            // Arrange
            QRType qrType = QRType.CONTROLLER;
            string qrId = "SomeQRID";
            string activeSSID = "QRSSID";
            string activePassword = "QRPassword";
            string macEthernet = "someMacEthernet";
            string macWifi = "12345";

            ControllerQRSticker qr = new ControllerQRSticker(qrType, qrId, activeSSID, activePassword);
            Controller validController = new Controller(qr, macEthernet, macWifi);
            validController.ActiveSSID = newControllerSSID;
            validController.ActivePassword = newControllerPassword;
            validController.SerialNumber = serialNumber;

            // Act & Assert
            Assert.Throws<ValidationException>(() => validController.ValidateSelf(SerialOrQR.BothSerialAndQRId), Desc);
        }

        #endregion ValidateSelf

    }
}
