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
    class QRStickerTest
    {
        #region ConfirmAssemblyAndLabelling
        [TestCase("Test that the function validates and saves a rover")]
        public async Task ConfirmAssemblyAndLabeling_QrWithRoverType_ShouldValidateAndSaveRover(string Desc)
        {
            // Mock
            var mockDBAPI = Substitute.For<IRegistrationDBAPI<GPS>>();
            mockDBAPI.Save(Arg.Any<GPS>()).Returns(true);
            GPS.API = mockDBAPI;

            // Arrange
            QRSticker scannedQR = new QRSticker();
            scannedQR.OfType = QRType.ROVER;
            scannedQR.ID = "QRStickerNumber1";
            scannedQR.ConfirmedLabelled = true;
            SimCard validSimcard = new SimCard();
            validSimcard.ID = "SerialFromBarcode";
            validSimcard.Barcode.ICCID = "SerialFromBarcode";

            // Act
            bool Actual = await scannedQR.Preregister(validSimcard);

            // Assert
            await mockDBAPI.Received().Save(Arg.Any<GPS>());
            Assert.AreEqual(true, Actual, Desc);
        }
        [TestCase("Should throw when Simcard ID and ICCID isn't equal", "SomeQRId", "SomeSimcardId", "SomeBarcodeICCID")]
        [TestCase("Should throw when QR ID isn't set", "", "SimIdAndBarICCID", "SimIdAndBarICCID")]
        [TestCase("Should throw when Simcard ID isn't set", "SomeQRId", "", "SimIdAndBarICCID")]
        [TestCase("Should throw when Barcode ICCID isn't set", "SomeQRId", "SimIdAndBarICCID", "")]
        public void ConfirmAssemblyAndLabeling_RoverWithInvalidQr_ShouldThrowInvalidQRException(string Desc, string QRID, string SimcardID, string BarcodeICCID)
        {
            // Mock
            var mockDBAPI = Substitute.For<IRegistrationDBAPI<GPS>>();
            mockDBAPI.Save(Arg.Any<GPS>()).Returns(true);
            GPS.API = mockDBAPI;

            // Arrange
            QRSticker scannedQR = new QRSticker();
            scannedQR.OfType = QRType.ROVER;
            scannedQR.ID = QRID;
            scannedQR.ConfirmedLabelled = true;
            SimCard validSimcard = new SimCard();
            validSimcard.ID = SimcardID;
            validSimcard.Barcode.ICCID = BarcodeICCID;

            // Act
            Assert.ThrowsAsync<ValidationException>(async () => await scannedQR.Preregister(validSimcard), Desc);
        }
        #endregion ConfirmAssemblyAndLabelling
        #region ScanResultValidation

        [TestCase("TYPE:CONTROLLER QRID:1234 SSID:TTAP1234 PASSWORD:TTIO1234", "Tests a valid QR string returns true")]
        [TestCase("TYPE:CONTROLLER QRID:1 SSID:TTAP1 PASSWORD:TTIO1", "Tests a QR with a short id, ssid and password is still valid")]
        [TestCase("Type:Controller qrid:1 sSiD:ttAp1 passWORD:TtIO1", "Tests that scanned qrs ar case insensitive")]
        public void ControllerQRSTickerScanResultValidation_QRWithvalidScanResults_ShouldBeValid(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.DoesNotThrow(() => new ControllerQRSticker(scanresult), Desc);
        }

        [TestCase("Typ:controller QRID:1234 SSID:TTAP1234 PASSWORD:TTIO1234", "Tests that an exception is thrown when an identifier is misspelled")]
        [TestCase("Type:control QRID:1234 SSID:TTAP1234 PASSWORD:TTIO1234", "Tests that an exception is thrown when the type is misspelled")]
        [TestCase("Type:controller QRID:12aa34 SSID:TTAP1234 PASSWORD:TTIO1234", "Tests that an exception is thrown when there are letters in the QRID")]
        [TestCase("Type:controller QRID: SSID:TTAP1234 PASSWORD:TTIO1234", "Tests that an exception is thrown when there is no QRID")]
        [TestCase("Type:controller QRID:1234 SSID:TTA1234 PASSWORD:TTIO1234", "Tests that an exception is thrown when the ssid does not start with 'TTAP'")]
        [TestCase("Type:controller QRID:1234 SSID:TTAP12a34 PASSWORD:TTIO1234", "Tests that an exception is thrown when there are other letters than 'TTAP' in the SSID")]
        [TestCase("Type:controller QRID:1234 SSID:TTAP PASSWORD:TTIO1234", "Tests that an exception is thrown when there are no numbers after 'TTAP'")]
        [TestCase("Type:controller QRID:1234 SSID:TTAP1234 PASSWORD:TTI1234", "Tests that an exception is thrown when the password does not start with 'TTIO'")]
        [TestCase("Type:controller QRID:1234 SSID:TTAP1234 PASSWORD:TTIO1a234", "Tests that an exception is thrown when there are other letters than 'TTIO' in the password")]
        [TestCase("Type:controller QRID:1234 SSID:TTAP1234 PASSWORD:TTIO", "Tests that an exception is thrown when there are no numbers after 'TTIO'")]
        public void ControllerQRSTickerScanResultValidation_QRWithInvalidScanResults_ShouldThrowValidationException(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => new ControllerQRSticker(scanresult), Desc);
        }

        [TestCase("TYPE:CONTROLLER QRID:1234 SSID:TTAP1234 PASSWORD:TTIO1234", "Tests that values are correctly added to the ControllerQRSticker")]
        public void ControllerQRSTickerScanResultValidation_QRData(string scanresult, string Desc)
        {
            //Arrange
            var controllerQRSticker = new ControllerQRSticker(scanresult);
            //Act
            //Assert
            Assert.AreEqual("1234", controllerQRSticker.ID);
            Assert.AreEqual(QRType.CONTROLLER, controllerQRSticker.OfType);
            Assert.AreEqual("TTAP1234", controllerQRSticker.FinalSSID);
            Assert.AreEqual("TTIO1234", controllerQRSticker.FinalPASSWORD, Desc);
        }

        [TestCase("TYPE:ROBOTPACKAGE QRID:1234", "Tests a valid QR string returns true")]
        [TestCase("TYPE:ROBOTPACKAGE QRID:1", "Tests a QR with a short id, ssid and password is still valid")]
        [TestCase("Type:rOBOTpaCkAGE qrId:1", "Tests that scanned qrs ar case insensitive")]
        [TestCase("TYPE:BASE QRID:1234", "Tests that a base is a valid type")]
        [TestCase("TYPE:ROVER QRID:1234", "Tests that a rover is a valid type")]
        [TestCase("TYPE:TABLET QRID:1234", "Tests that a tablet is a valid type")]
        public void QRSTickerScanResultValidation_QRWithvalidScanResults_ShouldBeValid(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.DoesNotThrow(() => new QRSticker(scanresult), Desc);
        }
        [TestCase("Typ:ROBOTPACKAGE QRID:1234", "Tests that an exception is thrown when an identifier is misspelled")]
        [TestCase("Type:ROBOTPACKAG QRID:1234", "Tests that an exception is thrown when the type is misspelled")]
        [TestCase("Type:ROBOTPACKAGE QRID:12aa34", "Tests that an exception is thrown when there are letters in the QRID")]
        [TestCase("Type:ROBOTPACKAGE QRID:", "Tests that an exception is thrown when there is no QRID")]
        [TestCase("TYPE:CONTROLLER QRID:1234", "Tests that an exception is thrown if you try to create a regular qr with Controller type")]
        public void QRSTickerScanResultValidation_QRWithInvalidScanResults_ShouldThrowValidationException(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => new QRSticker(scanresult), Desc);
        }

        [TestCase("TYPE:ROBOTPACKAGE QRID:1234", "Tests that values are correctly added to the QRSticker")]
        public void QRSTickerScanResultValidation_QRData(string scanresult, string Desc)
        {
            //Arrange
            var qrSticker = new QRSticker(scanresult);
            //Act
            //Assert
            Assert.AreEqual("1234", qrSticker.ID);
            Assert.AreEqual(QRType.ROBOTPACKAGE, qrSticker.OfType, Desc);
        }
        #endregion

    }
}
