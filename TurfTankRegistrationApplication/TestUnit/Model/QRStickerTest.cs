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
            var mockDBAPI = Substitute.For<IDBAPI<GPS>>();
            mockDBAPI.Save(Arg.Any<GPS>()).Returns(true);
            GPS.API = mockDBAPI;

            // Arrange
            QRSticker scannedQR = new QRSticker();
            scannedQR.OfType = QRType.rover;
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
            var mockDBAPI = Substitute.For<IDBAPI<GPS>>();
            mockDBAPI.Save(Arg.Any<GPS>()).Returns(true);
            GPS.API = mockDBAPI;

            // Arrange
            QRSticker scannedQR = new QRSticker();
            scannedQR.OfType = QRType.rover;
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

        [TestCase("CONTROLLER|123|TTAP1234|TTIO1234", "Tests a valid QR string returns true")]
        [TestCase("CONTROLLER|1|TTAP1|TTIO1", "Tests a QR with a short id, ssid and password is still valid")]
        public void ControllerQRSTickerScanResultValidation_QRWithvalidScanResults_ShouldBeValid(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.DoesNotThrow(() => new ControllerQRSticker(scanresult), Desc);
        }

        [TestCase("control|1234|TTAP1234|TTIO1234", "Tests that an exception is thrown when the type is misspelled")]       
        [TestCase("controller||TTAP1234|TTIO1234", "Tests that an exception is thrown when there is no QRID")]
        [TestCase("controller|1234|TTA1234|TTIO1234", "Tests that an exception is thrown when the ssid does not start with 'TTAP'")]
        [TestCase("controller|1234|TTAP12a34|TTIO1234", "Tests that an exception is thrown when there are other letters than 'TTAP' in the SSID")]
        [TestCase("controller|1234|TTAP|TTIO1234", "Tests that an exception is thrown when there are no numbers after 'TTAP'")]
        [TestCase("controller|1234|TTAP1234|TTI1234", "Tests that an exception is thrown when the password does not start with 'TTIO'")]
        [TestCase("controller|1234|TTAP1234|TTIO1a234", "Tests that an exception is thrown when there are other letters than 'TTIO' in the password")]
        [TestCase("controller|1234|TTAP1234|TTIO", "Tests that an exception is thrown when there are no numbers after 'TTIO'")]
        public void ControllerQRSTickerScanResultValidation_QRWithInvalidScanResults_ShouldThrowValidationException(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => new ControllerQRSticker(scanresult), Desc);
        }

        [TestCase("CONTROLLER|1234|TTAP1234|TTIO1234", "Tests that values are correctly added to the ControllerQRSticker")]
        public void ControllerQRSTickerScanResultValidation_QRData(string scanresult, string Desc)
        {
            //Arrange
            var controllerQRSticker = new ControllerQRSticker(scanresult);
            //Act
            //Assert
            Assert.AreEqual("CONTROLLER|1234", controllerQRSticker.ID);
            Assert.AreEqual(QRType.controller, controllerQRSticker.OfType);
            Assert.AreEqual("TTAP1234", controllerQRSticker.FinalSSID);
            Assert.AreEqual("TTIO1234", controllerQRSticker.FinalPASSWORD, Desc);
        }

        [TestCase("ROBOTPACKAGE|1234", "Tests a valid QR string returns true")]
        [TestCase("ROBOTPACKAGE|1", "Tests a QR with a short id, ssid and password is still valid")]
        [TestCase("rOBOTpaCkAGE|1", "Tests that scanned qrs ar case insensitive")]
        [TestCase("BASESTATION|1234", "Tests that a base is a valid type")]
        [TestCase("ROVER|1234", "Tests that a rover is a valid type")]
        [TestCase("TABLET|1234", "Tests that a tablet is a valid type")]
        public void QRSTickerScanResultValidation_QRWithvalidScanResults_ShouldBeValid(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.DoesNotThrow(() => new QRSticker(scanresult), Desc);
        }
        
        [TestCase("ROBOTPACKAG|1234", "Tests that an exception is thrown when the type is misspelled")]
        [TestCase("ROBOTPACKAGE|", "Tests that an exception is thrown when there is no QRID")]
        [TestCase("CONTROLLER|1234", "Tests that an exception is thrown if you try to create a regular qr with Controller type")]
        public void QRSTickerScanResultValidation_QRWithInvalidScanResults_ShouldThrowValidationException(string scanresult, string Desc)
        {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => new QRSticker(scanresult), Desc);
        }

        // Remember to update test below
        //[TestCase("ROBOTPACKAGE|1234", "Tests that values are correctly added to the QRSticker")]
        //public void QRSTickerScanResultValidation_QRData(string scanresult, string Desc)
        //{
        //    //Arrange
        //    var qrSticker = new QRSticker(scanresult);
        //    //Act
        //    //Assert
        //    Assert.AreEqual("1234", qrSticker.ID);
        //    Assert.AreEqual(QRType.ROBOTPACKAGE, qrSticker.OfType, Desc);
        //}
        #endregion

    }
}
