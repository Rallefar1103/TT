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
using TurfTankRegistrationApplication.ViewModel;
using Xamarin.Forms;

namespace TestUnit.ViewModel
{
    [TestFixture]
    class PreRegistrationViewModelTest
    {
        #region PreRegisterComponent

        /*
        [TestCase()]
        public void PreregisterComponent_ValidRoverQRAndBarcode_ShouldCreateRoverWithSimcardAndQRAttached()
        {
            // Mock Navigation
            var mockNavigation = Substitute.For<INavigation>();
            mockNavigation.PopAsync();

            // Mock QR to remove dependency of the Preregister method
            var scannedQR = Substitute.For<IQRSticker>();
            scannedQR.Data.Returns("Rover: SomeSerial");
            scannedQR.ID.Returns("SomeSerial");
            scannedQR.ofType.Returns(QRType.Rover);
            scannedQR.ConfirmedLabelled.Returns(true);
            scannedQR.Preregister(Arg.Any<SimCard>()).Returns(true);

            // Arrange
            var viewModel = new PreRegistrationViewModel(mockNavigation);
            viewModel.QR = scannedQR;

            BarcodeSticker scannedBarcode = new BarcodeSticker();
            scannedBarcode.Data = "SomeBarcodeSerial";
            scannedBarcode.ICCID = "SomeBarcodeSerial";
            viewModel.Barcode = scannedBarcode;

            // Act
            Assert.DoesNotThrow(() => viewModel.PreregisterComponentCommand.Execute(new object()));

            // Assert
            scannedQR.Received().Preregister(Arg.Any<SimCard>());
            mockNavigation.Received().PopAsync();
        }

        [TestCase()]
        public void PreregisterComponent_InvalidQRException_ShouldCatchAndDisplayRelevantErrorMessage()
        {
            // Mock


            // Arrange


            // Act


            // Assert
            Assert.Fail();
        }
        */

        #endregion PreRegisterComponent
    }
}
