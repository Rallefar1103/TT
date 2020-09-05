using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Project; 
using System;

namespace Tests
{
    [TestFixture]
    public class SomeClassTest
    {
        private SomeClass FakeSomeClass(decimal salary)
        {
            return new SomeClass(salary);
        }

        [TestCase(4, 5, "Testing for small number")]
        [TestCase(-1, 0, "Testing for negative number")]
        [TestCase(123, 124, "Testing for large number")]
        public void AddOne_SingleNumber_ReturnsNumberIncrementedByOne(int Arranged, int Expected, string TestName)
        {
            int ArrangedValue = Arranged;
            int VALUE_NEEDED_FOR_INITIALISATION = 0; 
            SomeClass StubObject = FakeSomeClass(VALUE_NEEDED_FOR_INITIALISATION);
            int ExpectedValue = Expected;

            int ActualValue = StubObject.AddOne(ArrangedValue);

            Assert.IsTrue(
                ExpectedValue == ActualValue,
                TestName
            );
        }
        [TestCase(4, 4, "Testing for small number")]
        [TestCase(0, 0, "Testing for zero")]
        public void IncreaseSalary_SingleNumber_IncreaseInternalSalaryAttribute(int Arranged, int Expected, string TestName)
        {
            int ArrangedValue = Arranged;
            int INITIAL_SALARY_IS_ZERO = 0;
            SomeClass MockObject = FakeSomeClass(INITIAL_SALARY_IS_ZERO);
            int ExpectedValue = Expected;

            MockObject.IncreaseSalary(ArrangedValue);

            Assert.IsTrue(
                ExpectedValue == MockObject.Salary, 
                TestName 
            );
        }
        [Test]
        public void IncreaseSalary_NegativeNumber_ShouldThrowException()
        {
            int ArrangedValue = -3;
            int INITIAL_SALARY_IS_ZERO = 0;
            SomeClass MockObject = FakeSomeClass(INITIAL_SALARY_IS_ZERO);

            Assert.That(
                () => MockObject.IncreaseSalary(ArrangedValue),
                Throws.TypeOf<Project.CustomException>()
            );
        }
    }
}