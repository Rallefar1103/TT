using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Project; // Be aware of this import! Project and Tests are NOT the same project, and do not share namespace, which makes it better for compile time.
using System;

// ALL THE CODE HERE IS SomeClassTest CODE COMMENTED! IF THE TESTS WONT WORK, JUST SHIFT-CLICK ON THE "VIAL" IN TEST EXPLORER!
// This is primarily a lookup of how/why the tests are done. In reality, tests shouldn't be commented at all (the Test itself and TestName should be fully explanatory)
// Official documentation: https://docs.nunit.org/articles/nunit/intro.html

// General Rules:
// Everything should be fully clear in the Test. Don't use "magical variables" such as "0", give it a name (see Test 1)
// No logic in tests (if/else, for, while, etc.). The test should be contained. If logic is needed, make two seperate tests / use TestCase.
// Only one assert per test
// never unittest private methods. Only validate them through a public method

// "Stub Static References", can best be explained through example:
// Instead of using "Datetime.Now.DayOfWeek" (which cannot be tested, since the value is static based on the day, ie. you cant test "discount on friday" unless it is a friday!)
// Use a "seam" which creates a public interface which can manipulate the output so as to make testable code
// Their example:
// public interface IDateTimeProvider {    DayOfWeek DayOfWeek();    }                          Creates the interface
// In production code (logic example): dateTimeProvider.DayOfWeek() == DayOfWeek.Tuesday        Uses the interface in production
// In test code: var dateTimeProviderStub = new Mock<IDateTimeProvider>();                      Creates a Stub object in test
//               dateTimeProviderStub.Setup(dtp => dtp.DayOfWeek()).Returns(DayOfWeek.Monday);  Overwrites Stub attributes to make it testable
/*
namespace Tests
{
    // All Tests have the name: "The Class Being Tested"Test. Makes it easier to navigate through.
    [TestFixture] // Denotes that SomeClassTest is a test-suite which should be run by Test/Run All Tests.
    public class SomeClassTest_commented
    {
        // All methods which are private are helper methods which setup the test.
        // Methods which are public, should be a [TestCase], with an Arranged, Expected and TestName value.
        // If the parameter needs to take in a complex object (such as a list/dictionary), use helper methods to generate them, BUT
        // make sure it is obvious from the parameters what the values are (ie. no magic variables in helper methods! only variables!)
        private SomeClass FakeSomeClass_commented(decimal salary)
        {
            // This is a helperclass which creates a Fake, which can be either a stub or a mock.
            // A StubObject is a Fake, which is only used to get the actual value (see first Test)
            // A MockObject is a Fake, which is directly asserted against (see second Test)
            return new SomeClass(salary);
        }
     
        // TestCase takes its parameters and puts them in the Test Methods. 
        // This means this test is run 3 times with 3 different tests to try different aspects of the same requirement.
        [TestCase(4,5, "Testing for small number")]
        [TestCase(-1,0, "Testing for negative number")]
        [TestCase(123,124, "Testing for large number")]
        // The test should describe: The name of the method being tested. The scenario under which it's being tested. The expected behavior when the scenario is invoked.
        public void AddOne_SingleNumber_ReturnsNumberIncrementedByOne_commented(int Arranged, int Expected, string TestName)
        {
            int ArrangedValue = Arranged;
            int INITIAL_SALARY = 0; // This explicitates what the "0" in FakeSomeClass is used for. In a test, make as much explicit as possible.
            SomeClass StubObject = FakeSomeClass(INITIAL_SALARY);
            int ExpectedValue = Expected;

            int ActualValue = StubObject.AddOne(ArrangedValue);
            
            Assert.IsTrue(
                ExpectedValue == ActualValue, // The assertion
                TestName // The fail message. Should be given by each TestCase. Will not give the message if an exception is thrown!
            );
        }
        [TestCase(4, 4, "Testing for small number")]
        [TestCase(0, 0, "Testing for zero")]
        // The test should describe: The name of the method being tested. The scenario under which it's being tested. The expected behavior when the scenario is invoked.
        public void IncreaseSalary_SingleNumber_IncreaseInternalSalaryAttribute_commented(int Arranged, int Expected, string TestName)
        {
            int ArrangedValue = Arranged;
            int INITIAL_SALARY = 0;
            SomeClass MockObject = FakeSomeClass_commented(INITIAL_SALARY); // Please see the difference if it is a Stub or Mock object.
            int ExpectedValue = Expected;

            MockObject.IncreaseSalary(ArrangedValue);

            Assert.IsTrue(
                ExpectedValue == MockObject.Salary, // The assertion
                TestName // The fail message. Should be given by each TestCase. Will not give the message if an exception is thrown!
            );
        }
        [Test]
        // This tests for throwing an exception. (It could use "TestCase" instead of "Test", but this is just to show that Test works fine))
        public void IncreaseSalary_NegativeNumber_ShouldThrowException()
        {
            int ArrangedValue = -3;
            int INITIAL_SALARY = 0;
            SomeClass MockObject = FakeSomeClass(INITIAL_SALARY);

            // throwing an exception with Assert.Throws<NameOfException>() RETURNS that exception, which means it can be used to verify test messages.
            CustomException exception = Assert.Throws<Project.CustomException>(() => MockObject.IncreaseSalary(ArrangedValue));
            // An example of this is verifying that the text message is the same as in the Project.SomeClass file.
            // This is NOT necessarily needed, unless it is a specific requirement that an exception needs to throw some specific values under specific circumstances!
            Assert.AreEqual(exception.Message, "Salary must not increase by one", "Testing for message in SomeClass.IncreaseSalary when a negative number is given");
            // These assertions could continue if needed.
        }
    }
}
*/