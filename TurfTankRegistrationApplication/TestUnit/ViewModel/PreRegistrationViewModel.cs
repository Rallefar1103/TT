using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;

using TurfTankRegistrationApplication.Model;
using TurfTankRegistrationApplication.ViewModel;

namespace TestUnit.ViewModel
{
    [TestFixture]
    class PreRegistrationViewModel
    {
        #region PreRegisterComponent
        [TestCase()]
        public void PreregisterComponent_ValidRoverAndSimcard_ShouldCreateQRStickerWithSimcardAndTypeRover()
        {
            // Mock


            // Arrange


            // Act


            // Assert
        }

        [TestCase()]
        public void PreregisterComponent_InvalidQRException_ShouldCatchAndDisplayRelevantErrorMessage()
        {
            // Mock


            // Arrange


            // Act


            // Assert
        }

        #endregion PreRegisterComponent
    }
}
