using NUnit.Framework;
using saxjaxTryingToTest;

namespace CalculatorTests
{

    public class CalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }
        [TestCase(3,4,7)]
        [TestCase(-3, 4, 1)]
        [TestCase(3, -4, -1)]

        [Test]
        public void adding_a_and_b_equals_c(int a,int b, int expected)
        {
            Calculator calc = new Calculator();
            int result = calc.add(a, b);
            Assert.AreEqual(expected,result);
        }
    }
}