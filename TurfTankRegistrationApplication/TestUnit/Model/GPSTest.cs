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
    class GPSTest
    {
        #region ValidateSelf
        [TestCase("1234", "5678", "91011",QRType.basestation, SerialOrQR.BothSerialAndQRId, "Tests that a GPS is valid with a Base type")]
        [TestCase("1234", "5678", "91011", QRType.rover, SerialOrQR.BothSerialAndQRId, "Tests that a GPS is valid with a Rover type")]
        [TestCase("", "5678", "91011", QRType.basestation, SerialOrQR.OnlyQRId, "Tests that a gps is valid when the idrestriction is onlyQRID")]
        public void GPSValidateSelfTest_ValidGPS_ShouldNotThrow(string serialNumber, string qrid, string iccid, QRType qrType, SerialOrQR idRestriction,  string Desc) 
        {
            //Arrange
            QRSticker qr = new QRSticker(qrid, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode, qr);
            GPS gps = new GPS(simCard, serialNumber);
            //Act
            //Assert
            Assert.DoesNotThrow(() => gps.ValidateSelf(idRestriction), Desc);
        }

        [TestCase("1234", "5678", "91011", GPSType.Rover, QRType.basestation, SerialOrQR.BothSerialAndQRId, "Tests that an exception is thrown when the GPS type and QR type doesn't match")]
        [TestCase("1234", "", "91011", GPSType.Base, QRType.basestation, SerialOrQR.BothSerialAndQRId, "Tests that an exception is thrown when there is no QRID")]
        [TestCase("", "5678", "91011", GPSType.Base, QRType.basestation, SerialOrQR.BothSerialAndQRId, "Tests that an exception is thrown when it should have qr and serial, but the serial is missing")]
        [TestCase("1234", "5678", "91011", GPSType.NoType, QRType.notype, SerialOrQR.BothSerialAndQRId, "Tests that an exception is thrown when the GPS dosen't have a type")]
        [TestCase("1234", "5678", "", GPSType.Base, QRType.basestation, SerialOrQR.BothSerialAndQRId, "Tests that an exception is thrown if the simcard is invalid")]
        public void GPSValidateSelfTest_InvalidGPS_ShouldTrow(string serialNumber, string qrid, string iccid, GPSType gpsType, QRType qrType, SerialOrQR idRestriction, string Desc)
        {
            //Arrange
            QRSticker qr = new QRSticker(qrid, qrType);
            BarcodeSticker barcode = new BarcodeSticker(iccid);
            SimCard simCard = new SimCard(barcode, qr);
            GPS gps = new GPS(gpsType ,simCard, serialNumber);
            //Act
            //Assert
            Assert.Throws<ValidationException>(() => gps.ValidateSelf(idRestriction), Desc);
        }
    }
    #endregion ValidateSelf
}
