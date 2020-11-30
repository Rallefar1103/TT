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
            scannedQR.ofType = QRType.Rover;
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
            scannedQR.ofType = QRType.Rover;
            scannedQR.ID = QRID;
            scannedQR.ConfirmedLabelled = true;
            SimCard validSimcard = new SimCard();
            validSimcard.ID = SimcardID;
            validSimcard.Barcode.ICCID = BarcodeICCID;

            // Act
            Assert.ThrowsAsync<ValidationException>(async () => await scannedQR.Preregister(validSimcard), Desc);
        }
        #endregion ConfirmAssemblyAndLabelling
    }
}
