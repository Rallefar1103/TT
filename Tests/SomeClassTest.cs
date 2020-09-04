using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Project; // Be aware of this import! Project and Tests are NOT the same project, and do not share namespace, which makes it better for compile time.

namespace Tests
{
    [TestFixture] // Denotes that SomeClassTest is a test-suite which should be run by Test/Run All Tests.
    public class SomeClassTest
    {
        private SomeClass ArrangedObject;

        [SetUp] // This method is called before each [Test] method.
        public void SetUp()
        {
            // Should ONLY be used if it enhances clarity. If in doubt, put all test code in the same test method.
            // IE. The reason this is made here, is because it can be assumed that all tests needs an instance of the SomeClass object.
            ArrangedObject = new SomeClass();
        }

        [TearDown] // This method is called after each [Test] method.
        public void TearDown()
        {
            // Should have the same restrictions as SetUp.
        }
     
        // TestCase takes its parameters and puts them in the Test Methods. 
        // This means this test is run 3 times with 3 different tests to try different aspects of the same requirement.
        // See if, and how, you can make the test fail (hint: float, please read line 44!)
        [TestCase(4,5, "Testing for small number")]
        [TestCase(-1,0, "Testing for negative number")]
        [TestCase(123,124, "Testing for large number")]
        public void Test_SomeClass_Should_Add_One_To_Value(int Arranged, int Expected, string TestName)
        {
            // The test is explicit in what requirement it is testing for. (Ie. "To_Value" specifies why the method exists. 
            // "To Bank Account" "Add_One_Million_To_House_Value", etc. are examples of a test explaining its requirement, so it is easier to understand)
            int ArrangedValue = Arranged;
            int ExpectedValue = Expected;

            int ActualValue = ArrangedObject.AddOne(ArrangedValue);
            
            Assert.IsTrue(
                ExpectedValue == ActualValue, // The assertion
                TestName // The fail message. Should be given by each TestCase. Will not give the message if an exception is thrown!
            );
        }
    }
}