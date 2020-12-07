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
    class SimcardTest
    {
        #region ValidateSelf
        [TestCase("1234", "5678", QRType.BASE, "Tests that simcards does not throw with a qr which is type BASE")]
        [TestCase("1234", "5678", QRType.TABLET, "Tests that simcards does not throw with a qr which is type TABLET")]
        [TestCase("1234", "5678", QRType.ROVER, "Tests that simcards does not throw with a qr which is type ROVER")]
        public void SimcardValidateSelfTest_ValidSimcard_ShouldNotTrow(string iccid, string qrID, QRType qrType, string Desc)
        {
            //Arrange
            QRSticker qr = new QRSticker(qrID, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode, qr);
            //Act
            //Assert
            Assert.DoesNotThrow(() => simCard.ValidateSelf(), Desc);
        }
        [TestCase("1234", "5678", QRType.CONTROLLER, "Tests that simcards throw with a qr which is type CONTROLLER")]
        [TestCase("", "5678", QRType.BASE, "Tests that the validation throws when the id is empty")]
        [TestCase(null, "5678", QRType.BASE, "Tests that the validation throws when the id is null")]
        [TestCase("1234", "", QRType.BASE, "Tests that the validation throws when the qrid is empty")]
        [TestCase("1234", null, QRType.BASE, "Tests that the validation throws when the qrid is null")]
        public void SimcardValidateSelfTest_InvalidSimcard_ShouldTrow(string iccid, string qrID, QRType qrType, string Desc)
        {
            //Arrange
            QRSticker qr = new QRSticker(qrID, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode, qr);
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => simCard.ValidateSelf(), Desc);
        }
        [TestCase("1234", "5678", QRType.BASE, "Tests that simcards throw when the iccid and simcard id doesn't match")]
        public void SimcardValidateSelfTest_SimcardWithDifferentIDs_ShouldTrow(string iccid, string qrID, QRType qrType, string Desc)
        {
            //Arrange
            QRSticker qr = new QRSticker(qrID, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode, qr);
            simCard.ID = "91011";
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => simCard.ValidateSelf(), Desc);
        }
        [TestCase("1234", "Tests that simcards throw when the iccid and simcard id doesn't match")]
        public void SimcardValidateSelfTest_SimcardMissingQR_ShouldTrow(string iccid, string Desc)
        {
            //Arrange
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode);
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => simCard.ValidateSelf(), Desc);
        }

        #endregion ValidateSelf
    }
}
