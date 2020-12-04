using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.Exceptions;


namespace TestUnit.Model
{
    [TestFixture]
    class TabletTest
    {
        #region ValidateSelf
        [TestCase("1234", "5678", QRType.TABLET, "Tests that tablets does not throw with a simcard qr which is type TABLET")]
        public void TabletValidateSelfTest_ValidTablets_ShouldNotTrow(string iccid, string qrid, QRType qrType, string Desc)
        {
            //Arrange
            QRSticker qr = new QRSticker(qrid, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode, qr);
            Tablet tablet = new Tablet(simCard);
            //Act
            //Assert
            Assert.DoesNotThrow(() => tablet.ValidateSelf(), Desc);
        }

        [TestCase("1234", "5678", QRType.ROBOTPACKAGE, "Tests that tablets throw with a simcard qr which is of a type that is not TABLET")]
        [TestCase("", "5678", QRType.TABLET, "Tests that the validation throws when the id is empty")]
        [TestCase(null, "5678", QRType.TABLET, "Tests that the validation throws when the id is null")]
        [TestCase("1234", "", QRType.TABLET, "Tests that the validation throws when the qrid is empty")]
        [TestCase("1234", null, QRType.TABLET, "Tests that the validation throws when the qrid is null")]
        public void TabletValidateSelfTest_InvalidTablets_ShouldTrow(string iccid, string qrid, QRType qrType, string Desc)
        {
            //Arrange
            QRSticker qr = new QRSticker(qrid, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode);
            Tablet tablet = new Tablet(simCard);
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => tablet.ValidateSelf(), Desc);
        }
        [TestCase("Tests that the validation throws when the tablet is missing a simcard")]
        public void TabletValidateSelfTest_TabletsMissingSimcards_ShouldTrow(string Desc)
        {
            //Arrange
            Tablet tablet = new Tablet();
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => tablet.ValidateSelf(), Desc);
        }
        #endregion ValidateSelf
    }
}
