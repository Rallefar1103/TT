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
using System.Security.Cryptography;

namespace TestUnit.ViewModel
{
    [TestFixture]
    public class LoginViewModelTest
    {
        [TestCase("SomeUser", "SomePassword", "Testing that the employee gets redirected to the correct page")]
        public void LoginUser_ValidLogin_ShouldSendToRegistrationView(string username, string password, string Desc)
        {
            // Mock
            var mockUser = Substitute.For<IEmployee>();
            mockUser.Login().Returns(true);
            var mockNav = Substitute.For<INavigationService>();
            mockNav.NavigateToMenu();

            // Arrange
            LoginViewModel vm = new LoginViewModel(mockUser, mockNav);
            vm.UsernameLabel = username;
            vm.PasswordLabel = password;

            // Act
            vm.LoginCommand.Execute("StringNeededToExecuteTheCommandForSomeReason");
            string ActualErrorMsg = vm.ErrorMsgLabel;

            // Assert
            mockUser.Received().Login();
            mockNav.Received().NavigateToMenu();
            Assert.AreEqual("", ActualErrorMsg, Desc);
        }
        [TestCase("", "", "Testing that it rejects a completely empty form")]
        [TestCase("SomeUsername", "", "Testing that it rejects a form without password")]
        [TestCase("", "SomePassword", "Testing that it rejects a form without username")]
        public void LoginUser_InvalidLogin_ShouldNotMakeLoginCall(string username, string password, string Desc)
        {
            // Mock
            var mockUser = Substitute.For<IEmployee>();
            mockUser.Login().Returns(false);

            // Arrange
            LoginViewModel vm = new LoginViewModel(mockUser);
            vm.UsernameLabel = username;
            vm.PasswordLabel = password;

            // Act
            vm.LoginCommand.Execute("StringNeededToExecuteTheCommandForSomeReason");
            string ActualErrorMsg = vm.ErrorMsgLabel;

            // Assert
            mockUser.DidNotReceive().Login();
            Assert.IsTrue(ActualErrorMsg != "", Desc);
        }
    }
}
